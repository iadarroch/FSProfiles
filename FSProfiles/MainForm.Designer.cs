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
            lblBasePath = new Label();
            txtBasePath = new TextBox();
            btnBasePath = new Button();
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
            BtnNative20 = new Button();
            BtnSteam20 = new Button();
            LblOr1 = new Label();
            LblOr2 = new Label();
            BtnSteam24 = new Button();
            BtnNative24 = new Button();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblBasePath
            // 
            lblBasePath.AutoSize = true;
            lblBasePath.Location = new Point(11, 98);
            lblBasePath.Margin = new Padding(2, 0, 2, 0);
            lblBasePath.Name = "lblBasePath";
            lblBasePath.Size = new Size(58, 15);
            lblBasePath.TabIndex = 4;
            lblBasePath.Text = "Base Path";
            // 
            // txtBasePath
            // 
            txtBasePath.Location = new Point(81, 95);
            txtBasePath.Margin = new Padding(2);
            txtBasePath.Name = "txtBasePath";
            txtBasePath.Size = new Size(900, 23);
            txtBasePath.TabIndex = 5;
            // 
            // btnBasePath
            // 
            btnBasePath.Location = new Point(992, 92);
            btnBasePath.Margin = new Padding(2);
            btnBasePath.Name = "btnBasePath";
            btnBasePath.Size = new Size(130, 27);
            btnBasePath.TabIndex = 6;
            btnBasePath.Text = "Select &Profiles Path";
            btnBasePath.UseVisualStyleBackColor = true;
            btnBasePath.Click += BtnBasePath_Click;
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
            LblInstallType.Size = new Size(99, 15);
            LblInstallType.TabIndex = 0;
            LblInstallType.Text = "Select Install Type";
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
            // BtnNative20
            // 
            BtnNative20.BackColor = SystemColors.Control;
            BtnNative20.Location = new Point(147, 23);
            BtnNative20.Margin = new Padding(2);
            BtnNative20.Name = "BtnNative20";
            BtnNative20.Size = new Size(169, 27);
            BtnNative20.TabIndex = 1;
            BtnNative20.Text = "Native Windows FS2020";
            BtnNative20.UseVisualStyleBackColor = false;
            BtnNative20.Click += BtnNative20_Click;
            // 
            // BtnSteam20
            // 
            BtnSteam20.BackColor = SystemColors.Control;
            BtnSteam20.Location = new Point(342, 23);
            BtnSteam20.Margin = new Padding(2);
            BtnSteam20.Name = "BtnSteam20";
            BtnSteam20.Size = new Size(169, 27);
            BtnSteam20.TabIndex = 3;
            BtnSteam20.Text = "Steam FS2020";
            BtnSteam20.UseVisualStyleBackColor = false;
            BtnSteam20.Click += BtnSteam20_Click;
            // 
            // LblOr1
            // 
            LblOr1.AutoSize = true;
            LblOr1.Location = new Point(321, 29);
            LblOr1.Name = "LblOr1";
            LblOr1.Size = new Size(18, 15);
            LblOr1.TabIndex = 2;
            LblOr1.Text = "or";
            // 
            // LblOr2
            // 
            LblOr2.AutoSize = true;
            LblOr2.Location = new Point(321, 60);
            LblOr2.Name = "LblOr2";
            LblOr2.Size = new Size(18, 15);
            LblOr2.TabIndex = 19;
            LblOr2.Text = "or";
            // 
            // BtnSteam24
            // 
            BtnSteam24.BackColor = SystemColors.Control;
            BtnSteam24.Location = new Point(342, 54);
            BtnSteam24.Margin = new Padding(2);
            BtnSteam24.Name = "BtnSteam24";
            BtnSteam24.Size = new Size(169, 27);
            BtnSteam24.TabIndex = 20;
            BtnSteam24.Text = "Steam FS2024";
            BtnSteam24.UseVisualStyleBackColor = false;
            BtnSteam24.Click += BtnSteam24_Click;
            // 
            // BtnNative24
            // 
            BtnNative24.BackColor = SystemColors.Control;
            BtnNative24.Location = new Point(147, 54);
            BtnNative24.Margin = new Padding(2);
            BtnNative24.Name = "BtnNative24";
            BtnNative24.Size = new Size(169, 27);
            BtnNative24.TabIndex = 18;
            BtnNative24.Text = "Native Windows FS2024";
            BtnNative24.UseVisualStyleBackColor = false;
            BtnNative24.Click += BtnNative24_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1137, 661);
            Controls.Add(LblOr2);
            Controls.Add(BtnSteam24);
            Controls.Add(BtnNative24);
            Controls.Add(LblOr1);
            Controls.Add(BtnSteam20);
            Controls.Add(BtnNative20);
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
            Controls.Add(btnBasePath);
            Controls.Add(txtBasePath);
            Controls.Add(lblBasePath);
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
        private Label lblBasePath;
        private Button btnBasePath;
        private Label lblMappings;
        public TextBox txtBasePath;
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
        public Button BtnNative20;
        public Button BtnSteam20;
        private Label LblOr1;
        private Label LblOr2;
        public Button BtnSteam24;
        public Button BtnNative24;
    }
}
