namespace FSProfiles
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            fbdBasePath = new FolderBrowserDialog();
            lblMappings = new Label();
            clbMappings = new CheckedListBox();
            btnGenerate = new Button();
            txtOutputFile = new TextBox();
            LblOutputFile = new Label();
            lblContent = new Label();
            statusStrip1 = new StatusStrip();
            tsStatus = new ToolStripStatusLabel();
            tsProgress = new ToolStripProgressBar();
            cmbContent = new ComboBox();
            chkIncludeUncategorised = new CheckBox();
            toolTip1 = new ToolTip(components);
            LblInstallType = new Label();
            LinkHelp = new LinkLabel();
            btnDefaultLocations = new Button();
            LblOr = new Label();
            btnCustomLocations = new Button();
            lblIncludeAircraftSpecific = new Label();
            cmbOutputFormat = new ComboBox();
            LblOutputFormat = new Label();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblMappings
            // 
            lblMappings.AutoSize = true;
            lblMappings.Location = new Point(10, 60);
            lblMappings.Margin = new Padding(2, 0, 2, 0);
            lblMappings.Name = "lblMappings";
            lblMappings.Size = new Size(96, 15);
            lblMappings.TabIndex = 8;
            lblMappings.Text = "Detected Profiles";
            // 
            // clbMappings
            // 
            clbMappings.CheckOnClick = true;
            clbMappings.FormattingEnabled = true;
            clbMappings.Location = new Point(147, 60);
            clbMappings.Name = "clbMappings";
            clbMappings.Size = new Size(1126, 400);
            clbMappings.TabIndex = 9;
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.LightSkyBlue;
            btnGenerate.Location = new Point(146, 607);
            btnGenerate.Margin = new Padding(2);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(163, 29);
            btnGenerate.TabIndex = 15;
            btnGenerate.Text = "&Generate Binding Report";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += BtnGenerate_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(146, 570);
            txtOutputFile.Margin = new Padding(2);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(1127, 23);
            txtOutputFile.TabIndex = 11;
            // 
            // LblOutputFile
            // 
            LblOutputFile.AutoSize = true;
            LblOutputFile.Location = new Point(10, 573);
            LblOutputFile.Margin = new Padding(2, 0, 2, 0);
            LblOutputFile.Name = "LblOutputFile";
            LblOutputFile.Size = new Size(66, 15);
            LblOutputFile.TabIndex = 10;
            LblOutputFile.Text = "Output File";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(10, 482);
            lblContent.Margin = new Padding(2, 0, 2, 0);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(71, 15);
            lblContent.TabIndex = 12;
            lblContent.Text = "List Content";
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.Window;
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatus, tsProgress });
            statusStrip1.Location = new Point(0, 660);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1296, 31);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            tsStatus.AutoSize = false;
            tsStatus.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsStatus.Name = "tsStatus";
            tsStatus.Size = new Size(250, 26);
            tsStatus.Text = " ";
            tsStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tsProgress
            // 
            tsProgress.Alignment = ToolStripItemAlignment.Right;
            tsProgress.AutoSize = false;
            tsProgress.Name = "tsProgress";
            tsProgress.Size = new Size(150, 25);
            tsProgress.Style = ProgressBarStyle.Continuous;
            tsProgress.Visible = false;
            // 
            // cmbContent
            // 
            cmbContent.FormattingEnabled = true;
            cmbContent.Items.AddRange(new object[] { "All", "Assigned", "New", "Difference" });
            cmbContent.Location = new Point(147, 479);
            cmbContent.Name = "cmbContent";
            cmbContent.Size = new Size(121, 23);
            cmbContent.TabIndex = 13;
            cmbContent.Text = "All";
            // 
            // chkIncludeUncategorised
            // 
            chkIncludeUncategorised.CheckAlign = ContentAlignment.MiddleRight;
            chkIncludeUncategorised.Checked = true;
            chkIncludeUncategorised.CheckState = CheckState.Checked;
            chkIncludeUncategorised.Location = new Point(10, 511);
            chkIncludeUncategorised.Name = "chkIncludeUncategorised";
            chkIncludeUncategorised.Size = new Size(151, 19);
            chkIncludeUncategorised.TabIndex = 14;
            chkIncludeUncategorised.Text = "Include Unrecognised";
            toolTip1.SetToolTip(chkIncludeUncategorised, "Checking this box includes an additional section on the report that lists control bindings that could not be matched to a Controls Options item");
            chkIncludeUncategorised.UseVisualStyleBackColor = true;
            // 
            // LblInstallType
            // 
            LblInstallType.AutoSize = true;
            LblInstallType.Location = new Point(11, 29);
            LblInstallType.Name = "LblInstallType";
            LblInstallType.Size = new Size(101, 15);
            LblInstallType.TabIndex = 0;
            LblInstallType.Text = "Search Directories";
            // 
            // LinkHelp
            // 
            LinkHelp.AutoSize = true;
            LinkHelp.Font = new Font("Segoe UI", 12F);
            LinkHelp.Location = new Point(1159, 608);
            LinkHelp.Name = "LinkHelp";
            LinkHelp.Size = new Size(114, 21);
            LinkHelp.TabIndex = 16;
            LinkHelp.TabStop = true;
            LinkHelp.Text = "Web Help Page";
            LinkHelp.LinkClicked += LinkHelp_LinkClicked;
            // 
            // btnDefaultLocations
            // 
            btnDefaultLocations.BackColor = Color.LightSkyBlue;
            btnDefaultLocations.Font = new Font("Segoe UI", 9F);
            btnDefaultLocations.Location = new Point(146, 22);
            btnDefaultLocations.Name = "btnDefaultLocations";
            btnDefaultLocations.Size = new Size(163, 29);
            btnDefaultLocations.TabIndex = 18;
            btnDefaultLocations.Text = "Default Install Locations";
            btnDefaultLocations.UseVisualStyleBackColor = false;
            btnDefaultLocations.Click += BtnDefaultLocations_Click;
            // 
            // LblOr
            // 
            LblOr.AutoSize = true;
            LblOr.Location = new Point(317, 29);
            LblOr.Name = "LblOr";
            LblOr.Size = new Size(18, 15);
            LblOr.TabIndex = 19;
            LblOr.Text = "or";
            // 
            // btnCustomLocations
            // 
            btnCustomLocations.BackColor = Color.LightSkyBlue;
            btnCustomLocations.Font = new Font("Segoe UI", 9F);
            btnCustomLocations.Location = new Point(341, 22);
            btnCustomLocations.Name = "btnCustomLocations";
            btnCustomLocations.Size = new Size(163, 29);
            btnCustomLocations.TabIndex = 20;
            btnCustomLocations.Text = "Custom Install Locations";
            btnCustomLocations.UseVisualStyleBackColor = false;
            btnCustomLocations.Click += BtnCustomLocations_Click;
            // 
            // lblIncludeAircraftSpecific
            // 
            lblIncludeAircraftSpecific.AutoSize = true;
            lblIncludeAircraftSpecific.Location = new Point(185, 512);
            lblIncludeAircraftSpecific.Name = "lblIncludeAircraftSpecific";
            lblIncludeAircraftSpecific.Size = new Size(215, 15);
            lblIncludeAircraftSpecific.TabIndex = 21;
            lblIncludeAircraftSpecific.Text = "(this includes aircraft-specific bindings)";
            // 
            // cmbOutputFormat
            // 
            cmbOutputFormat.FormattingEnabled = true;
            cmbOutputFormat.Items.AddRange(new object[] { "HTML - Web Page", "CSV - Comma Separated Values" });
            cmbOutputFormat.Location = new Point(146, 538);
            cmbOutputFormat.Name = "cmbOutputFormat";
            cmbOutputFormat.Size = new Size(203, 23);
            cmbOutputFormat.TabIndex = 23;
            cmbOutputFormat.SelectedIndexChanged += cmdOutputFormat_SelectedIndexChanged;
            // 
            // LblOutputFormat
            // 
            LblOutputFormat.AutoSize = true;
            LblOutputFormat.Location = new Point(9, 541);
            LblOutputFormat.Margin = new Padding(2, 0, 2, 0);
            LblOutputFormat.Name = "LblOutputFormat";
            LblOutputFormat.Size = new Size(86, 15);
            LblOutputFormat.TabIndex = 22;
            LblOutputFormat.Text = "Output Format";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(1296, 691);
            Controls.Add(cmbOutputFormat);
            Controls.Add(LblOutputFormat);
            Controls.Add(lblIncludeAircraftSpecific);
            Controls.Add(btnCustomLocations);
            Controls.Add(LblOr);
            Controls.Add(btnDefaultLocations);
            Controls.Add(LinkHelp);
            Controls.Add(LblInstallType);
            Controls.Add(chkIncludeUncategorised);
            Controls.Add(cmbContent);
            Controls.Add(statusStrip1);
            Controls.Add(lblContent);
            Controls.Add(txtOutputFile);
            Controls.Add(LblOutputFile);
            Controls.Add(btnGenerate);
            Controls.Add(clbMappings);
            Controls.Add(lblMappings);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "Microsoft FlightSim 2020 & 2024 Controller Bindings Report";
            Shown += MainForm_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblMappings;
        public CheckedListBox clbMappings;
        public FolderBrowserDialog fbdBasePath;
        private Button btnGenerate;
        public TextBox txtOutputFile;
        private Label LblOutputFile;
        private Label lblContent;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsStatus;
        private ToolStripProgressBar tsProgress;
        private ComboBox cmbContent;
        private CheckBox chkIncludeUncategorised;
        private ToolTip toolTip1;
        private Label LblInstallType;
        public LinkLabel LinkHelp;
        private Button btnDefaultLocations;
        private Label LblOr;
        private Button btnCustomLocations;
        private Label lblIncludeAircraftSpecific;
        private ComboBox cmbOutputFormat;
        private Label LblOutputFormat;
    }
}
