using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

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
    }
}
