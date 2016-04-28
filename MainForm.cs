using System;
using System.Windows.Forms;
using System.IO;
//using System.Diagnostics;
using XdsObjects;
//using XdsObjects.Enums;
//using System.Collections.Generic;
//using Org.BouncyCastle.Crypto.Tls;
//using System.ServiceModel;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace XdsRepository
{
    public partial class RepositoryForm : Form
    {
        Repository Rep = new Repository();
        Repository.LogMessageHandler LogMessageHandler;
        DateTime currentDate = DateTime.Now;
        
        public RepositoryForm()
        {
            try
            {
                InitializeComponent();
                LogMessageHandler = new Repository.LogMessageHandler(logMessageHandler);
                this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository (stopped)";
                //SetupProperties();
            }
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message;
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": RepositoryForm - " + exceptionMsg + "...\n"));
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if(cmdStart.Text == "START")
            {
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Starting Repository...\n");
                SetupProperties();
                cmdStart.Text = "STOP";
                cmdStart.FlatStyle = FlatStyle.Popup;
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository started...\n");
                logWindow.AppendText("--- --- ---\n");
                this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository";
            }
            else if (cmdStart.Text == "STOP")
            {
                logWindow.AppendText("--- --- ---\n");
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Stopping Repository...\n");
                Rep.StopListen();
                clearLogWindow();
                cmdStart.Text = "START";
                cmdStart.FlatStyle = FlatStyle.Flat;
                cmdStart.FlatAppearance.BorderSize = 2;
                cmdStart.FlatAppearance.BorderColor = System.Drawing.Color.Red;
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository stopped...\n");
                logWindow.AppendText("--- --- ---\n");
                this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository (stopped)";
            }
        }

        private void SetupProperties()
        {
            try
            {
                //logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Setting properties...\n"));
                Rep.LogMessageEvent -= LogMessageHandler;
                bool readProperties = Rep.readProperties();
                if (readProperties == false)
                {
                    MessageBox.Show("Error in reading properties...", "Error");
                }
                else
                {
                    Rep.LogMessageEvent += LogMessageHandler;
                    //logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Authority Domain - " + Rep.authDomain + "...\n"));
                    logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Store - " + Rep.StoragePath + "...\n"));
                    logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": Repository Log - " + Rep.repositoryLog + "...\n"));
                    testConnection("ATNA", Rep.atnaHost, Rep.atnaPort);
                    Rep.StartListen();
                    lblAuthDomain.Text = Rep.authDomain;
                    lblRepId.Text = Rep.repositoryId;
                    lblRepUrl.Text = Rep.repositoryURI;
                    lblRegUrl.Text = Rep.registryURI;
                    
                    /*
                    int testConn = Rep.testRegConnection();
                    if(testConn == 0)
                    {
                        lblRegUrl.ForeColor = System.Drawing.Color.Black;
                    }
                    else if(testConn == 1)
                    {
                        lblRegUrl.ForeColor = System.Drawing.Color.Red;
                    }
                    */
                    //testConnections();
                }
                //logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": All properties set...\n"));
            }
            catch(Exception ex)
            {
                string exceptionMsg = ex.Message;
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": SetupProperties - " + exceptionMsg + "...\n"));
            }
        }

        /*
        private void testConnections()
        {
            try
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
                        int posSingleSlash = Rep.registryURI.IndexOf('/', posRegistryPort);
                        int registryPort = int.Parse(Rep.registryURI.Substring(posRegistryPort + 1, posSingleSlash - (posRegistryPort + 1)));
                        //int registryPort = int.Parse(Rep.registryURI.Substring(posRegistryPort + 1, 4));
                        //bool regConnected = testConnection("Registry", hostname, registryPort);
                        bool regConnected = testConnection("Registry", Rep.registryURI);
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
            catch (Exception ex)
            {
                string exceptionMsg = ex.Message;
                logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": testConnections - " + exceptionMsg + "...\n"));
            }
        }
        */

        /*
        private bool testConnection(string hostname,string url)
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)myRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //  it's at least in some way responsive
                    //  but may be internally broken
                    //  as you could find out if you called one of the methods for real
                    logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connection open to " + hostname + "...\n");
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
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connection failed to " + hostname + "...\n");
                //logWindow.AppendText((DateTime.Now.ToString("HH:mm:ss.fff") + ": testConnection - " + ex.Message + "...\n"));
                return false;
            }
        }
        */

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
                string exceptionMsg = ex.Message;
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Connection failed to " + hostname + " at " + host + ":" + port + "...\n");
                client.Close();
                return false;
            }
        }

        /*
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            {
                UpdateSettings();
                return true;
            }
            else if (keyData == (Keys.F4) && cmdStart.Text != "START")
            {
                Rep.StopListen();
                logWindow.AppendText("--- --- ---\n");
                logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Refreshing values...\n");
                SetupProperties();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        */

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
            //testConnections();
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            frmSettings frmSettings = new frmSettings();
            frmSettings.StartPosition = FormStartPosition.Manual;
            frmSettings.Owner = this;
            frmSettings.ShowDialog();

            Rep.StopListen();
            logWindow.AppendText("--- --- ---\n");
            logWindow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + ": Refreshing settings...\n");
            SetupProperties();

            cmdStart.Text = "STOP";
            cmdStart.FlatStyle = FlatStyle.Popup;
            this.Text = DateTime.Now.ToString("dd/MM/yyyy") + " - HSS XDS Repository";
        }
    }
}
