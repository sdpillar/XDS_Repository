using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XdsObjects;
using XdsObjects.Enums;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace XdsRepository
{
    class ATNA_Messaging
    {
        /*
        private string getIpAddress()
        {
            // get local IP addresses
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

            return localIPs[2].ToString();
        }
        */

        private string getIpAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        public void ATNA_Application_Start(XdsDomain atnaDomain)
        {
            try
            {
                XdsObjects.XML_InnerObjects.AuditMessage appStart = new XdsObjects.XML_InnerObjects.AuditMessage();
                //EventIdentification
                appStart.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success;
                appStart.EventIdentification.EventActionCode = XdsObjects.XML_InnerObjects.Enums.EventActionCode.E;
                appStart.EventIdentification.EventDateTime = DateTime.Now; ;
                appStart.EventIdentification.EventID = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application Activity", "110100", atnaDomain.AuditSchema);
                appStart.EventIdentification.EventTypeCode = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application Start", "110120", atnaDomain.AuditSchema);
                //ActiveParticipant1
                XdsObjects.XML_InnerObjects.ActiveParticipant myParticipant1 = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                myParticipant1.UserID = atnaDomain.AuditSourceAddress;
                myParticipant1.UserIsRequestor = true;
                myParticipant1.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.IPAddress;
                myParticipant1.NetworkAccessPointID = getIpAddress();
                XdsObjects.XML_InnerObjects.CodedValueString myRole1 = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application", "110150", atnaDomain.AuditSchema);
                myParticipant1.RoleIDCodes.Add(myRole1);
                appStart.ActiveParticipants.Add(myParticipant1);
                //ActiveParticipant2
                XdsObjects.XML_InnerObjects.ActiveParticipant myParticipant2 = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                myParticipant2.UserID = atnaDomain.AuditSourceAddress;
                myParticipant2.UserIsRequestor = true;
                myParticipant2.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.IPAddress;
                myParticipant2.NetworkAccessPointID = getIpAddress();
                XdsObjects.XML_InnerObjects.CodedValueString myRole2 = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application Launcher", "110151", atnaDomain.AuditSchema);
                myParticipant2.RoleIDCodes.Add(myRole2);
                appStart.ActiveParticipants.Add(myParticipant2);
                //AuditSourceIdentification
                XdsObjects.XML_InnerObjects.AuditSourceIdentification myAuditSource = new XdsObjects.XML_InnerObjects.AuditSourceIdentification(atnaDomain.AuditSourceID);
                myAuditSource.AuditEnterpriseSiteID = atnaDomain.AuditEnterpriseSiteID;
                myAuditSource.Code = "4";
                appStart.AuditSourceIdentification = myAuditSource;
                atnaDomain.SendAuditMessage(appStart);
            }
            catch(Exception ex)
            {
                throw new Exception("ATNA_Application_Start - " + ex.Message);
            }
        }

        public void ATNA_Application_Stop(XdsDomain atnaDomain)
        {
            try
            {
                XdsObjects.XML_InnerObjects.AuditMessage appStop = new XdsObjects.XML_InnerObjects.AuditMessage();
                //EventIdentification
                appStop.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success;
                appStop.EventIdentification.EventActionCode = XdsObjects.XML_InnerObjects.Enums.EventActionCode.E;
                appStop.EventIdentification.EventDateTime = DateTime.Now; ;
                appStop.EventIdentification.EventID = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application Activity", "110100", atnaDomain.AuditSchema);
                appStop.EventIdentification.EventTypeCode = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application Stop", "110121", atnaDomain.AuditSchema);
                //ActiveParticipant1
                XdsObjects.XML_InnerObjects.ActiveParticipant myParticipant1 = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                myParticipant1.UserID = atnaDomain.AuditSourceAddress;
                myParticipant1.UserIsRequestor = true;
                myParticipant1.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.IPAddress;
                myParticipant1.NetworkAccessPointID = getIpAddress();
                XdsObjects.XML_InnerObjects.CodedValueString myRole1 = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Application", "110150", atnaDomain.AuditSchema);
                myParticipant1.RoleIDCodes.Add(myRole1);
                appStop.ActiveParticipants.Add(myParticipant1);
                //AuditSourceIdentification
                XdsObjects.XML_InnerObjects.AuditSourceIdentification myAuditSource = new XdsObjects.XML_InnerObjects.AuditSourceIdentification(atnaDomain.AuditSourceID);
                myAuditSource.AuditEnterpriseSiteID = atnaDomain.AuditEnterpriseSiteID;
                myAuditSource.Code = "4";
                appStop.AuditSourceIdentification = myAuditSource;
                atnaDomain.SendAuditMessage(appStop);
            }
            catch (Exception ex)
            {
                throw new Exception("ATNA_Application_Stop - " + ex.Message);
            }
        }

        public void Repository_Import(XdsDomain atnaDomain, string patientId, string uniqueId, string sourceId, bool success)
        {
            try
            {
                XdsObjects.XML_InnerObjects.AuditMessage repImport = new XdsObjects.XML_InnerObjects.AuditMessage();
                if (success == true)
                {
                    repImport.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success; //dependent on presence of errors in Repository?
                }
                else
                {
                    repImport.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.MajorFailure; //dependent on presence of errors in Repository?
                }
                repImport.EventIdentification.EventActionCode = XdsObjects.XML_InnerObjects.Enums.EventActionCode.C;
                repImport.EventIdentification.EventID = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Import", "110107", atnaDomain.AuditSchema);
                repImport.EventIdentification.EventTypeCode = new XdsObjects.XML_InnerObjects.CodedValueString("IHE Transactions", "Provide and Register Document Set-b", "ITI-41", atnaDomain.AuditSchema);
                //Source
                XdsObjects.XML_InnerObjects.ActiveParticipant source = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                source.UserID = "";
                source.UserIsRequestor = true;
                string ipAddress = getIpAddress();
                source.NetworkAccessPointID = sourceId;
                source.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString sourceRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Source", "110153", atnaDomain.AuditSchema);
                source.RoleIDCodes.Add(sourceRole);
                repImport.ActiveParticipants.Add(source);
                //Destination
                XdsObjects.XML_InnerObjects.ActiveParticipant destination = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                destination.UserID = atnaDomain.SubmissionRepositoryEndpoint.ToString();
                destination.AlternativeUserID = atnaDomain.SubmissionRepositoryEndpoint.ToString();
                destination.UserIsRequestor = false;
                ipAddress = getIpAddress();
                destination.NetworkAccessPointID = sourceId;
                destination.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString destinationRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Destination", "110152", atnaDomain.AuditSchema);
                destination.RoleIDCodes.Add(destinationRole);
                repImport.ActiveParticipants.Add(destination);
                //Patient
                XdsObjects.XML_InnerObjects.ParticipantObjectIdentification patient = new XdsObjects.XML_InnerObjects.ParticipantObjectIdentification();
                patient.ParticipantObjectTypeCode = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCode.Person;
                patient.ParticipantObjectTypeCodeRole = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCodeRole.Patient;
                patient.ParticipantObjectID = patientId;
                patient.ParticipantObjectIDTypeCode = new XdsObjects.XML_InnerObjects.CodedValueParticipantObjectIDType("RFC-3881", "Patient Number", XdsObjects.XML_InnerObjects.Enums.ParticipantObjectIDTypeCode.Patient_Number, atnaDomain.AuditSchema);
                repImport.ParticipantObjectIdentification.Add(patient);
                //SubmissionSet
                XdsObjects.XML_InnerObjects.ParticipantObjectIdentification submissionSet = new XdsObjects.XML_InnerObjects.ParticipantObjectIdentification();
                submissionSet.ParticipantObjectTypeCode = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCode.System_Object;
                submissionSet.ParticipantObjectTypeCodeRole = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCodeRole.Job;
                submissionSet.ParticipantObjectID = uniqueId;
                submissionSet.ParticipantObjectIDTypeCode = new XdsObjects.XML_InnerObjects.CodedValueParticipantObjectIDType("IHE XDS Metadata", "submission set classificationNode", XdsObjects.XML_InnerObjects.Enums.ParticipantObjectIDTypeCode.IHE_XDS_Metadata, atnaDomain.AuditSchema);
                repImport.ParticipantObjectIdentification.Add(submissionSet);
                //AuditSourceIdentification
                XdsObjects.XML_InnerObjects.AuditSourceIdentification myAuditSource = new XdsObjects.XML_InnerObjects.AuditSourceIdentification(atnaDomain.AuditSourceID);
                myAuditSource.AuditEnterpriseSiteID = atnaDomain.AuditEnterpriseSiteID;
                myAuditSource.Code = "4";
                repImport.AuditSourceIdentification = myAuditSource;
                atnaDomain.SendAuditMessage(repImport);
            }
            catch (Exception ex)
            {
                throw new Exception("Repository_Import - " + ex.Message);
            }
        }

        public void Repository_Export(XdsDomain atnaDomain, string patientId, string uniqueId, string sourceId, bool success)
        {
            try
            {
                XdsObjects.XML_InnerObjects.AuditMessage repExport = new XdsObjects.XML_InnerObjects.AuditMessage();
                if (success == true)
                {
                    repExport.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success; //dependent on presence of errors in Repository?
                }
                else
                {
                    repExport.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.MajorFailure; //dependent on presence of errors in Repository?
                }
                repExport.EventIdentification.EventActionCode = XdsObjects.XML_InnerObjects.Enums.EventActionCode.R;
                repExport.EventIdentification.EventID = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Export", "110106", atnaDomain.AuditSchema);
                repExport.EventIdentification.EventTypeCode = new XdsObjects.XML_InnerObjects.CodedValueString("IHE Transactions", "Register Document Set-b", "ITI-42", atnaDomain.AuditSchema);
                //Source
                XdsObjects.XML_InnerObjects.ActiveParticipant source = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                source.UserID = atnaDomain.RegistryEndpoint.ToString();
                source.AlternativeUserID = atnaDomain.RegistryEndpoint.ToString();
                source.UserIsRequestor = true;
                source.NetworkAccessPointID = atnaDomain.RegistryEndpoint.ToString();
                source.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString sourceRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Source", "110153", atnaDomain.AuditSchema);
                source.RoleIDCodes.Add(sourceRole);
                repExport.ActiveParticipants.Add(source);
                //Destination
                XdsObjects.XML_InnerObjects.ActiveParticipant destination = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                destination.UserID = atnaDomain.RegistryEndpoint.ToString();
                destination.AlternativeUserID = atnaDomain.RegistryEndpoint.ToString();
                destination.UserIsRequestor = false;
                destination.NetworkAccessPointID = atnaDomain.RegistryEndpoint.ToString();
                destination.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString destinationRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Destination", "110152", atnaDomain.AuditSchema);
                destination.RoleIDCodes.Add(destinationRole);
                repExport.ActiveParticipants.Add(destination);
                //Patient
                XdsObjects.XML_InnerObjects.ParticipantObjectIdentification patient = new XdsObjects.XML_InnerObjects.ParticipantObjectIdentification();
                patient.ParticipantObjectTypeCode = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCode.Person;
                patient.ParticipantObjectTypeCodeRole = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCodeRole.Patient;
                patient.ParticipantObjectID = patientId;
                patient.ParticipantObjectIDTypeCode = new XdsObjects.XML_InnerObjects.CodedValueParticipantObjectIDType("RFC-3881", "Patient Number", XdsObjects.XML_InnerObjects.Enums.ParticipantObjectIDTypeCode.Patient_Number, atnaDomain.AuditSchema);
                repExport.ParticipantObjectIdentification.Add(patient);
                //SubmissionSet
                XdsObjects.XML_InnerObjects.ParticipantObjectIdentification submissionSet = new XdsObjects.XML_InnerObjects.ParticipantObjectIdentification();
                submissionSet.ParticipantObjectTypeCode = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCode.System_Object;
                submissionSet.ParticipantObjectTypeCodeRole = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCodeRole.Job;
                submissionSet.ParticipantObjectID = uniqueId;
                submissionSet.ParticipantObjectIDTypeCode = new XdsObjects.XML_InnerObjects.CodedValueParticipantObjectIDType("IHE XDS Metadata", "submission set classificationNode", XdsObjects.XML_InnerObjects.Enums.ParticipantObjectIDTypeCode.IHE_XDS_Metadata, atnaDomain.AuditSchema);
                repExport.ParticipantObjectIdentification.Add(submissionSet);
                //AuditSourceIdentification
                XdsObjects.XML_InnerObjects.AuditSourceIdentification myAuditSource = new XdsObjects.XML_InnerObjects.AuditSourceIdentification(atnaDomain.AuditSourceID);
                myAuditSource.AuditEnterpriseSiteID = atnaDomain.AuditEnterpriseSiteID;
                myAuditSource.Code = "4";
                repExport.AuditSourceIdentification = myAuditSource;
                atnaDomain.SendAuditMessage(repExport);
            }
            catch (Exception ex)
            {
                throw new Exception("Repository_Export - " + ex.Message);
            }
        }

        public void Repository_Retrieve(XdsDomain atnaDomain, string documentUniqueId, bool success)
        {
            try
            {
                XdsObjects.XML_InnerObjects.AuditMessage repRetrieve = new XdsObjects.XML_InnerObjects.AuditMessage();
                if (success == true)
                {
                    repRetrieve.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success; //dependent on presence of errors in Repository?
                }
                else
                {
                    repRetrieve.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.MajorFailure; //dependent on presence of errors in Repository?
                }
                repRetrieve.EventIdentification.EventOutcomeIndicator = EventOutcomeIndicator.Success; //dependent on presence of errors in Repository?
                repRetrieve.EventIdentification.EventActionCode = XdsObjects.XML_InnerObjects.Enums.EventActionCode.R;
                repRetrieve.EventIdentification.EventID = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Export", "110106", atnaDomain.AuditSchema);
                repRetrieve.EventIdentification.EventTypeCode = new XdsObjects.XML_InnerObjects.CodedValueString("IHE Transactions", "Retrieve Document Set", "ITI-43", atnaDomain.AuditSchema);
                //Source
                XdsObjects.XML_InnerObjects.ActiveParticipant source = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                source.UserID = atnaDomain.SubmissionRepositoryEndpoint.ToString();
                source.AlternativeUserID = atnaDomain.SubmissionRepositoryEndpoint.ToString();
                source.UserIsRequestor = false;
                source.NetworkAccessPointID = atnaDomain.AuditSourceID;
                source.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString sourceRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Source", "110153", atnaDomain.AuditSchema);
                source.RoleIDCodes.Add(sourceRole);
                repRetrieve.ActiveParticipants.Add(source);
                //Destination
                XdsObjects.XML_InnerObjects.ActiveParticipant destination = new XdsObjects.XML_InnerObjects.ActiveParticipant();
                destination.UserID = "";
                destination.AlternativeUserID = "";
                destination.UserIsRequestor = true;
                destination.NetworkAccessPointID = atnaDomain.RegistryEndpoint.ToString();
                destination.NetworkAccessPointTypeCode = XdsObjects.XML_InnerObjects.Enums.NetworkAccessPointTypeCode.MachineNameIncludingDNSName;
                XdsObjects.XML_InnerObjects.CodedValueString destinationRole = new XdsObjects.XML_InnerObjects.CodedValueString("DCM", "Destination", "110152", atnaDomain.AuditSchema);
                destination.RoleIDCodes.Add(destinationRole);
                repRetrieve.ActiveParticipants.Add(destination);
                //Document URI
                XdsObjects.XML_InnerObjects.ParticipantObjectIdentification document = new XdsObjects.XML_InnerObjects.ParticipantObjectIdentification();
                document.ParticipantObjectTypeCode = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCode.System_Object;
                document.ParticipantObjectTypeCodeRole = XdsObjects.XML_InnerObjects.Enums.ParticipantObjectTypeCodeRole.Report;
                document.ParticipantObjectIDTypeCode = new XdsObjects.XML_InnerObjects.CodedValueParticipantObjectIDType("RFC-3881", "Report Number", XdsObjects.XML_InnerObjects.Enums.ParticipantObjectIDTypeCode.Report_Number, atnaDomain.AuditSchema);
                document.ParticipantObjectID = documentUniqueId;
                XdsObjects.XML_InnerObjects.TypeValuePair objectDetail = new XdsObjects.XML_InnerObjects.TypeValuePair("Repository Unique Id", atnaDomain.AuditSourceAddress);
                document.ParticipantObjectDetail.Add(objectDetail);
                repRetrieve.ParticipantObjectIdentification.Add(document);
                //AuditSourceIdentification
                XdsObjects.XML_InnerObjects.AuditSourceIdentification myAuditSource = new XdsObjects.XML_InnerObjects.AuditSourceIdentification(atnaDomain.AuditSourceID);
                myAuditSource.AuditEnterpriseSiteID = atnaDomain.AuditEnterpriseSiteID;
                myAuditSource.Code = "4";
                repRetrieve.AuditSourceIdentification = myAuditSource;
                atnaDomain.SendAuditMessage(repRetrieve);
            }
            catch (Exception ex)
            {
                throw new Exception("Repository_Retrieve - " + ex.Message);
            }
        }
    }
}
