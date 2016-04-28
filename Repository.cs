using System;
using System.Collections.Specialized;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using XdsObjects;
using XdsObjects.Enums;
using System.IO;
using System.Net.NetworkInformation;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Runtime;
using System.Net;
using System.Net.Sockets;
using MedicalConnections.Security.Enums;
using MedicalConnections.Security.EventArguments;

namespace XdsRepository
{
    class Repository
    {
        public delegate void LogMessageHandler(String msg);
        public event LogMessageHandler LogMessageEvent;
        XdsDomain atnaTest = new XdsDomain();
        AuditEndpoint myAudit = new AuditEndpoint();
        ATNA_Messaging myATNA = new ATNA_Messaging();

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
                rootCertificate = Properties.Settings.Default.RootCertificate;
                serverCertificate = Properties.Settings.Default.ServerCertificate;
                serverCertPassword = Properties.Settings.Default.ServerCertPassword;
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
        public string rootCertificate;
        public string serverCertificate;
        public string serverCertPassword;
        bool atnaMessageComplete = false;
        #endregion

        #region "Start and Stop"
        internal void StartListen()
        {
            try
            {
                XdsGlobal.LogToFile(repositoryLog, XdsObjects.Enums.LogLevel.All, 3600);
                XdsGlobal.Log("This is a Test");
                // Create a new instance of the Server
                server = new XdsMtomServer();
                // Set up server events
                server.ProvideAndRegisterRequestReceived += new RegisterDocumentSetHandler(server_ProvideAndRegisterRequestReceived);
                server.RetrieveRequestReceived += new RetrieveHandler(server_RetrieveRequestReceived);
                // Listen for incoming requests
                server.Listen(repositoryURI);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": " + repositoryURI + " listening...");

                xds = new XdsDomain();
                SetUpAtna();
                atnaMessageComplete = myATNA.ATNA_Application_Start(atnaTest);
                if(atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Start audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Start audit event logged...");
                }

                bool httpsRegistry = registryURI.Contains("https");
                if (httpsRegistry == true)
                {
                    //add certificates
                    MedicalConnections.Security.TlsClientBouncyCastle bc = new MedicalConnections.Security.TlsClientBouncyCastle();
                    bc.AddTrustedRoot(File.ReadAllBytes(rootCertificate));
                    bc.LoadFromPfx(File.ReadAllBytes(serverCertificate), serverCertPassword);
                    //bc.AddTrustedRoot(File.ReadAllBytes(@"C:\HSS\XDS_Repository\Certificates\643.der"));
                    //bc.LoadFromPfx(File.ReadAllBytes(@"C:\HSS\XDS_Repository\Certificates\1606.p12"), "password");
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, bc);
                }
                else
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                }
            }
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": StartListen - " + exceptionMsg + "...\n");
            }
        }

        internal void StopListen()
        {
            if (server != null)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": " + repositoryURI + " not listening...");
                atnaMessageComplete = myATNA.ATNA_Application_Stop(atnaTest);
                if (atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Stop audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Stop audit event logged...");
                }
                atnaTest.AuditRepositories.Clear();
                server.UnListenAll();
            }
        }

        private void SetUpAtna()
        {
            //set up ATNA
            myAudit.Host = atnaHost;
            myAudit.Port = atnaPort;
            AuditProtocol atnaProtocol = AuditProtocol.Tcp;
            myAudit.Protocol = atnaProtocol;
            atnaTest.AuditSchema = XdsDomain.AuditSchemaType.DICOM;
            atnaTest.AuditSourceTypeCode = AuditSourceTypeCode.Application_Server_Process_Tier;
            atnaTest.AuditEnterpriseSiteID = authDomain;
            atnaTest.AuditSourceID = System.Environment.MachineName;
            atnaTest.AuditSourceAddress = repositoryId;
            atnaTest.RegistryEndpoint = new WebServiceEndpoint(registryURI);
            atnaTest.SubmissionRepositoryEndpoint = new WebServiceEndpoint(repositoryURI);
            atnaTest.AuditRepositories.Add(myAudit);
        }
        #endregion

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
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document Source SourceId - " + SubmissionSet.SourceID + "...");
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document Source host:port - " + RequestInfo.RemoteEndpoint.Address + ":" + RequestInfo.RemoteEndpoint.Port + "...");
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Message Id - " + RequestInfo.Message.Headers.MessageId + "...");
            atnaMessageComplete = myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, true);
            if (atnaMessageComplete == false)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event failed...");
            }
            else
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
            }

            XdsRegistryResponse myResponse = new XdsRegistryResponse();
            //rather than sending an error response the first error encountered, 
            //errors are added to the response when encountered and the response sent once all checks are complete
            bool errors = false;

            //if there are ExtrinsicDocument objects present then there should be matching Documents
            //that is if not checked here it may result in an error when the document(?) is saved in store
            // if not then send error response straight back
            if(SubmissionSet.Documents.Count > 0)
            {
                int docCount = 0;
                foreach (XdsDocument document in SubmissionSet.Documents)
                {
                    if (document.Data == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Missing document - " + document.UniqueID + "...");
                        atnaMessageComplete = myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                        }
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, "Missing document", "Document unique id - " + document.UniqueID);
                        errors = true;
                    }
                    else
                    {
                        //IHE requirement for repository to add repository uniqueId, document size and document hash attributes
                        docCount++;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Patient Id - " + document.PatientID + "...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") UniqueId - " + document.UniqueID + "...");
                        //insert repository unique id
                        document.RepositoryUniqueId = repositoryId;
                        //calculate size and hash
                        //if different from any values contained in submission from source reject
                        document.SetSizeAndHash();
                        bool HashSizeCheck = document.CheckSizeAndHash();
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") size - " + document.Size);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") hash - " + document.Hash);
                        if (!HashSizeCheck)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": XDSRepositoryMetadataError - Hash and/or Size atrributes of supplied document are in error...");
                            atnaMessageComplete = myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                            if (atnaMessageComplete == false)
                            {
                                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event failed...");
                            }
                            else
                            {
                                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                            }
                            myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                            myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRepositoryMetadataError, "Hash and/or Size atrributes of supplied document are in error", "");
                            errors = true;
                        }
                    }
                }
            }

            //if there have been any metadata issues picked up the repository will report them here and return error response
            if (errors == true)
            {
                foreach (var e in myResponse.Errors.ErrorList)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error in Submission - " + e.ErrorCode);
                }
                atnaMessageComplete = myATNA.Repository_Export(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                if (atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event logged...");
                }
                return myResponse;
            }

            try
            {
                using (var db = new RepositoryDataBase())
                {
                    foreach (XdsDocument document in SubmissionSet.Documents)
                    {
                        //Save document into the repository store
                        string currentDate = DateTime.Now.Date.ToString("yyyy_MM_dd");
                        string dir = Path.Combine(StoragePath, currentDate);
                        Directory.CreateDirectory(dir);
                        string location = Path.Combine(dir, document.UniqueID);
                        File.WriteAllBytes(location, document.Data);
                        //added 'http' for pre-connectathon test
                        document.Uri = @"http://" + location.Substring(3);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document saved to - " + location + "...");
                        //document details saved to database
                        db.Documents.Add(new Document()
                        {
                            DocumentId = document.UniqueID,
                            Location = location,
                            MimeType = document.MimeType,
                            DocDateTime = document.CreationTime
                        });
                        db.SaveChanges();
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document details saved in db...");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Sending SubmissionSet to Registry...");
                        atnaMessageComplete = myATNA.Repository_Export(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, true);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event logged...");
                        }
                    }
                }
                //metadata sent to registry
                myResponse = xds.RegisterDocumentSet(SubmissionSet);
                //were there errors in the registry?
                if (myResponse.Errors.ErrorList.Count > 0)
                {
                    foreach(var e in myResponse.Errors.ErrorList)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry error returned - " + e.ErrorCode + " - " + e.CodeContext);
                    }
                    foreach (XdsDocument d in SubmissionSet.Documents)
                    {
                        //need to remove previously saved database record
                        RollBackDatabaseSave(d.UniqueID);
                        //need to remove document from physical store
                        RemoveDocumentFromStore(d.UniqueID);
                    }
                }
                //return response to source
                return myResponse;
            }
            //registry address was not reachable etc
            catch(SocketException socEx)
            {
                atnaMessageComplete = myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                if (atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                }
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unable to connect to the Registry to register document");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsErrorCode.XDSRegistryNotAvailable, "Unable to connect to the Registry to register document", socEx.Message);
                
                foreach (XdsDocument d in SubmissionSet.Documents)
                {
                    //need to remove previously saved database record
                    RollBackDatabaseSave(d.UniqueID);
                    //need to remove document from physical store
                    RemoveDocumentFromStore(d.UniqueID);
                }
                return myResponse;
            }
            //any other error
            catch (Exception ex)
            {
                atnaMessageComplete = myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                if (atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                }
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + ex.Message + " - " + ex.InnerException.ToString());
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.GeneralException, ex.Message, ex.InnerException.ToString());
                foreach (XdsDocument d in SubmissionSet.Documents)
                {
                    //need to remove previously saved database record
                    RollBackDatabaseSave(d.UniqueID);
                    //need to remove document from physical store
                    RemoveDocumentFromStore(d.UniqueID);
                }
                return myResponse;
            }
        }

        private void RemoveDocumentFromStore(string docUniqueId)
        {
            try
            {
                string currentDate = DateTime.Now.Date.ToString("yyyy_MM_dd");
                string dir = Path.Combine(StoragePath, currentDate);
                string location = Path.Combine(dir, docUniqueId);
                File.Delete(location);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Removed document " + docUniqueId + " from " + location + "...");
            }
            catch(Exception ex)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error removing record from store - " + ex.Message + " - " + ex.InnerException.ToString());
            }
        }

        private void RollBackDatabaseSave(string docUniqueId)
        {
            try
            {
                using (var db = new RepositoryDataBase())
                {
                    foreach (var d in db.Documents)
                    {
                        if (d.DocumentId == docUniqueId)
                        {
                            db.Documents.Remove(d);
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Removed document " + docUniqueId + " from database...");
                            break;
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error removing record - " + ex.Message + " - " + ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Event to handle incoming document retrieve requests
        /// </summary>
        /// <param name="Request">Incoming Requests</param>
        /// <returns>XdsRetrieveResponse which carries matching documents</returns>
        XdsRetrieveResponse server_RetrieveRequestReceived(XdsRetrieveRequest Request, XdsRequestInfo RequestInfo)
        {
            string itemId = "";
            //rather than sending an error response the first error encountered, 
            //errors are added to the response when encountered and the response sent once all checks are complete
            bool errors = false;
            XdsRetrieveResponse myResponse = new XdsRetrieveResponse();
            try
            {
                LogMessageEvent("--- --- ---");
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve document request received");
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document Consumer - " + RequestInfo.RemoteEndpoint.Address + ":" + RequestInfo.RemoteEndpoint.Port + "...");
                //response.Status = XdsObjects.Enums.RegistryResponseStatus.Success; // gets changed if we have a failure
                XdsPatient myPatient = new XdsPatient();
                WebServiceEndpoint myRepositoryEndPoint = new WebServiceEndpoint(repositoryURI);
                
                foreach (XdsRetrieveItem item in Request.Requests)
                {
                    //check for documentUniqueId in request
                    if(item.DocumentUniqueID == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": DocumentUniqueID is not present in retrieval request");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRepositoryError, "DocumentUniqueID is not present in retrieval request", "");
                        atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, "Not Present", false);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                        }
                        errors = true;
                    }
                    //check for repositoryUniqueId in request
                    if (item.RepositoryUniqueID == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository UniqueId is not present in retrieval request");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "Repository UniqueId is not present in retrieval request", "");
                        atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                        }
                        errors = true;
                    }
                    //check that the stated repositoryUniqueId really is us
                    else if (item.RepositoryUniqueID != repositoryId)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + item.RepositoryUniqueID + "...");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsErrorCode.XDSUnknownRepositoryId, "", item.RepositoryUniqueID);
                        atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                        }
                        errors = true;
                    }

                    //if there have been any issues in the retrieval request will report them here and return error response
                    if (errors == true)
                    {
                        return myResponse;
                    }

                    string location = "";
                    string mimeType = "";
                    //does document database record exist?
                    using (var dbRep = new RepositoryDataBase())
                    {
                        foreach (var doc in dbRep.Documents)
                        {
                            if (doc.DocumentId == item.DocumentUniqueID)
                            {
                                //locate mimeType and location from database
                                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Located document database record for - " + item.DocumentUniqueID + "...");
                                location = doc.Location;
                                mimeType = doc.MimeType; //this was inserted to pass pre-connectathon test 12345
                                //ensure document exists in repository store
                                if (!File.Exists(location))
                                {
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cannot locate document in repository store - " + location);
                                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                                    myResponse.AddError(XdsErrorCode.XDSMissingDocument, "Cannot locate document in repository store", "");
                                    atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                                    if (atnaMessageComplete == false)
                                    {
                                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                                    }
                                    else
                                    {
                                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                                    }
                                    return myResponse;
                                }
                                else
                                {
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Located physical document - " + doc.Location + "...");
                                }
                                break;
                            }
                        }
                    }

                    //if no record of document found
                    if(location == "")
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": No record of document " +  item.DocumentUniqueID + " found ...");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsErrorCode.XDSMissingDocument, "No record of document " +  item.DocumentUniqueID + " found", "");
                        atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        if (atnaMessageComplete == false)
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                        }
                        return myResponse;
                    }

                    XdsDocument document = new XdsDocument(location);
                    document.UniqueID = item.DocumentUniqueID;
                    document.MimeType = mimeType; //this was inserted to pass pre-connectathon test 12345
                    document.RepositoryUniqueId = item.RepositoryUniqueID;
                    atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, true);
                    if (atnaMessageComplete == false)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                    }
                    else
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                    }
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Returning requested document for " + item.DocumentUniqueID);
                    myResponse.Documents.Add(document);
                }
                return myResponse;
            }
            catch(Exception ex)
            {
                atnaMessageComplete = myATNA.Repository_Retrieve(atnaTest, itemId, false);
                if (atnaMessageComplete == false)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event failed...");
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retrieve Document Set Export audit event logged...");
                }
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsErrorCode.XDSMissingDocument, "Error retrieving document", ex.Message);
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error retrieving document - " + exceptionMsg + "...\n");
                return myResponse;
            }
        }
        #endregion
    }
}
