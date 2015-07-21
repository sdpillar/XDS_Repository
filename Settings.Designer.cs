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
            this.txtRepositoryPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRepositoryURI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRepositoryId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRepositoryLog = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSaveSettings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAtnaPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAtnaHost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRegistryURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtThumbprint = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ttpSettings = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRepositoryPath
            // 
            this.txtRepositoryPath.Location = new System.Drawing.Point(101, 25);
            this.txtRepositoryPath.Name = "txtRepositoryPath";
            this.txtRepositoryPath.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryPath.TabIndex = 3;
            this.txtRepositoryPath.Tag = "";
            this.txtRepositoryPath.Leave += new System.EventHandler(this.txtRepositoryPath_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Repository Path:";
            // 
            // txtRepositoryURI
            // 
            this.txtRepositoryURI.Location = new System.Drawing.Point(101, 78);
            this.txtRepositoryURI.Name = "txtRepositoryURI";
            this.txtRepositoryURI.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryURI.TabIndex = 7;
            this.txtRepositoryURI.Tag = "";
            this.txtRepositoryURI.Leave += new System.EventHandler(this.txtRepositoryURI_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Repository URI:";
            // 
            // txtRepositoryId
            // 
            this.txtRepositoryId.Location = new System.Drawing.Point(101, 52);
            this.txtRepositoryId.Name = "txtRepositoryId";
            this.txtRepositoryId.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryId.TabIndex = 5;
            this.txtRepositoryId.Tag = "";
            this.txtRepositoryId.Leave += new System.EventHandler(this.txtRepositoryId_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Repository Id:";
            // 
            // txtRepositoryLog
            // 
            this.txtRepositoryLog.Location = new System.Drawing.Point(101, 104);
            this.txtRepositoryLog.Name = "txtRepositoryLog";
            this.txtRepositoryLog.Size = new System.Drawing.Size(192, 20);
            this.txtRepositoryLog.TabIndex = 9;
            this.txtRepositoryLog.Tag = "";
            this.txtRepositoryLog.Leave += new System.EventHandler(this.txtRepositoryLog_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Repository Log:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtRepositoryPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtRepositoryLog);
            this.groupBox1.Controls.Add(this.txtRepositoryId);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRepositoryURI);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 134);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Repository";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(240, 445);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 15;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSaveSettings
            // 
            this.cmdSaveSettings.Enabled = false;
            this.cmdSaveSettings.Location = new System.Drawing.Point(106, 445);
            this.cmdSaveSettings.Name = "cmdSaveSettings";
            this.cmdSaveSettings.Size = new System.Drawing.Size(122, 23);
            this.cmdSaveSettings.TabIndex = 16;
            this.cmdSaveSettings.Text = "Save Settings";
            this.cmdSaveSettings.UseVisualStyleBackColor = true;
            this.cmdSaveSettings.Click += new System.EventHandler(this.cmdSaveSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAtnaPort);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtAtnaHost);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 358);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 81);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ATNA";
            // 
            // txtAtnaPort
            // 
            this.txtAtnaPort.Location = new System.Drawing.Point(100, 51);
            this.txtAtnaPort.Name = "txtAtnaPort";
            this.txtAtnaPort.Size = new System.Drawing.Size(76, 20);
            this.txtAtnaPort.TabIndex = 17;
            this.txtAtnaPort.Tag = "";
            this.txtAtnaPort.Leave += new System.EventHandler(this.txtAtnaPort_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "ATNA Port:";
            // 
            // txtAtnaHost
            // 
            this.txtAtnaHost.Location = new System.Drawing.Point(100, 25);
            this.txtAtnaHost.Name = "txtAtnaHost";
            this.txtAtnaHost.Size = new System.Drawing.Size(193, 20);
            this.txtAtnaHost.TabIndex = 15;
            this.txtAtnaHost.Tag = "";
            this.txtAtnaHost.Leave += new System.EventHandler(this.txtAtnaHost_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "ATNA Host:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRegistryURI);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 206);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 55);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Registry";
            // 
            // txtRegistryURI
            // 
            this.txtRegistryURI.Location = new System.Drawing.Point(100, 22);
            this.txtRegistryURI.Name = "txtRegistryURI";
            this.txtRegistryURI.Size = new System.Drawing.Size(193, 20);
            this.txtRegistryURI.TabIndex = 3;
            this.txtRegistryURI.Tag = "";
            this.txtRegistryURI.Leave += new System.EventHandler(this.txtRegistryURI_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Registry URI:";
            // 
            // txtThumbprint
            // 
            this.txtThumbprint.Location = new System.Drawing.Point(101, 28);
            this.txtThumbprint.Name = "txtThumbprint";
            this.txtThumbprint.Size = new System.Drawing.Size(193, 20);
            this.txtThumbprint.TabIndex = 5;
            this.txtThumbprint.Tag = "";
            this.txtThumbprint.Leave += new System.EventHandler(this.txtThumbprint_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Thumbprint:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtDomain);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 48);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Authority Domain";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Domain Id:";
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(101, 19);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(192, 20);
            this.txtDomain.TabIndex = 5;
            this.txtDomain.Tag = "";
            this.txtDomain.Leave += new System.EventHandler(this.txtDomain_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtAppId);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtThumbprint);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(12, 267);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(303, 85);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Certificates";
            // 
            // txtAppId
            // 
            this.txtAppId.Location = new System.Drawing.Point(100, 54);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.ReadOnly = true;
            this.txtAppId.Size = new System.Drawing.Size(193, 20);
            this.txtAppId.TabIndex = 7;
            this.txtAppId.Tag = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "App Id:";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 477);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdSaveSettings);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRepositoryPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRepositoryURI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRepositoryId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRepositoryLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSaveSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAtnaPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAtnaHost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRegistryURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtThumbprint;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtAppId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip ttpSettings;
    }
}