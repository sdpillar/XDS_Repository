using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using XdsObjects;
using XdsObjects.Enums;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Tls;
using System.ServiceModel;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;
using System.Text;

namespace XdsRepository
{
    public partial class RepositoryForm : Form
    {
        Repository Rep = new Repository();
        Repository.LogMessageHandler LogMessageHandler;
        DateTime currentDate = DateTime.Now;
        
        public RepositoryForm()
        {
            InitializeComponent();
            LogMessageHandler = new Repository.LogMessageHandler(logMessageHandler);
            this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository";
            SetupProperties();
        }

        private void SetupProperties()
        {
            Rep.LogMessageEvent -= LogMessageHandler;
            bool readProperties = Rep.readProperties();
            if (readProperties == false)
            {
                MessageBox.Show("Error in reading properties...", "Error");
            }
            else
            {
                Rep.LogMessageEvent += LogMessageHandler;
                Rep.StartListen();
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Authority Domain - " + Rep.authDomain + "...\n"));
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Store - " + Rep.StoragePath + "...\n"));
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Log - " + Rep.repositoryLog + "...\n"));
                lblRepId.Text = Rep.repositoryId;
                lblRepUrl.Text = Rep.repositoryURI;
                testConnections();
            }
        }

        private void testConnections()
        {
            logWindow.AppendText("--- --- ---\n");
            //test Registry connection
            //extract hostname
            int posDoubleSlash = Rep.registryURI.IndexOf("//");
            if (posDoubleSlash > -1)
            {
                //extract port
                int posRegistryPort = Rep.registryURI.IndexOf(":", posDoubleSlash);
                if (posRegistryPort > -1)
                {
                    string hostname = Rep.registryURI.Substring(posDoubleSlash + 2, posRegistryPort - (posDoubleSlash + 2));
                    int registryPort = int.Parse(Rep.registryURI.Substring(posRegistryPort + 1, 4));
                    bool regConnected = testConnection("Registry", hostname, registryPort);
                    if (regConnected == true)
                    {
                        lblRegUrl.Text = Rep.registryURI;
                    }
                    else
                    {
                        lblRegUrl.Text = "Not able to connect...";
                    }
                }
            }
            //test ATNA connection
            testConnection("ATNA", Rep.atnaHost, Rep.atnaPort);
        }

        private bool testConnection(string hostname, string host, int port)
        {
            string textToSend = DateTime.Now.ToString();
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(host, port);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
                nwStream.Write(bytesToSend, 0, bytesToSend.Length);
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connection open to " + hostname + " at " + host + ":" + port + "...\n");
                client.Close();
                return true;
            }
            catch (Exception ex)
            {
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connection failed to " + hostname + " at " + host + ":" + port + "...\n");
                client.Close();
                return false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            {
                frmSettings frmSettings = new frmSettings();
                frmSettings.StartPosition = FormStartPosition.Manual;
                frmSettings.Owner = this;
                frmSettings.ShowDialog();
                return true;
            }
            else if (keyData == (Keys.F4))
            {
                Rep.StopListen();
                logWindow.AppendText("--- --- ---\n");
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Refreshing values...\n");
                SetupProperties();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        void logMessageHandler(string msg)
        {
            if (InvokeRequired)
                Invoke(LogMessageHandler, new object[] { msg });
            else
                logWindow.AppendText(msg + "\n");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult closeDecision = MessageBox.Show("Are you sure you want to close the XDS Repository?", "Close Repository", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if(closeDecision == DialogResult.Yes)
            {
                clearLogWindow();
                Rep.StopListen();
                this.Close();
            }
        }

        private void clearLogWindow()
        {
            //create log of all days entries
            using (StreamWriter sw = new StreamWriter(Rep.repositoryLog + "eventLog_" + DateTime.Now.ToString("ddMMyyyy") + ".txt", true))
            {
                string allevents = logWindow.Text;
                sw.Write("--- --- ---\n");
                sw.Write(allevents);
            }
        }

        private void tmrRegistryConn_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Date > currentDate.Date)
            {
                clearLogWindow();
                this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository";
                logWindow.Text = "";
                Rep.StopListen();
                logWindow.AppendText("--- --- ---\n");
                SetupProperties();
                currentDate = DateTime.Now;
            }
            testConnections();
        }
    }
}
