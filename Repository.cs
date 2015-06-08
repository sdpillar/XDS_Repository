using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XdsObjects;
using XdsObjects.Enums;
using System.IO;
using System.Net.NetworkInformation;
using System.ServiceModel;

namespace XdsRepository
{
    class Repository
    {
        public delegate void LogMessageHandler(String msg);
        public event LogMessageHandler LogMessageEvent;
        XdsDomain atnaTest = new XdsDomain();
        AuditEndpoint myAudit = new AuditEndpoint();


        public bool readProperties()
        {
            try
            {
                authDomain = Properties.Settings.Default.AuthDomain;
                registryURI = Properties.Settings.Default.RegistryURI;
                StoragePath = Properties.Settings.Default.RepositoryPath;
                repositoryId = Properties.Settings.Default.RepositoryId;
                repositoryURI = Properties.Settings.Default.RepositoryURI;
                repositoryLog = Properties.Settings.Default.RepositoryLog;
                atnaHost = Properties.Settings.Default.AtnaHost;
                atnaPort = Properties.Settings.Default.AtnaPort;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        #region "Varibles and Constants"
        // the Server object which handles the incoming Provide-and-Register and Retrieve requests
        XdsMtomServer server;
        XdsDomain xds;

        public string authDomain;
        public string registryURI;
        public string StoragePath;
        public string repositoryId;
        public string repositoryURI;
        public string repositoryLog;
        public string atnaHost;
        public int atnaPort;
        #endregion

        internal void StartListen()
        {
            XdsGlobal.LogToFile(repositoryLog, XdsObjects.Enums.LogLevel.All, 60);
            // Create a new instance of the Server
            server = new XdsMtomServer();

            // Set up server events
            server.ProvideAndRegisterRequestReceived += new RegisterDocumentSetHandler(server_ProvideAndRegisterRequestReceived);
            server.RetrieveRequestReceived += new RetrieveHandler(server_RetrieveRequestReceived);

            // Listen for incoming requests
            server.Listen(repositoryURI);
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": " + repositoryURI + " listening...");

            //set up ATNA
            myAudit.Host = atnaHost;
            myAudit.Port = atnaPort;
            AuditProtocol atnaProtocol = AuditProtocol.Tcp;
            myAudit.Protocol = atnaProtocol;
            //atnaTest.RegistryEndpoint = xds.RegistryEndpoint;
            atnaTest.RegistryEndpoint = new WebServiceEndpoint(registryURI);
            //atnaTest.SubmissionRepositoryEndpoint = xds.SubmissionRepositoryEndpoint;
            atnaTest.SubmissionRepositoryEndpoint = new WebServiceEndpoint(repositoryURI);
            atnaTest.AuditRepositories.Add(myAudit);
            atnaTest.AuditEnterpriseSiteID = "1.3.6.1.4.1.21367.9";
            atnaTest.ValidateOnSending = true;
            atnaTest.AuditSchema = XdsDomain.AuditSchemaType.DICOM;
            //xds.AuditSourceTypeCode = AuditSourceTypeCode.Data_Acquisition_Device_Or_Instrument;
            XdsAudit.ActorStart(atnaTest);
            Directory.CreateDirectory(StoragePath);

            xds = new XdsDomain();
            // Set up the Registry we are going to talk to
            xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
            xds.SubmissionRepositoryEndpoint = new WebServiceEndpoint(repositoryURI);
        }

        /*internal void setUpLogWindow()
        {
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": " + repositoryURI + " listening...");
        }*/

        internal void StopListen()
        {
            if (server != null)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": " + repositoryURI + " not listening...");
                server.UnListenAll();
            }
        }

        #region "Server Events"
        /// <summary>
        /// Server event to handle incoming Provide-and-Register request
        /// </summary>
        /// <param name="SubmissionSet">The SubmissionSet object which carries the documents (may also have documents within folders)</param>
        /// <returns>XdsRegistryResponse</returns>
        /// 
        XdsRegistryResponse server_ProvideAndRegisterRequestReceived(XdsSubmissionSet SubmissionSet, XdsRequestInfo RequestInfo)
        {
            LogMessageEvent("--- --- ---");
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Provide and Register request received...");
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet.SourceId - " + SubmissionSet.SourceID);
            XdsRegistryResponse myResponse = new XdsRegistryResponse();
            try
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Login audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, true);
                XdsPatient myPatient = new XdsPatient();
                myPatient.CompositeId = SubmissionSet.PatientID;
                List<string> StudyUIDList = new List<string>();
                StudyUIDList.Add(SubmissionSet.UniqueID);
                XdsAudit.PHIImport(atnaTest, SubmissionSet.SourceID, StudyUIDList, myPatient);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");

                //count the number of documents in submissionset
                int docCount = SubmissionSet.Documents.Count;
                if (docCount == 0) //return failure if there are none
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": No document metadata present - ");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                    XdsAudit.UserAuthentication(atnaTest, false);
                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocumentMetadata, "", "");
                    return myResponse;
                }

                //check that Authority Domain of patient matches with that of Repository
                if (SubmissionSet.PatientInfo.ID_Root != authDomain)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Authority Domains do not match...");
                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownCommunity, "Authority Domains do not match", "");
                    return myResponse;
                }

                using (var db = new RepositoryDataBase())
                {
                    docCount = 0;
                    foreach (XdsDocument document in SubmissionSet.Documents)
                    {
                        docCount++;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Patient Id - " + document.PatientID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") UniqueId - " + document.UniqueID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ").UUID - " + document.UUID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet.UUID - " + SubmissionSet.UUID + "...");

                        if (document.RepositoryUniqueId == null)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Id is not present - ");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", "");
                            return myResponse;
                        }
                        // check that the stated repository OID really is us (this is an example of how to return an error)
                        else if (document.RepositoryUniqueId != repositoryId)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + document.RepositoryUniqueId + "...");
                            //logMessageHandler(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + repositoryId + "...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", document.RepositoryUniqueId);
                            //return new XdsRegistryResponse(XdsErrorCode.XDSUnknownRepositoryId, "", document.RepositoryUniqueId);
                            return myResponse;
                        }

                        //document.RepositoryUniqueId = repositoryId;    

                        if (document.Data == null)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": missing document...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, "", document.UniqueID);
                            //return new XdsRegistryResponse(XdsErrorCode.XDSMissingDocument, "", document.UniqueID);
                            return myResponse;
                        }

                        document.SetSizeAndHash();
                        bool HashSizeCheck = document.CheckSizeAndHash();
                        if (!HashSizeCheck)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": XDSRepositoryMetadataError - Hash and/or Size atrributes of supplied document are in error...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRepositoryMetadataError, "", "Hash and/or Size atrributes of supplied document are in error");
                            //return new XdsRegistryResponse(XdsErrorCode.XDSRepositoryMetadataError, "", "Hash and/or Size atrributes of supplied document are in error");
                            return myResponse;
                        }

                        //Save Content 
                        string dir = Path.Combine(StoragePath, "");
                        Directory.CreateDirectory(dir);
                        string location = Path.Combine(dir, document.UniqueID);
                        System.IO.File.WriteAllBytes(location, document.Data);

                        System.IO.File.WriteAllText(StoragePath + document.UniqueID + ".mime", document.MimeType);

                        db.Documents.Add(new Document()
                        {
                            DocUUID = document.UUID,
                            DocumentId = document.UniqueID,
                            MimeType = document.MimeType,
                            Location = location,
                            DocDateTime = document.CreationTime,
                            DocSize = document.Size
                        });

                        db.SaveChanges();
                    }
                }
                // then send whole lot off to the chosen registry (main data will NOT be sent - this is
                // automatic and internal!)  Then pass back the response
            }
            catch (ArgumentNullException nrExc)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + nrExc.Message);
                XdsAudit.UserAuthentication(atnaTest, false);
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, "", SubmissionSet.UniqueID);
                //return new XdsRegistryResponse(XdsErrorCode.XDSMissingDocument, SubmissionSet.UniqueID);
                return myResponse;

            }
            catch (Exception ex)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + ex.Message);
                XdsAudit.UserAuthentication(atnaTest, false);
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.GeneralException, ex.Message, "");
                //return new XdsRegistryResponse(XdsErrorCode.GeneralException, ex.Message);
                return myResponse;
            }

            try
            {
                // Pass back the Registry Response
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Sending SubmissionSet to Registry...");
                XdsPatient myPatient = new XdsPatient();
                myPatient.CompositeId = SubmissionSet.PatientID;
                System.Uri hssRegistry = new System.Uri(registryURI);
                List<string> StudyUIDList = new List<string>();
                StudyUIDList.Add(SubmissionSet.UniqueID);
                //XdsAudit.PHIImport(atnaTest, SubmissionSet.SourceID, StudyUIDList, myPatient);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Export audit event logged...");
                XdsAudit.PHIExport(atnaTest, hssRegistry, StudyUIDList, myPatient);
                //List<string> StudyUIDList = new List<string>();
                //StudyUIDList.Add(SubmissionSet.UniqueID);
                //XdsAudit.PHIImport(atnaTest, SubmissionSet.SourceID, StudyUIDList, myPatient);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return xds.RegisterDocumentSet(SubmissionSet);
            }
            catch (NullReferenceException nrEx)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - NullReferencException");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRegistryNotAvailable, SubmissionSet.UniqueID, nrEx.InnerException.Message);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return myResponse;
            }
            catch (System.ServiceModel.EndpointNotFoundException endPointEx)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - EndPointNotFoundException");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRegistryNotAvailable, SubmissionSet.UniqueID, endPointEx.InnerException.Message);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return myResponse;
            }
            catch (Exception ex)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + ex.InnerException.InnerException.Message);
                XdsAudit.UserAuthentication(atnaTest, false);
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRegistryError, SubmissionSet.UniqueID, ex.InnerException.Message);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                //return new XdsRegistryResponse(XdsErrorCode.XDSRegistryError, SubmissionSet.UniqueID);
                return myResponse;
            }
        }

        /// <summary>
        /// Event to handle incoming document retrieve requests
        /// </summary>
        /// <param name="Request">Incoming Requests</param>
        /// <returns>XdsRetrieveResponse which carries matching documents</returns>
        XdsRetrieveResponse server_RetrieveRequestReceived(XdsRetrieveRequest Request, XdsRequestInfo RequestInfo)
        {
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive document request received");

            XdsRetrieveResponse response = new XdsRetrieveResponse();
            response.Status = XdsObjects.Enums.RegistryResponseStatus.Success; // gets changed if we have a failure
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Login audit event logged...");
            XdsAudit.UserAuthentication(atnaTest, true);
            XdsPatient myPatient = new XdsPatient();
            XdsRegistryResponse myResponse = new XdsRegistryResponse();
            WebServiceEndpoint myRepositoryEndPoint = new WebServiceEndpoint(repositoryURI);
            XdsAudit.RetrieveDocumentSet(atnaTest, "Retrieve Request", myRepositoryEndPoint, myPatient, response, true);

            foreach (XdsRetrieveItem item in Request.Requests)
            {
                if (item.RepositoryUniqueID == null)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Id is not present - ");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                    XdsAudit.UserAuthentication(atnaTest, false);
                    response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    response.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", "");
                    return response;
                }
                // check that the stated repository OID really is us (this is an example of how to return an error)
                else if (item.RepositoryUniqueID != repositoryId)
                //if (item.RepositoryUniqueID != repositoryId)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + item.RepositoryUniqueID + "...");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                    XdsAudit.UserAuthentication(atnaTest, false);
                    response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    response.AddError(XdsErrorCode.XDSUnknownRepositoryId, "", item.RepositoryUniqueID);
                    //return new XdsRetrieveResponse(XdsErrorCode.XDSUnknownRepositoryId, "", item.RepositoryUniqueID);
                    return response;
                }

                // check the document exists
                // in a real world application, the matching could be done in database queries
                if (!System.IO.File.Exists(StoragePath + item.DocumentUniqueID))
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cannot locate - " + StoragePath + item.DocumentUniqueID + "...");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                    XdsAudit.UserAuthentication(atnaTest, false);
                    response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    response.AddError(XdsErrorCode.XDSMissingDocument, "", item.DocumentUniqueID);
                    //return new XdsRetrieveResponse(XdsErrorCode.XDSMissingDocument, "", item.DocumentUniqueID);
                    return response;
                }

                // otherwise just pick up the document and mimetype
                XdsDocument document = new XdsDocument(StoragePath + item.DocumentUniqueID);
                //document.MimeType = System.IO.File.ReadAllText(StoragePath + item.DocumentUniqueID + ".mime");

                document.UniqueID = item.DocumentUniqueID;
                document.HomeCommunityID = item.RepositoryUniqueID;
                document.RepositoryUniqueId = item.RepositoryUniqueID;
                //document.MimeType = 
                //<mimeType>application/octet-stream</mimeType>

                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Returning requested document for " + item.DocumentUniqueID);
                response.Documents.Add(document);
            }
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
            XdsAudit.UserAuthentication(atnaTest, false);
            return response;
        }
        #endregion
    }

    
}
