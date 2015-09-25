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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Repository");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Registry");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ATNA");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Certificates");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Authority Domain");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Logging");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Settings", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
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
            this.tvwSettings = new System.Windows.Forms.TreeView();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.pnlLogging = new System.Windows.Forms.Panel();
            this.lblLogging = new System.Windows.Forms.Label();
            this.cmdLog = new System.Windows.Forms.Button();
            this.txtRepositoryLog = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpRegistry = new System.Windows.Forms.GroupBox();
            this.pnlRegistry = new System.Windows.Forms.Panel();
            this.lblRegistry = new System.Windows.Forms.Label();
            this.grpAtna = new System.Windows.Forms.GroupBox();
            this.pnlAtna = new System.Windows.Forms.Panel();
            this.lblAtna = new System.Windows.Forms.Label();
            this.txtAtnaPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAtnaHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpCertificates = new System.Windows.Forms.GroupBox();
            this.pnlCertificates = new System.Windows.Forms.Panel();
            this.lblCertificates = new System.Windows.Forms.Label();
            this.grpDomain = new System.Windows.Forms.GroupBox();
            this.pnlDomain = new System.Windows.Forms.Panel();
            this.lblDomain = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.grpRepository = new System.Windows.Forms.GroupBox();
            this.pnlRepository = new System.Windows.Forms.Panel();
            this.lblRepository = new System.Windows.Forms.Label();
            this.cmdRepository = new System.Windows.Forms.Button();
            this.dlgLog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.grpLogging.SuspendLayout();
            this.pnlLogging.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpRegistry.SuspendLayout();
            this.pnlRegistry.SuspendLayout();
            this.grpAtna.SuspendLayout();
            this.pnlAtna.SuspendLayout();
            this.grpCertificates.SuspendLayout();
            this.pnlCertificates.SuspendLayout();
            this.grpDomain.SuspendLayout();
            this.pnlDomain.SuspendLayout();
            this.grpRepository.SuspendLayout();
            this.pnlRepository.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRepositoryPath
            // 
            this.txtRepositoryPath.Location = new System.Drawing.Point(102, 45);
            this.txtRepositoryPath.Name = "txtRepositoryPath";
            this.txtRepositoryPath.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryPath.TabIndex = 3;
            this.txtRepositoryPath.Tag = "";
            this.ttpSettings.SetToolTip(this.txtRepositoryPath, "Date sub folder (dd_mm_yyyy) will be automatically created ");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Repository Path:";
            // 
            // txtRepositoryURI
            // 
            this.txtRepositoryURI.Location = new System.Drawing.Point(102, 97);
            this.txtRepositoryURI.Name = "txtRepositoryURI";
            this.txtRepositoryURI.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryURI.TabIndex = 7;
            this.txtRepositoryURI.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository URI:";
            // 
            // txtRepositoryId
            // 
            this.txtRepositoryId.Location = new System.Drawing.Point(102, 71);
            this.txtRepositoryId.Name = "txtRepositoryId";
            this.txtRepositoryId.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryId.TabIndex = 5;
            this.txtRepositoryId.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 71);
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
            this.txtRegistryURI.Location = new System.Drawing.Point(102, 45);
            this.txtRegistryURI.Name = "txtRegistryURI";
            this.txtRegistryURI.Size = new System.Drawing.Size(193, 20);
            this.txtRegistryURI.TabIndex = 3;
            this.txtRegistryURI.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Registry URI:";
            // 
            // txtThumbprint
            // 
            this.txtThumbprint.Location = new System.Drawing.Point(102, 45);
            this.txtThumbprint.Name = "txtThumbprint";
            this.txtThumbprint.Size = new System.Drawing.Size(193, 20);
            this.txtThumbprint.TabIndex = 5;
            this.txtThumbprint.Tag = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Thumbprint:";
            // 
            // txtAppId
            // 
            this.txtAppId.Location = new System.Drawing.Point(102, 71);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.ReadOnly = true;
            this.txtAppId.Size = new System.Drawing.Size(193, 20);
            this.txtAppId.TabIndex = 7;
            this.txtAppId.Tag = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "App Id:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grpCertificates);
            this.panel1.Controls.Add(this.grpRepository);
            this.panel1.Controls.Add(this.grpAtna);
            this.panel1.Controls.Add(this.tvwSettings);
            this.panel1.Controls.Add(this.grpDomain);
            this.panel1.Controls.Add(this.grpLogging);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.grpRegistry);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 237);
            this.panel1.TabIndex = 20;
            // 
            // tvwSettings
            // 
            this.tvwSettings.Location = new System.Drawing.Point(6, 12);
            this.tvwSettings.Name = "tvwSettings";
            treeNode1.BackColor = System.Drawing.Color.Transparent;
            treeNode1.Checked = true;
            treeNode1.ForeColor = System.Drawing.Color.Black;
            treeNode1.Name = "nodRepository";
            treeNode1.Text = "Repository";
            treeNode2.ForeColor = System.Drawing.Color.SlateGray;
            treeNode2.Name = "nodRegistry";
            treeNode2.Text = "Registry";
            treeNode3.ForeColor = System.Drawing.Color.SlateGray;
            treeNode3.Name = "nodAtna";
            treeNode3.Text = "ATNA";
            treeNode4.ForeColor = System.Drawing.Color.SlateGray;
            treeNode4.Name = "nodCertificates";
            treeNode4.Text = "Certificates";
            treeNode5.ForeColor = System.Drawing.Color.SlateGray;
            treeNode5.Name = "nodDomain";
            treeNode5.Text = "Authority Domain";
            treeNode6.ForeColor = System.Drawing.Color.SlateGray;
            treeNode6.Name = "nodLogging";
            treeNode6.Text = "Logging";
            treeNode7.Checked = true;
            treeNode7.Name = "nodSettings";
            treeNode7.Text = "Settings";
            this.tvwSettings.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.tvwSettings.Size = new System.Drawing.Size(150, 150);
            this.tvwSettings.TabIndex = 18;
            this.tvwSettings.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSettings_AfterSelect);
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.pnlLogging);
            this.grpLogging.Controls.Add(this.cmdLog);
            this.grpLogging.Controls.Add(this.txtRepositoryLog);
            this.grpLogging.Controls.Add(this.label12);
            this.grpLogging.Location = new System.Drawing.Point(165, 12);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Size = new System.Drawing.Size(350, 150);
            this.grpLogging.TabIndex = 21;
            this.grpLogging.TabStop = false;
            this.grpLogging.Visible = false;
            // 
            // pnlLogging
            // 
            this.pnlLogging.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlLogging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLogging.Controls.Add(this.lblLogging);
            this.pnlLogging.Location = new System.Drawing.Point(0, 0);
            this.pnlLogging.Name = "pnlLogging";
            this.pnlLogging.Size = new System.Drawing.Size(350, 25);
            this.pnlLogging.TabIndex = 25;
            // 
            // lblLogging
            // 
            this.lblLogging.AutoSize = true;
            this.lblLogging.BackColor = System.Drawing.Color.Transparent;
            this.lblLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogging.Location = new System.Drawing.Point(2, 2);
            this.lblLogging.Name = "lblLogging";
            this.lblLogging.Size = new System.Drawing.Size(67, 20);
            this.lblLogging.TabIndex = 23;
            this.lblLogging.Text = "label16";
            // 
            // cmdLog
            // 
            this.cmdLog.Location = new System.Drawing.Point(301, 45);
            this.cmdLog.Name = "cmdLog";
            this.cmdLog.Size = new System.Drawing.Size(29, 22);
            this.cmdLog.TabIndex = 6;
            this.cmdLog.UseVisualStyleBackColor = true;
            this.cmdLog.Click += new System.EventHandler(this.cmdLog_Click);
            // 
            // txtRepositoryLog
            // 
            this.txtRepositoryLog.Location = new System.Drawing.Point(102, 45);
            this.txtRepositoryLog.Name = "txtRepositoryLog";
            this.txtRepositoryLog.Size = new System.Drawing.Size(193, 20);
            this.txtRepositoryLog.TabIndex = 5;
            this.txtRepositoryLog.Tag = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Log Directory:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cmdClose);
            this.panel2.Location = new System.Drawing.Point(6, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 50);
            this.panel2.TabIndex = 17;
            // 
            // grpRegistry
            // 
            this.grpRegistry.Controls.Add(this.pnlRegistry);
            this.grpRegistry.Controls.Add(this.txtRegistryURI);
            this.grpRegistry.Controls.Add(this.label1);
            this.grpRegistry.Location = new System.Drawing.Point(165, 12);
            this.grpRegistry.Name = "grpRegistry";
            this.grpRegistry.Size = new System.Drawing.Size(350, 150);
            this.grpRegistry.TabIndex = 20;
            this.grpRegistry.TabStop = false;
            this.grpRegistry.Visible = false;
            // 
            // pnlRegistry
            // 
            this.pnlRegistry.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlRegistry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRegistry.Controls.Add(this.lblRegistry);
            this.pnlRegistry.Location = new System.Drawing.Point(0, 0);
            this.pnlRegistry.Name = "pnlRegistry";
            this.pnlRegistry.Size = new System.Drawing.Size(350, 25);
            this.pnlRegistry.TabIndex = 24;
            // 
            // lblRegistry
            // 
            this.lblRegistry.AutoSize = true;
            this.lblRegistry.BackColor = System.Drawing.Color.Transparent;
            this.lblRegistry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistry.Location = new System.Drawing.Point(2, 2);
            this.lblRegistry.Name = "lblRegistry";
            this.lblRegistry.Size = new System.Drawing.Size(67, 20);
            this.lblRegistry.TabIndex = 23;
            this.lblRegistry.Text = "label11";
            // 
            // grpAtna
            // 
            this.grpAtna.Controls.Add(this.pnlAtna);
            this.grpAtna.Controls.Add(this.txtAtnaPort);
            this.grpAtna.Controls.Add(this.label5);
            this.grpAtna.Controls.Add(this.txtAtnaHost);
            this.grpAtna.Controls.Add(this.label6);
            this.grpAtna.Location = new System.Drawing.Point(165, 12);
            this.grpAtna.Name = "grpAtna";
            this.grpAtna.Size = new System.Drawing.Size(350, 150);
            this.grpAtna.TabIndex = 21;
            this.grpAtna.TabStop = false;
            this.grpAtna.Visible = false;
            // 
            // pnlAtna
            // 
            this.pnlAtna.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlAtna.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAtna.Controls.Add(this.lblAtna);
            this.pnlAtna.Location = new System.Drawing.Point(0, 0);
            this.pnlAtna.Name = "pnlAtna";
            this.pnlAtna.Size = new System.Drawing.Size(350, 25);
            this.pnlAtna.TabIndex = 25;
            // 
            // lblAtna
            // 
            this.lblAtna.AutoSize = true;
            this.lblAtna.BackColor = System.Drawing.Color.Transparent;
            this.lblAtna.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtna.Location = new System.Drawing.Point(2, 2);
            this.lblAtna.Name = "lblAtna";
            this.lblAtna.Size = new System.Drawing.Size(57, 20);
            this.lblAtna.TabIndex = 23;
            this.lblAtna.Text = "label7";
            // 
            // txtAtnaPort
            // 
            this.txtAtnaPort.Location = new System.Drawing.Point(102, 71);
            this.txtAtnaPort.Name = "txtAtnaPort";
            this.txtAtnaPort.Size = new System.Drawing.Size(76, 20);
            this.txtAtnaPort.TabIndex = 21;
            this.txtAtnaPort.Tag = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "ATNA Port:";
            // 
            // txtAtnaHost
            // 
            this.txtAtnaHost.Location = new System.Drawing.Point(102, 45);
            this.txtAtnaHost.Name = "txtAtnaHost";
            this.txtAtnaHost.Size = new System.Drawing.Size(193, 20);
            this.txtAtnaHost.TabIndex = 19;
            this.txtAtnaHost.Tag = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "ATNA Host:";
            // 
            // grpCertificates
            // 
            this.grpCertificates.Controls.Add(this.pnlCertificates);
            this.grpCertificates.Controls.Add(this.txtAppId);
            this.grpCertificates.Controls.Add(this.txtThumbprint);
            this.grpCertificates.Controls.Add(this.label10);
            this.grpCertificates.Controls.Add(this.label9);
            this.grpCertificates.Location = new System.Drawing.Point(165, 12);
            this.grpCertificates.Name = "grpCertificates";
            this.grpCertificates.Size = new System.Drawing.Size(350, 150);
            this.grpCertificates.TabIndex = 22;
            this.grpCertificates.TabStop = false;
            this.grpCertificates.Visible = false;
            // 
            // pnlCertificates
            // 
            this.pnlCertificates.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlCertificates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCertificates.Controls.Add(this.lblCertificates);
            this.pnlCertificates.Location = new System.Drawing.Point(0, 0);
            this.pnlCertificates.Name = "pnlCertificates";
            this.pnlCertificates.Size = new System.Drawing.Size(350, 25);
            this.pnlCertificates.TabIndex = 25;
            // 
            // lblCertificates
            // 
            this.lblCertificates.AutoSize = true;
            this.lblCertificates.BackColor = System.Drawing.Color.Transparent;
            this.lblCertificates.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCertificates.Location = new System.Drawing.Point(2, 2);
            this.lblCertificates.Name = "lblCertificates";
            this.lblCertificates.Size = new System.Drawing.Size(67, 20);
            this.lblCertificates.TabIndex = 23;
            this.lblCertificates.Text = "label15";
            // 
            // grpDomain
            // 
            this.grpDomain.Controls.Add(this.pnlDomain);
            this.grpDomain.Controls.Add(this.label8);
            this.grpDomain.Controls.Add(this.txtDomain);
            this.grpDomain.Location = new System.Drawing.Point(165, 12);
            this.grpDomain.Name = "grpDomain";
            this.grpDomain.Size = new System.Drawing.Size(350, 150);
            this.grpDomain.TabIndex = 22;
            this.grpDomain.TabStop = false;
            this.grpDomain.Visible = false;
            // 
            // pnlDomain
            // 
            this.pnlDomain.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlDomain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDomain.Controls.Add(this.lblDomain);
            this.pnlDomain.Location = new System.Drawing.Point(0, 0);
            this.pnlDomain.Name = "pnlDomain";
            this.pnlDomain.Size = new System.Drawing.Size(350, 25);
            this.pnlDomain.TabIndex = 25;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.BackColor = System.Drawing.Color.Transparent;
            this.lblDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomain.Location = new System.Drawing.Point(2, 2);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(67, 20);
            this.lblDomain.TabIndex = 23;
            this.lblDomain.Text = "label13";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Domain Id:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(102, 45);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(192, 20);
            this.txtDomain.TabIndex = 7;
            this.txtDomain.Tag = "";
            // 
            // grpRepository
            // 
            this.grpRepository.Controls.Add(this.pnlRepository);
            this.grpRepository.Controls.Add(this.cmdRepository);
            this.grpRepository.Controls.Add(this.label2);
            this.grpRepository.Controls.Add(this.txtRepositoryPath);
            this.grpRepository.Controls.Add(this.txtRepositoryURI);
            this.grpRepository.Controls.Add(this.label4);
            this.grpRepository.Controls.Add(this.label3);
            this.grpRepository.Controls.Add(this.txtRepositoryId);
            this.grpRepository.Location = new System.Drawing.Point(165, 12);
            this.grpRepository.Name = "grpRepository";
            this.grpRepository.Size = new System.Drawing.Size(350, 150);
            this.grpRepository.TabIndex = 19;
            this.grpRepository.TabStop = false;
            // 
            // pnlRepository
            // 
            this.pnlRepository.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlRepository.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRepository.Controls.Add(this.lblRepository);
            this.pnlRepository.Location = new System.Drawing.Point(0, 0);
            this.pnlRepository.Name = "pnlRepository";
            this.pnlRepository.Size = new System.Drawing.Size(350, 25);
            this.pnlRepository.TabIndex = 25;
            // 
            // lblRepository
            // 
            this.lblRepository.AutoSize = true;
            this.lblRepository.BackColor = System.Drawing.Color.Transparent;
            this.lblRepository.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepository.Location = new System.Drawing.Point(2, 2);
            this.lblRepository.Name = "lblRepository";
            this.lblRepository.Size = new System.Drawing.Size(67, 20);
            this.lblRepository.TabIndex = 23;
            this.lblRepository.Text = "label14";
            // 
            // cmdRepository
            // 
            this.cmdRepository.Location = new System.Drawing.Point(301, 45);
            this.cmdRepository.Name = "cmdRepository";
            this.cmdRepository.Size = new System.Drawing.Size(29, 22);
            this.cmdRepository.TabIndex = 8;
            this.cmdRepository.UseVisualStyleBackColor = true;
            this.cmdRepository.Click += new System.EventHandler(this.cmdRepository_Click);
            // 
            // dlgLog
            // 
            this.dlgLog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.dlgLog.SelectedPath = "C:\\HSS";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 257);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "XDS Repository Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.panel1.ResumeLayout(false);
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.pnlLogging.ResumeLayout(false);
            this.pnlLogging.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.grpRegistry.ResumeLayout(false);
            this.grpRegistry.PerformLayout();
            this.pnlRegistry.ResumeLayout(false);
            this.pnlRegistry.PerformLayout();
            this.grpAtna.ResumeLayout(false);
            this.grpAtna.PerformLayout();
            this.pnlAtna.ResumeLayout(false);
            this.pnlAtna.PerformLayout();
            this.grpCertificates.ResumeLayout(false);
            this.grpCertificates.PerformLayout();
            this.pnlCertificates.ResumeLayout(false);
            this.pnlCertificates.PerformLayout();
            this.grpDomain.ResumeLayout(false);
            this.grpDomain.PerformLayout();
            this.pnlDomain.ResumeLayout(false);
            this.pnlDomain.PerformLayout();
            this.grpRepository.ResumeLayout(false);
            this.grpRepository.PerformLayout();
            this.pnlRepository.ResumeLayout(false);
            this.pnlRepository.PerformLayout();
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
        private System.Windows.Forms.Panel pnlRegistry;
        private System.Windows.Forms.Label lblRegistry;
        private System.Windows.Forms.Button cmdLog;
        private System.Windows.Forms.TextBox txtRepositoryLog;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.FolderBrowserDialog dlgLog;
        private System.Windows.Forms.Button cmdRepository;
        private System.Windows.Forms.Panel pnlAtna;
        private System.Windows.Forms.Label lblAtna;
        private System.Windows.Forms.Panel pnlCertificates;
        private System.Windows.Forms.Label lblCertificates;
        private System.Windows.Forms.Panel pnlDomain;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.Panel pnlLogging;
        private System.Windows.Forms.Label lblLogging;
        private System.Windows.Forms.Panel pnlRepository;
        private System.Windows.Forms.Label lblRepository;
    }
}