namespace XdsRepository
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode50 = new System.Windows.Forms.TreeNode("Repository");
            System.Windows.Forms.TreeNode treeNode51 = new System.Windows.Forms.TreeNode("Registry");
            System.Windows.Forms.TreeNode treeNode52 = new System.Windows.Forms.TreeNode("ATNA");
            System.Windows.Forms.TreeNode treeNode53 = new System.Windows.Forms.TreeNode("Certificates");
            System.Windows.Forms.TreeNode treeNode54 = new System.Windows.Forms.TreeNode("Authority Domain");
            System.Windows.Forms.TreeNode treeNode55 = new System.Windows.Forms.TreeNode("Logging");
            System.Windows.Forms.TreeNode treeNode56 = new System.Windows.Forms.TreeNode("Settings", new System.Windows.Forms.TreeNode[] {
            treeNode50,
            treeNode51,
            treeNode52,
            treeNode53,
            treeNode54,
            treeNode55});
            this.txtRepositoryPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRepositoryURI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRepositoryId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.txtRegistryURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThumbprint = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ttpSettings = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.grpRegistry = new System.Windows.Forms.GroupBox();
            this.grpAtna = new System.Windows.Forms.GroupBox();
            this.txtAtnaPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAtnaHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpCertificates = new System.Windows.Forms.GroupBox();
            this.grpDomain = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.cmdLog = new System.Windows.Forms.Button();
            this.txtRepositoryLog = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.grpRepository = new System.Windows.Forms.GroupBox();
            this.tvwSettings = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dlgLog = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdRepository = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.grpRegistry.SuspendLayout();
            this.grpAtna.SuspendLayout();
            this.grpCertificates.SuspendLayout();
            this.grpDomain.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.grpRepository.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRepositoryPath
            // 
            this.txtRepositoryPath.Location = new System.Drawing.Point(100, 16);
            this.txtRepositoryPath.Name = "txtRepositoryPath";
            this.txtRepositoryPath.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryPath.TabIndex = 3;
            this.txtRepositoryPath.Tag = "";
            this.ttpSettings.SetToolTip(this.txtRepositoryPath, "Date sub folder (dd_mm_yyyy) will be automatically created ");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Repository Path:";
            // 
            // txtRepositoryURI
            // 
            this.txtRepositoryURI.Location = new System.Drawing.Point(101, 68);
            this.txtRepositoryURI.Name = "txtRepositoryURI";
            this.txtRepositoryURI.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryURI.TabIndex = 7;
            this.txtRepositoryURI.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository URI:";
            // 
            // txtRepositoryId
            // 
            this.txtRepositoryId.Location = new System.Drawing.Point(101, 42);
            this.txtRepositoryId.Name = "txtRepositoryId";
            this.txtRepositoryId.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryId.TabIndex = 5;
            this.txtRepositoryId.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Repository Id:";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 14);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(122, 23);
            this.cmdClose.TabIndex = 15;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // txtRegistryURI
            // 
            this.txtRegistryURI.Location = new System.Drawing.Point(100, 16);
            this.txtRegistryURI.Name = "txtRegistryURI";
            this.txtRegistryURI.Size = new System.Drawing.Size(193, 20);
            this.txtRegistryURI.TabIndex = 3;
            this.txtRegistryURI.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Registry URI:";
            // 
            // txtThumbprint
            // 
            this.txtThumbprint.Location = new System.Drawing.Point(100, 16);
            this.txtThumbprint.Name = "txtThumbprint";
            this.txtThumbprint.Size = new System.Drawing.Size(193, 20);
            this.txtThumbprint.TabIndex = 5;
            this.txtThumbprint.Tag = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Thumbprint:";
            // 
            // txtAppId
            // 
            this.txtAppId.Location = new System.Drawing.Point(100, 43);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.ReadOnly = true;
            this.txtAppId.Size = new System.Drawing.Size(193, 20);
            this.txtAppId.TabIndex = 7;
            this.txtAppId.Tag = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "App Id:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.grpRegistry);
            this.panel1.Controls.Add(this.grpAtna);
            this.panel1.Controls.Add(this.grpCertificates);
            this.panel1.Controls.Add(this.grpDomain);
            this.panel1.Controls.Add(this.grpLogging);
            this.panel1.Controls.Add(this.grpRepository);
            this.panel1.Controls.Add(this.tvwSettings);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 237);
            this.panel1.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(174, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 25);
            this.panel3.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(2, 2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 20);
            this.label11.TabIndex = 23;
            this.label11.Text = "label11";
            // 
            // grpRegistry
            // 
            this.grpRegistry.Controls.Add(this.txtRegistryURI);
            this.grpRegistry.Controls.Add(this.label1);
            this.grpRegistry.Location = new System.Drawing.Point(174, 33);
            this.grpRegistry.Name = "grpRegistry";
            this.grpRegistry.Size = new System.Drawing.Size(350, 130);
            this.grpRegistry.TabIndex = 20;
            this.grpRegistry.TabStop = false;
            this.grpRegistry.Visible = false;
            // 
            // grpAtna
            // 
            this.grpAtna.Controls.Add(this.txtAtnaPort);
            this.grpAtna.Controls.Add(this.label5);
            this.grpAtna.Controls.Add(this.txtAtnaHost);
            this.grpAtna.Controls.Add(this.label6);
            this.grpAtna.Location = new System.Drawing.Point(174, 33);
            this.grpAtna.Name = "grpAtna";
            this.grpAtna.Size = new System.Drawing.Size(350, 130);
            this.grpAtna.TabIndex = 21;
            this.grpAtna.TabStop = false;
            this.grpAtna.Visible = false;
            // 
            // txtAtnaPort
            // 
            this.txtAtnaPort.Location = new System.Drawing.Point(100, 43);
            this.txtAtnaPort.Name = "txtAtnaPort";
            this.txtAtnaPort.Size = new System.Drawing.Size(76, 20);
            this.txtAtnaPort.TabIndex = 21;
            this.txtAtnaPort.Tag = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "ATNA Port:";
            // 
            // txtAtnaHost
            // 
            this.txtAtnaHost.Location = new System.Drawing.Point(100, 16);
            this.txtAtnaHost.Name = "txtAtnaHost";
            this.txtAtnaHost.Size = new System.Drawing.Size(193, 20);
            this.txtAtnaHost.TabIndex = 19;
            this.txtAtnaHost.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "ATNA Host:";
            // 
            // grpCertificates
            // 
            this.grpCertificates.Controls.Add(this.txtAppId);
            this.grpCertificates.Controls.Add(this.txtThumbprint);
            this.grpCertificates.Controls.Add(this.label10);
            this.grpCertificates.Controls.Add(this.label9);
            this.grpCertificates.Location = new System.Drawing.Point(174, 33);
            this.grpCertificates.Name = "grpCertificates";
            this.grpCertificates.Size = new System.Drawing.Size(350, 130);
            this.grpCertificates.TabIndex = 22;
            this.grpCertificates.TabStop = false;
            this.grpCertificates.Visible = false;
            // 
            // grpDomain
            // 
            this.grpDomain.Controls.Add(this.label8);
            this.grpDomain.Controls.Add(this.txtDomain);
            this.grpDomain.Location = new System.Drawing.Point(174, 33);
            this.grpDomain.Name = "grpDomain";
            this.grpDomain.Size = new System.Drawing.Size(350, 130);
            this.grpDomain.TabIndex = 22;
            this.grpDomain.TabStop = false;
            this.grpDomain.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Domain Id:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(100, 16);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(192, 20);
            this.txtDomain.TabIndex = 7;
            this.txtDomain.Tag = "";
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.cmdLog);
            this.grpLogging.Controls.Add(this.txtRepositoryLog);
            this.grpLogging.Controls.Add(this.label12);
            this.grpLogging.Location = new System.Drawing.Point(174, 33);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(350, 130);
            this.grpLogging.TabIndex = 21;
            this.grpLogging.TabStop = false;
            this.grpLogging.Visible = false;
            // 
            // cmdLog
            // 
            this.cmdLog.Location = new System.Drawing.Point(299, 15);
            this.cmdLog.Name = "cmdLog";
            this.cmdLog.Size = new System.Drawing.Size(29, 22);
            this.cmdLog.TabIndex = 6;
            this.cmdLog.UseVisualStyleBackColor = true;
            this.cmdLog.Click += new System.EventHandler(this.cmdLog_Click);
            // 
            // txtRepositoryLog
            // 
            this.txtRepositoryLog.Location = new System.Drawing.Point(100, 16);
            this.txtRepositoryLog.Name = "txtRepositoryLog";
            this.txtRepositoryLog.Size = new System.Drawing.Size(193, 20);
            this.txtRepositoryLog.TabIndex = 5;
            this.txtRepositoryLog.Tag = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Log Directory:";
            // 
            // grpRepository
            // 
            this.grpRepository.Controls.Add(this.cmdRepository);
            this.grpRepository.Controls.Add(this.label2);
            this.grpRepository.Controls.Add(this.txtRepositoryPath);
            this.grpRepository.Controls.Add(this.txtRepositoryURI);
            this.grpRepository.Controls.Add(this.label4);
            this.grpRepository.Controls.Add(this.label3);
            this.grpRepository.Controls.Add(this.txtRepositoryId);
            this.grpRepository.Location = new System.Drawing.Point(174, 33);
            this.grpRepository.Name = "grpRepository";
            this.grpRepository.Size = new System.Drawing.Size(350, 130);
            this.grpRepository.TabIndex = 19;
            this.grpRepository.TabStop = false;
            // 
            // tvwSettings
            // 
            this.tvwSettings.Location = new System.Drawing.Point(6, 12);
            this.tvwSettings.Name = "tvwSettings";
            treeNode50.BackColor = System.Drawing.Color.Transparent;
            treeNode50.Checked = true;
            treeNode50.ForeColor = System.Drawing.Color.Black;
            treeNode50.Name = "nodRepository";
            treeNode50.Text = "Repository";
            treeNode51.ForeColor = System.Drawing.Color.SlateGray;
            treeNode51.Name = "nodRegistry";
            treeNode51.Text = "Registry";
            treeNode52.ForeColor = System.Drawing.Color.SlateGray;
            treeNode52.Name = "nodAtna";
            treeNode52.Text = "ATNA";
            treeNode53.ForeColor = System.Drawing.Color.SlateGray;
            treeNode53.Name = "nodCertificates";
            treeNode53.Text = "Certificates";
            treeNode54.ForeColor = System.Drawing.Color.SlateGray;
            treeNode54.Name = "nodDomain";
            treeNode54.Text = "Authority Domain";
            treeNode55.ForeColor = System.Drawing.Color.SlateGray;
            treeNode55.Name = "nodLogging";
            treeNode55.Text = "Logging";
            treeNode56.Checked = true;
            treeNode56.Name = "nodSettings";
            treeNode56.Text = "Settings";
            this.tvwSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode56});
            this.tvwSettings.Size = new System.Drawing.Size(153, 151);
            this.tvwSettings.TabIndex = 18;
            this.tvwSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSettings_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdClose);
            this.panel2.Location = new System.Drawing.Point(7, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 52);
            this.panel2.TabIndex = 17;
            // 
            // dlgLog
            // 
            this.dlgLog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.dlgLog.SelectedPath = "C:\\HSS";
            // 
            // cmdRepository
            // 
            this.cmdRepository.Location = new System.Drawing.Point(299, 15);
            this.cmdRepository.Name = "cmdRepository";
            this.cmdRepository.Size = new System.Drawing.Size(29, 22);
            this.cmdRepository.TabIndex = 8;
            this.cmdRepository.UseVisualStyleBackColor = true;
            this.cmdRepository.Click += new System.EventHandler(this.cmdRepository_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 260);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "XDS Repository Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grpRegistry.ResumeLayout(false);
            this.grpRegistry.PerformLayout();
            this.grpAtna.ResumeLayout(false);
            this.grpAtna.PerformLayout();
            this.grpCertificates.ResumeLayout(false);
            this.grpCertificates.PerformLayout();
            this.grpDomain.ResumeLayout(false);
            this.grpDomain.PerformLayout();
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.grpRepository.ResumeLayout(false);
            this.grpRepository.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRepositoryPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRepositoryURI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRepositoryId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox txtRegistryURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThumbprint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAppId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip ttpSettings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpRegistry;
        private System.Windows.Forms.GroupBox grpRepository;
        private System.Windows.Forms.TreeView tvwSettings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpAtna;
        private System.Windows.Forms.GroupBox grpCertificates;
        private System.Windows.Forms.GroupBox grpDomain;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtAtnaPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAtnaHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdLog;
        private System.Windows.Forms.TextBox txtRepositoryLog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FolderBrowserDialog dlgLog;
        private System.Windows.Forms.Button cmdRepository;
    }
}