using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
//using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace XdsRepository
{
    public partial class frmSettings : Form
    {
        int currentHashValue;

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtDomain.Text = Properties.Settings.Default.AuthDomain;
            txtRegistryURI.Text = Properties.Settings.Default.RegistryURI;
            txtRepositoryPath.Text = Properties.Settings.Default.RepositoryPath;
            txtRepositoryId.Text = Properties.Settings.Default.RepositoryId;
            txtRepositoryURI.Text = Properties.Settings.Default.RepositoryURI;
            txtRepositoryLog.Text = Properties.Settings.Default.RepositoryLog;
            txtAtnaHost.Text = Properties.Settings.Default.AtnaHost;
            txtAtnaPort.Text = Properties.Settings.Default.AtnaPort.ToString();
            txtThumbprint.Text = Properties.Settings.Default.Thumbprint;
            txtRoot.Text = Properties.Settings.Default.RootCertificate;
            txtServer.Text = Properties.Settings.Default.ServerCertificate;
            txtPassword.Text = Properties.Settings.Default.ServerCertPassword;
            ttpSettings.SetToolTip(txtThumbprint, Properties.Settings.Default.Thumbprint);
            txtAppId.Text = GetAppId();
            ttpSettings.SetToolTip(txtAppId, GetAppId());
            //Properties.Settings.Default.HashCode = CalcuateHash();
            Properties.Settings.Default.Save();
            this.Location = this.Owner.Location;
            this.Left += this.Owner.ClientSize.Width / 2 - this.Width / 2;
            this.Top += this.Owner.ClientSize.Height / 2 - this.Height / 2;
            this.txtDomain.SelectionStart = 0;
            this.txtDomain.SelectionLength = 0;

            tvwSettings.ExpandAll();
            tvwSettings.SelectedNode = tvwSettings.Nodes[0].Nodes[0];

            try
            {
                string line = "";
                StreamReader certsFile = new StreamReader(@"C:\HSS\XDS_Repository\certs.txt");
                while ((line = certsFile.ReadLine()) != null)
                {
                    cmbCertificates.Items.Add(line);
                }
                certsFile.Close();

            }
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message, "Certificate File Exception");
            }
        }

        private void tvwSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Console.WriteLine(e.Node.Text);
            if (e.Node.Text != "Settings")
            {
                for (int i = 0; i < tvwSettings.Nodes[0].Nodes.Count; i++)
                {
                    tvwSettings.Nodes[0].Nodes[i].ForeColor = Color.SlateGray;
                }


                tvwSettings.SelectedNode = e.Node;
                e.Node.ForeColor = Color.Black;
                //lblRegistry.Text = e.Node.Text;

                switch (e.Node.Text)
                {
                    case "Repository":
                        grpRepository.Visible = true;
                        lblRepository.Text = e.Node.Text;
                        grpRegistry.Visible = false;
                        grpAtna.Visible = false;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = false;
                        grpDomain.Visible = false;
                        grpLogging.Visible = false;
                        break;
                    case "Registry":
                        grpRepository.Visible = false;
                        grpRegistry.Visible = true;
                        lblRegistry.Text = e.Node.Text;
                        grpAtna.Visible = false;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = false;
                        grpDomain.Visible = false;
                        grpLogging.Visible = false;
                        break;
                    case "ATNA":
                        grpRepository.Visible = false;
                        grpRegistry.Visible = false;
                        grpAtna.Visible = true;
                        lblAtna.Text = e.Node.Text;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = false;
                        grpDomain.Visible = false;
                        grpLogging.Visible = false;
                        break;
                    case "Certificates":
                        grpRepository.Visible = false;
                        grpRegistry.Visible = false;
                        grpAtna.Visible = false;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = true;
                        lblBouncyCastle.Text = e.Node.Text;
                        grpDomain.Visible = false;
                        grpLogging.Visible = false;
                        break;
                    case "Authority Domain":
                        grpRepository.Visible = false;
                        grpRegistry.Visible = false;
                        grpAtna.Visible = false;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = false;
                        grpDomain.Visible = true;
                        lblDomain.Text = e.Node.Text;
                        grpLogging.Visible = false;
                        break;
                    case "Logging":
                        grpRepository.Visible = false;
                        grpRegistry.Visible = false;
                        grpAtna.Visible = false;
                        grpCertificates.Visible = false;
                        grpCertsBC.Visible = false;
                        grpDomain.Visible = false;
                        grpLogging.Visible = true;
                        lblLogging.Text = e.Node.Text;
                        break;
                }
            }
        }

        private string GetAppId()
        {
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(GuidAttribute), false);
            if (attributes.Length == 0) { return String.Empty; }
            return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value.ToUpper();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            int currentHash = CalcuateHash();
            if (currentHash != Properties.Settings.Default.HashCode)
            {
                Properties.Settings.Default.AuthDomain = txtDomain.Text;
                Properties.Settings.Default.RegistryURI = txtRegistryURI.Text;
                Properties.Settings.Default.RepositoryPath = txtRepositoryPath.Text;
                Properties.Settings.Default.RepositoryId = txtRepositoryId.Text;
                Properties.Settings.Default.RepositoryURI = txtRepositoryURI.Text;
                Properties.Settings.Default.RepositoryLog = txtRepositoryLog.Text;
                Properties.Settings.Default.AtnaHost = txtAtnaHost.Text;
                Properties.Settings.Default.AtnaPort = int.Parse(txtAtnaPort.Text);
                Properties.Settings.Default.Thumbprint = txtThumbprint.Text;
                Properties.Settings.Default.RootCertificate = txtRoot.Text;
                Properties.Settings.Default.ServerCertificate = txtServer.Text;
                Properties.Settings.Default.ServerCertPassword = txtPassword.Text;
                Properties.Settings.Default.HashCode = CalcuateHash();
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
        
        private int CalcuateHash()
        {
            string[] txtStrings = new string[] { txtDomain.Text, txtRegistryURI.Text, txtRepositoryPath.Text, txtRepositoryId.Text, txtRepositoryURI.Text, txtRepositoryLog.Text, txtAtnaHost.Text, txtAtnaPort.Text, txtThumbprint.Text, txtRoot.Text, txtServer.Text, txtPassword.Text };
            string hashString = String.Concat(txtStrings);
            int hash = hashString.GetHashCode();
            return hash;
        }

        private void txtAtnaPort_Leave(object sender, EventArgs e)
        {
            try
            {
                string endpoint = txtAtnaPort.Text;
                string[] endpointSplit = endpoint.Split(':');
                string ipAddress = endpointSplit[0];
                string port = endpointSplit[1];
                bool ipAddressCheck = true;

                if (ipAddress != "localhost")
                {
                    IPAddress clientIpAddr;
                    ipAddressCheck = IPAddress.TryParse(ipAddress, out clientIpAddr);

                    if (ipAddressCheck == false)
                    {
                        MessageBox.Show("IP Address entered is not a valid IP Address...");
                    }
                }
                bool portCheck = IsValidPort(port);
                if (portCheck == false)
                {
                    MessageBox.Show("Port entered is not a port...");
                }
                if (ipAddressCheck == true && portCheck == true)
                {
                    testNotifyEndpoint(ipAddress, port);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ip Address and/or Port is invalid");
            }
        }

        private void testNotifyEndpoint(string host, string port)
        {
            bool testNotifyEndpoint = testConnection(host, int.Parse(port));
            if (testNotifyEndpoint == true)
            {
                MessageBox.Show("Connectivity established to " + host + ":" + port);
            }
            else
            {
                MessageBox.Show("Failed to connect to " + host + ":" + port);
            }
        }

        private bool testConnection(string host, int port)
        {
            string textToSend = DateTime.Now.ToString();
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(host, port);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message;
                client.Close();
                return false;
            }
        }

        private bool IsValidPort(string port)
        {
            string pattern = @"^[0-9][0-9][0-9][0-9]$";
            bool valid = false;
            Regex check = new Regex(pattern);
            if (port == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(port, 0);
            }
            return valid;
        }

        private void cmdLog_Click(object sender, EventArgs e)
        {
            dlgLog.SelectedPath = txtRepositoryLog.Text;
            dlgLog.Description = "Select the folder in which to store repository logs";
            if (dlgLog.ShowDialog() == DialogResult.OK)
            {
                txtRepositoryLog.Text = dlgLog.SelectedPath;
            }
        }

        private void cmdRepository_Click(object sender, EventArgs e)
        {
            dlgLog.SelectedPath = txtRepositoryPath.Text;
            dlgLog.Description = "Select the folder in which to store reports and scanned documents";
            if (dlgLog.ShowDialog() == DialogResult.OK)
            {
                txtRepositoryPath.Text = dlgLog.SelectedPath;
            }
        }

        private void cmdVersions_Click(object sender, EventArgs e)
        {
            // Get the file version for XdsObjects
            FileVersionInfo xdsObjectsVersionInfo =
                FileVersionInfo.GetVersionInfo(@"C:\HSS\XDS_Repository\XdsObjects.6.40.dll");
            string xdsObjects = xdsObjectsVersionInfo.ProductVersion;

            // Get the file version for XdsObjectsSerializer
            FileVersionInfo xdsObjectsSerilaizerVersionInfo =
                FileVersionInfo.GetVersionInfo(@"C:\HSS\XDS_Repository\XdsObjects.6.40.XmlSerializers.dll");
            string xdsObjectsSerializer = xdsObjectsSerilaizerVersionInfo.ProductVersion;

            // Get the file version for Security
            FileVersionInfo securityVersionInfo =
                FileVersionInfo.GetVersionInfo(@"C:\HSS\XDS_Repository\Security.6.40.dll");
            string security = securityVersionInfo.ProductVersion;

            // Get the file version for BouncyCastlesdvsvs
            FileVersionInfo bouncyCastleVersionInfo =
                FileVersionInfo.GetVersionInfo(@"C:\HSS\XDS_Repository\BouncyCastle.6.40.dll");
            string bouncyCastle = bouncyCastleVersionInfo.ProductVersion;

            MessageBox.Show("XdsObjects - " + xdsObjects + "\nXdsObjectsSerializer - " + xdsObjectsSerializer + "\nSecurity - " + security + "\nBouncy Castle - " + bouncyCastle,"Versions");
        }

        private void cmdRoot_Click(object sender, EventArgs e)
        {
            string filename = "";
            string path = "";
            DialogResult result = openCertificate.ShowDialog();
            if (result == DialogResult.OK)
            {
                filename = Path.GetFileName(openCertificate.FileName);
                path = Path.GetDirectoryName(openCertificate.FileName);
            }
            txtRoot.Text = path + "\\" + filename;
        }

        private void cmdServer_Click(object sender, EventArgs e)
        {
            string filename = "";
            string path = "";
            DialogResult result = openCertificate.ShowDialog();
            if (result == DialogResult.OK)
            {
                filename = Path.GetFileName(openCertificate.FileName);
                path = Path.GetDirectoryName(openCertificate.FileName);
            }
            txtServer.Text = path + "\\" + filename;
        }
    }
}
