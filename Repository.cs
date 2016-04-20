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
                //xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                /*
                //atnaTest.AuditMessageGenerated += AtnaTest_AuditMessageGenerated;
                //set up ATNA
                myAudit.Host = atnaHost;
                myAudit.Port = atnaPort;
                AuditProtocol atnaProtocol = AuditProtocol.Tcp;
                myAudit.Protocol = atnaProtocol;
                atnaTest.AuditEnterpriseSiteID = authDomain;
                atnaTest.AuditSourceTypeCode = AuditSourceTypeCode.Application_Server_Process_Tier;
                atnaTest.AuditSchema = XdsDomain.AuditSchemaType.DICOM;

                atnaTest.AuditSourceID = System.Environment.MachineName;
                atnaTest.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                atnaTest.AuditRepositories.Add(myAudit);
                myATNA.ATNA_Application_Start(atnaTest);
                */
                SetUpAtna();
                myATNA.ATNA_Application_Start(atnaTest);

                //XdsAudit.ActorStart(atnaTest);
                //XdsAudit.UserAuthentication(atnaTest, true);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Start audit event logged...");

                // Set up the Registry we are going to talk to
                //xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                //certificate to reference

                
                MedicalConnections.Security.TlsClientBouncyCastle bc = new MedicalConnections.Security.TlsClientBouncyCastle();
                //TlsClientBouncyCastle bc = new TlsClientBouncyCastle();
                
                bc.AddTrustedRoot(File.ReadAllBytes(@"C:\HSS\XDS_Repository\Certificates\643.der"));
                bc.LoadFromPfx(File.ReadAllBytes(@"C:\HSS\XDS_Repository\Certificates\1606.p12"), "password");
                xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, bc);
                
                /*
                X509Certificate2 myCert = new X509Certificate2();
                myCert = GetCertificateByThumbprint(thumbprint, StoreLocation.LocalMachine);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                if (thumbprint != "")
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, myCert);
                }
                else
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                }
                */
                //X509Certificate2UI.DisplayCertificate(myCert);
                /*
                if(thumbprint != "")
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, myCert);
                    bool connected = testConnection(registryURI);
                    if (connected == false)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not connected...\n");
                    }
                    else
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint connected...\n");
                    }
                }
                else
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                    bool connected = testConnection(registryURI);
                    if (connected == false)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not connected...\n");
                    }
                    else
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint connected...\n");
                    }
                }
                */
                //myCert = GetCertificateByThumbprint("2bf0110aa0fb4deb55b63b3deb91ed83751ea81a", StoreLocation.LocalMachine);

                /*
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cert thumbprint - " + thumbprint);
                if (thumbprint != "")
                {
                    myCert = GetCertificateByThumbprint(thumbprint, StoreLocation.LocalMachine);
                    if (myCert == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Required certificate not found...\n");
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not created...\n");
                        xds.RegistryEndpoint = null;
                    }
                    else
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Picked up certificate...");
                        //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, myCert);
                        bool connected = testConnection(registryURI);
                        if(connected == false)
                        {
                            xds.RegistryEndpoint = null;
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not created...\n");
                        }
                        else
                        {
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint created...\n");
                        }
                    }
                }
                else
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                    bool connected = testConnection(registryURI);
                    if(connected == false)
                    {
                        xds.RegistryEndpoint = null;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint not created...\n");
                    }
                    else
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Registry endpoint created...\n");
                    }
                }
                */
            }
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": StartListen - " + exceptionMsg + "...\n");
            }
        }

        public int testRegConnection()
        {
            X509Certificate2 myCert = new X509Certificate2();
            //LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Testing connection to registry...");
            string urlPrefix = registryURI.Substring(0, registryURI.IndexOf(":")); //is the registry url https or http?
            if(urlPrefix == "https")
            {
                myCert = GetCertificateByThumbprint(thumbprint, StoreLocation.LocalMachine); //attempt to retrieve a certificate with that thumprint
                if (myCert == null)
                {
                    //cannot create registry endpoint
                    xds.RegistryEndpoint = null;
                    return 1;
                }
                else
                {
                    bool connected = testConnection(registryURI); //test connection to https address - is it open?
                    if(connected == true)
                    {
                        //connection is open
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        xds.RegistryEndpoint = new WebServiceEndpoint(registryURI, myCert);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connected to registry endpoint...\n");
                        return 0;
                    }
                    else
                    {
                        xds.RegistryEndpoint = null;
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unable to connect to registry endpoint...\n");
                        return 1;
                    }
                }
            }
            else if(urlPrefix == "http")
            {
            //certificate doesn't matter
                bool connected = testConnection(registryURI); //test connection to http address - is it open?
                if(connected == true)
                {
                    xds.RegistryEndpoint = new WebServiceEndpoint(registryURI);
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connected to registry endpoint...\n");
                    return 0;
                }
                else
                {
                    xds.RegistryEndpoint = null;
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unable to connect to registry endpoint...\n");
                    return 1;
                }
            }
            else
            {
                return 1; //the url is something other than http or https
            }
        }

        public bool ConnectathonTest(string url)
        {
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return false; };
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 5000;

                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    //  well, at least it returned...
                    return false;
                }
            }
            catch (Exception ex)
            {
                //  not available at all, for some reason
                return false;
            }
        }

        private bool testConnection(string url)
        {
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return false; };
                var myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = 5000;

                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    //  well, at least it returned...
                    return false;
                }
            }
            catch (Exception ex)
            {
                //  not available at all, for some reason
                return false;
            }
        }
        
        /*
        private bool AtnaTest_AuditMessageGenerated(XdsObjects.XML_InnerObjects.AuditMessage message)
        {
            byte[] payLoad;
            using (MemoryStream ms = new MemoryStream())
            {
                XdsObjectSerializer.Serialize(message, ms);
                payLoad = ms.ToArray();
            }

            return true; // don't auto-send, i will send later
            //throw new NotImplementedException();
        }*/

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
                myATNA.ATNA_Application_Stop(atnaTest);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Application Stop audit event logged...");
                atnaTest.AuditRepositories.Clear();
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

            myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, true);
            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");

            XdsRegistryResponse myResponse = new XdsRegistryResponse();
            bool errors = false;
            try
            {
                //the incoming submission is rejected outright if error encountered here
                //How many ExtrinsicDocument objects are there?
                int docCount = SubmissionSet.Documents.Count;
                if (docCount == 0) //return failure if there are none
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": No document metadata present - ");
                    myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocumentMetadata, "No document metadata present", "");
                    return myResponse;
                }
                else
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": No of documents in submission - " + docCount);
                }

                //the incoming submission is rejected outright if error encountered here
                //for each ExtrinsicDocument object, is the actual base64 encoded document present?
                foreach (XdsDocument document in SubmissionSet.Documents)
                {
                    if(document.Data == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Missing document...");
                        myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSMissingDocument, "Missing document", "");
                        return myResponse;
                    }
                }

                //check that Authority Domain of patient matches with that of Repository
                if (!SubmissionSet.PatientInfo.ID_Root.Contains(authDomain)) // != authDomain)
                {
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Authority Domains do not match...");
                    myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                    myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                    myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownCommunity, "Authority Domains do not match", "");
                    errors = true;
                }

                docCount = 0;
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet contains " + SubmissionSet.Documents.Count + " documents...");
                foreach (XdsDocument document in SubmissionSet.Documents)
                {
                    docCount++;
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Patient Id - " + document.PatientID + "...");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ") UniqueId - " + document.UniqueID + "...");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document(" + docCount + ").UUID - " + document.UUID + "...");
                    //LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet.UUID - " + SubmissionSet.UUID + "...");

                    document.RepositoryUniqueId = repositoryId;
                    //use for pre-connectathon test 11966
                    //document.Size = 36;
                    //document.Hash = "e543712c0e10501972de13a5bfcbe826c49feb75";
                    document.SetSizeAndHash();

                    bool HashSizeCheck = document.CheckSizeAndHash();
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document size - " + document.Size);
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document hash - " + document.Hash);
                    if (!HashSizeCheck)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": XDSRepositoryMetadataError - Hash and/or Size atrributes of supplied document are in error...");
                        myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                        myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        myResponse.AddError(XdsObjects.Enums.XdsErrorCode.XDSRepositoryMetadataError, "Hash and/or Size atrributes of supplied document are in error", "");
                        errors = true;
                    }
                }


                using (var db = new RepositoryDataBase())
                {
                    //docCount = 0;
                    //LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": SubmissionSet contains " + SubmissionSet.Documents.Count + " documents...");
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
                        //System.IO.File.WriteAllText(StoragePath + document.UniqueID + ".mime", document.MimeType);
                        //Save document info into repository database
                        db.Documents.Add(new Document()
                        {
                            DocUUID = document.UUID,
                            DocumentId = document.UniqueID,
                            MimeType = document.MimeType,
                            //application/octet-stream
                            //MimeType = "application/octet-stream",
                            Location = location,
                            DocDateTime = document.CreationTime,
                            DocSize = document.Size
                        });
                        if (errors == true)
                        {
                            foreach(var e in myResponse.Errors.ErrorList)
                            {
                                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error in Submission - " + e.ErrorCode);
                            }
                            //LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Errors in document and/or submission...");
                            myATNA.Repository_Export(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event logged...");
                            return myResponse;
                        }
                        else
                        {
                            // then send whole lot off to the chosen registry (main data will NOT be sent - this is
                            // automatic and internal!)  
                            //Then pass back the response
                            db.SaveChanges();
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Document details saved in db...");
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Sending SubmissionSet to Registry...");
                            myATNA.Repository_Export(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, true);
                            LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Register Document Set Export audit event logged...");
                            /////////////////////////////////////////////////
                            //xds.RegistryEndpoint.Security.CheckHostName = true;
                            //xds.RegistryEndpoint.Security.CheckCRL = true;
                            /////////////////////////////////////////////////
                            //return xds.RegisterDocumentSet(SubmissionSet);
                        }   
                    }
                    //return null;
                }
                return xds.RegisterDocumentSet(SubmissionSet);
            }
            catch(SocketException socEx)
            {
                myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unable to connect to the Registry to register document");
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsErrorCode.XDSRegistryNotAvailable, "Unable to connect to the Registry to register document", socEx.Message);
                return myResponse;
            }
            catch (Exception ex)
            {
                myATNA.Repository_Import(atnaTest, SubmissionSet.PatientID, SubmissionSet.UniqueID, SubmissionSet.SourceID, false);
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": ProvideAndRegister Import audit event logged...");
                LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Error - " + ex.Message + " - " + ex.InnerException.ToString());
                myResponse.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                myResponse.AddError(XdsObjects.Enums.XdsErrorCode.GeneralException, ex.Message, ex.InnerException.ToString());
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
                XdsPatient myPatient = new XdsPatient();
                XdsRegistryResponse myResponse = new XdsRegistryResponse();
                WebServiceEndpoint myRepositoryEndPoint = new WebServiceEndpoint(repositoryURI);
                
                foreach (XdsRetrieveItem item in Request.Requests)
                {
                    
                    if (item.RepositoryUniqueID == null)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Id is not present - ");
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsObjects.Enums.XdsErrorCode.XDSUnknownRepositoryId, "", "");
                        myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive Document Set Export audit event logged...");
                        return response;
                    }
                    // check that the stated repository OID really is us (this is an example of how to return an error)
                    else if (item.RepositoryUniqueID != repositoryId)
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Unknown Repository Id - " + item.RepositoryUniqueID + "...");
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsErrorCode.XDSUnknownRepositoryId, "", item.RepositoryUniqueID);
                        myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive Document Set Export audit event logged...");
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
                    string mimeType = "";
                    using (var dbRep = new RepositoryDataBase())
                    {
                        foreach (var doc in dbRep.Documents)
                        {
                            docId = doc.DocumentId;
                            if (docId == item.DocumentUniqueID)
                            {
                                location = doc.Location;
                                mimeType = doc.MimeType; //this was inserted to pass pre-connectathon test 12345
                                if (!File.Exists(location))
                                {
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cannot locate - " + location + "...");
                                    response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                                    response.AddError(XdsErrorCode.XDSMissingDocument, "", item.DocumentUniqueID);
                                    myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive Document Set Export audit event logged...");
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
                    if(location == "")
                    {
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Cannot locate - " + location + "...");
                        response.Status = XdsObjects.Enums.RegistryResponseStatus.Failure;
                        response.AddError(XdsErrorCode.XDSMissingDocument, "", item.DocumentUniqueID);
                        myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, false);
                        LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive Document Set Export audit event logged...");
                        return response;
                    }
                    XdsDocument document = new XdsDocument(location);
                    document.UniqueID = item.DocumentUniqueID;
                    document.MimeType = mimeType; //this was inserted to pass pre-connectathon test 12345
                    //document.HomeCommunityID = item.HomeCommunityID;
                    document.RepositoryUniqueId = item.RepositoryUniqueID;

                    myATNA.Repository_Retrieve(atnaTest, item.DocumentUniqueID, true);
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Retreive Document Set Export audit event logged...");
                    LogMessageEvent(DateTime.Now.ToString("HH:mm:ss.fff") + ": Returning requested document for " + item.DocumentUniqueID);
                    response.Documents.Add(document);
                }
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
