using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using XdsObjects;
using XdsObjects.Enums;
using System.IO;
//using System.Net.NetworkInformation;
//using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
//using System.Runtime;

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
                thumbprint = Properties.Settings.Default.Thumbprint;
                return true;
            }
            catch(Exception ex)
            {
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": readProperties - " + exceptionMsg + "...\n");
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
        public string thumbprint;
        public string appId;
        #endregion

        internal void StartListen()
        {
            try
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
                atnaTest.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                atnaTest.SubmissionRepositoryEndpoint = new WebServiceEndpoint(repositoryURI);
                atnaTest.AuditRepositories.Add(myAudit);
                atnaTest.AuditEnterpriseSiteID = authDomain;
                atnaTest.ValidateOnSending = true;
                atnaTest.AuditSchema = XdsDomain.AuditSchemaType.DICOM;
                XdsAudit.ActorStart(atnaTest);

                xds = new XdsDomain();
                // Set up the Registry we are going to talk to
                //xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                //certificate to reference
                //X509Certificate2 myCert = new X509Certificate2();
                //myCert = GetCertificateByThumbprint("3d03dd7f2486afe4a857af42c2c3e8d5fd029699", StoreLocation.LocalMachine);
                /*myCert = GetCertificateByThumbprint(thumbprint, StoreLocation.LocalMachine);
                if (myCert == null)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Required certificate not found...\n");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not created...\n");
                }
                else
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, myCert);
                }*/
                xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
            }
            catch(Exception ex)
            {
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": StartListen - " + exceptionMsg + "...\n");
            }
        }

        private static X509Certificate2 GetCertificateByThumbprint(string certificateThumbPrint, StoreLocation certificateStoreLocation)
        {
            X509Certificate2 certificate = null;
            X509Store certificateStore = new X509Store(certificateStoreLocation);
            certificateStore.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certCollection = certificateStore.Certificates;
            foreach (X509Certificate2 cert in certCollection)
            {
                if (cert.Thumbprint != null && cert.Thumbprint.Equals(certificateThumbPrint, StringComparison.OrdinalIgnoreCase))
                {
                    certificate = cert;
                    break;
                }
            }
            return certificate;
        }

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
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Message Id - " + RequestInfo.Message.Headers.MessageId + "...");
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet.SourceId - " + SubmissionSet.SourceID);
            XdsRegistryResponse myResponse = new XdsRegistryResponse();
            try
            {
                XdsAudit.UserAuthentication(atnaTest, true);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Login audit event logged...");

                XdsPatient myPatient_1 = new XdsPatient();
                myPatient_1.CompositeId = SubmissionSet.PatientID;
                List<string> StudyUIDList_1 = new List<string>();
                StudyUIDList_1.Add(SubmissionSet.UniqueID);
                XdsAudit.PHIImport(atnaTest, SubmissionSet.SourceID, StudyUIDList_1, myPatient_1);
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
                    XdsAudit.UserAuthentication(atnaTest, false);
                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownCommunity, "Authority Domains do not match", "");
                    return myResponse;
                }

                using (var db = new RepositoryDataBase())
                {
                    docCount = 0;
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet contains " + SubmissionSet.Documents.Count + " documents...");
                    foreach (XdsDocument document in SubmissionSet.Documents)
                    {
                        docCount++;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Patient Id - " + document.PatientID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") UniqueId - " + document.UniqueID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ").UUID - " + document.UUID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet.UUID - " + SubmissionSet.UUID + "...");

                        /*
                        if (document.RepositoryUniqueId == null)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Id is not present - ");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", "");
                            return myResponse;
                        }
                        else if (document.RepositoryUniqueId != repositoryId)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + document.RepositoryUniqueId + "...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", document.RepositoryUniqueId);
                            return myResponse;
                        }
                        */
                        if (document.Data == null)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Missing document...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, "", document.UniqueID);
                            return myResponse;
                        }

                        document.RepositoryUniqueId = repositoryId;
                        document.SetSizeAndHash();

                        bool HashSizeCheck = document.CheckSizeAndHash();
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document size - " + document.Size);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document hash - " + document.Hash);
                        if (!HashSizeCheck)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": XDSRepositoryMetadataError - Hash and/or Size atrributes of supplied document are in error...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                            XdsAudit.UserAuthentication(atnaTest, false);
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRepositoryMetadataError, "", "Hash and/or Size atrributes of supplied document are in error");
                            return myResponse;
                        }

                        //Save document into the repository store
                        string currentDate = DateTime.Now.Date.ToString("yyyy_MM_dd");
                        string dir = Path.Combine(StoragePath, currentDate);
                        Directory.CreateDirectory(dir);
                        string location = Path.Combine(dir, document.UniqueID);
                        File.WriteAllBytes(location, document.Data);
                        document.Uri = location;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document saved to - " + location + "...");
                        //System.IO.File.WriteAllText(StoragePath + document.UniqueID + ".mime", document.MimeType);
                        //Save document info into repository database
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
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document details saved in db...");
                    }
                }

                // then send whole lot off to the chosen registry (main data will NOT be sent - this is
                // automatic and internal!)  
                //Then pass back the response
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Sending SubmissionSet to Registry...");
                XdsPatient myPatient_2 = new XdsPatient();
                myPatient_2.CompositeId = SubmissionSet.PatientID;
                System.Uri hssRegistry = new System.Uri(registryURI);
                List<string> StudyUIDList_2 = new List<string>();
                StudyUIDList_2.Add(SubmissionSet.UniqueID);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Export audit event logged...");
                XdsAudit.PHIExport(atnaTest, hssRegistry, StudyUIDList_2, myPatient_2);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);

                return xds.RegisterDocumentSet(SubmissionSet);
            }
            catch (NullReferenceException nrEx)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - NullReferencException");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRegistryNotAvailable, SubmissionSet.UniqueID, nrEx.InnerException.Message);

                string exceptionMsg = nrEx.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": server_ProvideAndRegisterRequestReceived_1 - " + exceptionMsg + "...\n");

                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return myResponse;
            }
            catch (System.ServiceModel.EndpointNotFoundException endPointEx)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - EndPointNotFoundException");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRegistryNotAvailable, SubmissionSet.UniqueID, endPointEx.InnerException.Message);
                
                string exceptionMsg = endPointEx.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": server_ProvideAndRegisterRequestReceived_2 - " + exceptionMsg + "...\n");

                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return myResponse;
            }
            catch (ArgumentNullException nrExc)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - ArgumentNullException");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, SubmissionSet.UniqueID, nrExc.InnerException.Message);

                string exceptionMsg = nrExc.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": server_ProvideAndRegisterRequestReceived_3 - " + exceptionMsg + "...\n");

                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return myResponse;
            }
            catch (Exception ex)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + ex.Message);
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.GeneralException, ex.Message, ex.InnerException.ToString());

                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": server_ProvideAndRegisterRequestReceived_4 - " + exceptionMsg + "...\n");

                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
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
            XdsRetrieveResponse response = new XdsRetrieveResponse();
            try
            {
                LogMessageEvent("--- --- ---");
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive document request received");
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
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + item.RepositoryUniqueID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                        XdsAudit.UserAuthentication(atnaTest, false);
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsErrorCode.XDSUnknownRepositoryId, "", item.RepositoryUniqueID);
                        return response;
                    }

                    /*
                    if (item.HomeCommunityID == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Home Community Id is not present - ");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                        XdsAudit.UserAuthentication(atnaTest, false);
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownCommunity, "", "");
                        return response;
                    }
                    else if (item.HomeCommunityID != authDomain)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Home Community Id - " + item.HomeCommunityID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                        XdsAudit.UserAuthentication(atnaTest, false);
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsErrorCode.XDSUnknownCommunity, "", item.HomeCommunityID);
                        return response;
                    }
                    */
                    string docId = "";
                    string location = "";
                    using (var dbRep = new RepositoryDataBase())
                    {
                        foreach (var doc in dbRep.Documents)
                        {
                            docId = doc.DocumentId;
                            if (docId == item.DocumentUniqueID)
                            {
                                location = doc.Location;
                                if (!File.Exists(location))
                                {
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cannot locate - " + location + "...");
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                                    XdsAudit.UserAuthentication(atnaTest, false);
                                    response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                                    response.AddError(XdsErrorCode.XDSMissingDocument, "", item.DocumentUniqueID);
                                    return response;
                                }
                                else
                                {
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Located document - " + docId + "...");
                                }
                                break;
                            }
                        }
                    }

                    // otherwise just pick up the document and mimetype
                    XdsDocument document = new XdsDocument(location);
                    document.UniqueID = item.DocumentUniqueID;
                    //document.HomeCommunityID = item.HomeCommunityID;
                    document.RepositoryUniqueId = item.RepositoryUniqueID;

                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Returning requested document for " + item.DocumentUniqueID);
                    response.Documents.Add(document);
                }
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": User Logout audit event logged...");
                XdsAudit.UserAuthentication(atnaTest, false);
                return response;
            }
            catch(Exception ex)
            {
                response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                response.AddError(XdsErrorCode.XDSMissingDocument, ex.Message, ex.InnerException.ToString());
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": server_RetrieveRequestReceived - " + exceptionMsg + "...\n");
                return response;
            }
        }
        #endregion
    }
}
