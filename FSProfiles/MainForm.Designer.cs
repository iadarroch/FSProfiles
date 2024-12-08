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
            btnProcessFolders = new Button();
            lblMappings = new Label();
            clbMappings = new CheckedListBox();
            btnGenerate = new Button();
            txtOutputFile = new TextBox();
            lblOutputFile = new Label();
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
            label1 = new Label();
            btnCustomLocations = new Button();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnProcessFolders
            // 
            btnProcessFolders.Location = new Point(485, 129);
            btnProcessFolders.Margin = new Padding(2);
            btnProcessFolders.Name = "btnProcessFolders";
            btnProcessFolders.Size = new Size(111, 27);
            btnProcessFolders.TabIndex = 7;
            btnProcessFolders.Text = "Process &Folders";
            btnProcessFolders.UseVisualStyleBackColor = true;
            btnProcessFolders.Click += BtnProcessFolders_Click;
            // 
            // lblMappings
            // 
            lblMappings.AutoSize = true;
            lblMappings.Location = new Point(11, 191);
            lblMappings.Margin = new Padding(2, 0, 2, 0);
            lblMappings.Name = "lblMappings";
            lblMappings.Size = new Size(110, 15);
            lblMappings.TabIndex = 8;
            lblMappings.Text = "Detected Mappings";
            // 
            // clbMappings
            // 
            clbMappings.CheckOnClick = true;
            clbMappings.FormattingEnabled = true;
            clbMappings.Location = new Point(148, 191);
            clbMappings.Name = "clbMappings";
            clbMappings.Size = new Size(833, 292);
            clbMappings.TabIndex = 9;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(147, 595);
            btnGenerate.Margin = new Padding(2);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(163, 27);
            btnGenerate.TabIndex = 15;
            btnGenerate.Text = "&Generate Binding Report";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += BtnGenerate_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(147, 502);
            txtOutputFile.Margin = new Padding(2);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(833, 23);
            txtOutputFile.TabIndex = 11;
            // 
            // lblOutputFile
            // 
            lblOutputFile.AutoSize = true;
            lblOutputFile.Location = new Point(11, 505);
            lblOutputFile.Margin = new Padding(2, 0, 2, 0);
            lblOutputFile.Name = "lblOutputFile";
            lblOutputFile.Size = new Size(66, 15);
            lblOutputFile.TabIndex = 10;
            lblOutputFile.Text = "Output File";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(11, 539);
            lblContent.Margin = new Padding(2, 0, 2, 0);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(71, 15);
            lblContent.TabIndex = 12;
            lblContent.Text = "List Content";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatus, tsProgress });
            statusStrip1.Location = new Point(0, 639);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1137, 22);
            statusStrip1.TabIndex = 17;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsStatus
            // 
            tsStatus.AutoSize = false;
            tsStatus.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsStatus.Name = "tsStatus";
            tsStatus.Size = new Size(250, 17);
            tsStatus.Text = " ";
            tsStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tsProgress
            // 
            tsProgress.Alignment = ToolStripItemAlignment.Right;
            tsProgress.AutoSize = false;
            tsProgress.Name = "tsProgress";
            tsProgress.Size = new Size(150, 16);
            tsProgress.Style = ProgressBarStyle.Continuous;
            tsProgress.Visible = false;
            // 
            // cmbContent
            // 
            cmbContent.FormattingEnabled = true;
            cmbContent.Items.AddRange(new object[] { "All", "Assigned", "New" });
            cmbContent.Location = new Point(148, 536);
            cmbContent.Name = "cmbContent";
            cmbContent.Size = new Size(121, 23);
            cmbContent.TabIndex = 13;
            cmbContent.Text = "All";
            // 
            // chkIncludeUncategorised
            // 
            chkIncludeUncategorised.CheckAlign = ContentAlignment.MiddleRight;
            chkIncludeUncategorised.Location = new Point(11, 567);
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
            LinkHelp.Location = new Point(831, 586);
            LinkHelp.Name = "LinkHelp";
            LinkHelp.Size = new Size(114, 21);
            LinkHelp.TabIndex = 16;
            LinkHelp.TabStop = true;
            LinkHelp.Text = "Web Help Page";
            LinkHelp.LinkClicked += LinkHelp_LinkClicked;
            // 
            // btnDefaultLocations
            // 
            btnDefaultLocations.Location = new Point(147, 25);
            btnDefaultLocations.Name = "btnDefaultLocations";
            btnDefaultLocations.Size = new Size(163, 23);
            btnDefaultLocations.TabIndex = 18;
            btnDefaultLocations.Text = "Default Install Locations";
            btnDefaultLocations.UseVisualStyleBackColor = true;
            btnDefaultLocations.Click += BtnDefaultLocations_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(317, 29);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 19;
            label1.Text = "or";
            // 
            // btnCustomLocations
            // 
            btnCustomLocations.Location = new Point(342, 25);
            btnCustomLocations.Name = "btnCustomLocations";
            btnCustomLocations.Size = new Size(163, 23);
            btnCustomLocations.TabIndex = 20;
            btnCustomLocations.Text = "Custom Install Locations";
            btnCustomLocations.UseVisualStyleBackColor = true;
            btnCustomLocations.Click += BtnCustomLocations_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1137, 661);
            Controls.Add(btnCustomLocations);
            Controls.Add(label1);
            Controls.Add(btnDefaultLocations);
            Controls.Add(LinkHelp);
            Controls.Add(LblInstallType);
            Controls.Add(chkIncludeUncategorised);
            Controls.Add(cmbContent);
            Controls.Add(statusStrip1);
            Controls.Add(lblContent);
            Controls.Add(txtOutputFile);
            Controls.Add(lblOutputFile);
            Controls.Add(btnGenerate);
            Controls.Add(clbMappings);
            Controls.Add(lblMappings);
            Controls.Add(btnProcessFolders);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "FlightSim 2020 Controller Display";
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
        public Button btnProcessFolders;
        public TextBox txtOutputFile;
        private Label lblOutputFile;
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
        private Label label1;
        private Button btnCustomLocations;
    }
}
