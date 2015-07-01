using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;

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
            Properties.Settings.Default.HashCode = CalcuateHash();
            Properties.Settings.Default.Save();
            this.Location = this.Owner.Location;
            this.Left += this.Owner.ClientSize.Width / 2 - this.Width / 2;
            this.Top += this.Owner.ClientSize.Height / 2 - this.Height / 2;
            cmdSaveSettings.Enabled = false;
            this.txtDomain.SelectionStart = 0;
            this.txtDomain.SelectionLength = 0;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            if (cmdSaveSettings.Enabled == true)
            {
                MessageBox.Show("Save settings before exiting!");
            }
            else
            {
                this.Close();
            }
        }

        private int CalcuateHash()
        {
            string[] txtStrings = new string[] { txtDomain.Text, txtRegistryURI.Text, txtRepositoryPath.Text, txtRepositoryId.Text, txtRepositoryURI.Text, txtRepositoryLog.Text, txtAtnaHost.Text, txtAtnaPort.Text };
            string hashString = String.Concat(txtStrings);
            int hash = hashString.GetHashCode();
            return hash;
        }

        private void txtAtnaPort_TextChanged(object sender, EventArgs e)
        {
            string portString = txtAtnaPort.Text;
            if (portString.Length > 4)
            {
                txtAtnaPort.Text = portString.Substring(0, 4);
                MessageBox.Show("Port number cannot be more than 4 digits");
            }

            for (int i = 0; i < portString.Length; i++)
            {
                if (!char.IsNumber(portString[i]))
                {
                    MessageBox.Show("Please insert a valid number");
                }
            }
        }

        private void HashChanged()
        {
            int currentHash = CalcuateHash();
            if (currentHash != Properties.Settings.Default.HashCode)
            {
                currentHashValue = CalcuateHash();
                cmdSaveSettings.Enabled = true;
            }
        }

        private void txtRepositoryPath_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtRepositoryId_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtRepositoryURI_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtRepositoryLog_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtDomain_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtRegistryURI_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtAtnaHost_Leave(object sender, EventArgs e)
        {
            HashChanged();
        }

        private void txtAtnaPort_Leave(object sender, EventArgs e)
        {
            HashChanged();
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

        private void cmdSaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AuthDomain = txtDomain.Text;
            Properties.Settings.Default.RegistryURI = txtRegistryURI.Text;
            Properties.Settings.Default.RepositoryPath = txtRepositoryPath.Text;
            Properties.Settings.Default.RepositoryId = txtRepositoryId.Text;
            Properties.Settings.Default.RepositoryURI = txtRepositoryURI.Text;
            Properties.Settings.Default.RepositoryLog = txtRepositoryLog.Text;
            Properties.Settings.Default.AtnaHost = txtAtnaHost.Text;
            Properties.Settings.Default.AtnaPort = int.Parse(txtAtnaPort.Text);
            Properties.Settings.Default.HashCode = CalcuateHash();
            Properties.Settings.Default.Save();
            cmdSaveSettings.Enabled = false;
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
    }
}
