namespace XdsRepository
{
    partial class RepositoryForm
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
            this.logWindow = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRepId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRepUrl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRegUrl = new System.Windows.Forms.Label();
            this.tmrRegistryConn = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAuthDomain = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // logWindow
            // 
            this.logWindow.BackColor = System.Drawing.Color.White;
            this.logWindow.Location = new System.Drawing.Point(0, 0);
            this.logWindow.Multiline = true;
            this.logWindow.Name = "logWindow";
            this.logWindow.ReadOnly = true;
            this.logWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logWindow.Size = new System.Drawing.Size(532, 460);
            this.logWindow.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 5;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(12, 607);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(130, 27);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Repository ID:";
            // 
            // lblRepId
            // 
            this.lblRepId.AutoSize = true;
            this.lblRepId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepId.Location = new System.Drawing.Point(150, 46);
            this.lblRepId.Name = "lblRepId";
            this.lblRepId.Size = new System.Drawing.Size(0, 16);
            this.lblRepId.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Repository URL:";
            // 
            // lblRepUrl
            // 
            this.lblRepUrl.AutoSize = true;
            this.lblRepUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRepUrl.Location = new System.Drawing.Point(150, 77);
            this.lblRepUrl.Name = "lblRepUrl";
            this.lblRepUrl.Size = new System.Drawing.Size(0, 16);
            this.lblRepUrl.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Registry URL:";
            // 
            // lblRegUrl
            // 
            this.lblRegUrl.AutoSize = true;
            this.lblRegUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegUrl.Location = new System.Drawing.Point(150, 108);
            this.lblRegUrl.Name = "lblRegUrl";
            this.lblRegUrl.Size = new System.Drawing.Size(0, 16);
            this.lblRegUrl.TabIndex = 14;
            // 
            // tmrRegistryConn
            // 
            this.tmrRegistryConn.Enabled = true;
            this.tmrRegistryConn.Interval = 3600000;
            this.tmrRegistryConn.Tick += new System.EventHandler(this.tmrRegistryConn_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblAuthDomain);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblRepId);
            this.groupBox1.Controls.Add(this.lblRegUrl);
            this.groupBox1.Controls.Add(this.lblRepUrl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 460);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 141);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Authority Domain:";
            // 
            // lblAuthDomain
            // 
            this.lblAuthDomain.AutoSize = true;
            this.lblAuthDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthDomain.Location = new System.Drawing.Point(150, 16);
            this.lblAuthDomain.Name = "lblAuthDomain";
            this.lblAuthDomain.Size = new System.Drawing.Size(0, 16);
            this.lblAuthDomain.TabIndex = 16;
            // 
            // RepositoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 640);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.logWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "RepositoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HSS XDS Repository";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logWindow;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRepId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRepUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRegUrl;
        private System.Windows.Forms.Timer tmrRegistryConn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAuthDomain;
    }
}

