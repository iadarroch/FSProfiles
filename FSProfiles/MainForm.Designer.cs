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
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblBasePath
            // 
            lblBasePath.AutoSize = true;
            lblBasePath.Location = new Point(11, 44);
            lblBasePath.Margin = new Padding(2, 0, 2, 0);
            lblBasePath.Name = "lblBasePath";
            lblBasePath.Size = new Size(58, 15);
            lblBasePath.TabIndex = 0;
            lblBasePath.Text = "Base Path";
            // 
            // txtBasePath
            // 
            txtBasePath.Location = new Point(81, 41);
            txtBasePath.Margin = new Padding(2);
            txtBasePath.Name = "txtBasePath";
            txtBasePath.Size = new Size(900, 23);
            txtBasePath.TabIndex = 1;
            // 
            // btnBasePath
            // 
            btnBasePath.Location = new Point(992, 38);
            btnBasePath.Margin = new Padding(2);
            btnBasePath.Name = "btnBasePath";
            btnBasePath.Size = new Size(130, 27);
            btnBasePath.TabIndex = 2;
            btnBasePath.Text = "&Select Profiles Path";
            btnBasePath.UseVisualStyleBackColor = true;
            btnBasePath.Click += BtnBasePath_Click;
            // 
            // btnProcessFolders
            // 
            btnProcessFolders.Location = new Point(485, 80);
            btnProcessFolders.Margin = new Padding(2);
            btnProcessFolders.Name = "btnProcessFolders";
            btnProcessFolders.Size = new Size(111, 27);
            btnProcessFolders.TabIndex = 3;
            btnProcessFolders.Text = "&Process Folders";
            btnProcessFolders.UseVisualStyleBackColor = true;
            btnProcessFolders.Click += BtnProcessFolders_Click;
            // 
            // lblMappings
            // 
            lblMappings.AutoSize = true;
            lblMappings.Location = new Point(11, 131);
            lblMappings.Margin = new Padding(2, 0, 2, 0);
            lblMappings.Name = "lblMappings";
            lblMappings.Size = new Size(110, 15);
            lblMappings.TabIndex = 4;
            lblMappings.Text = "Detected Mappings";
            // 
            // clbMappings
            // 
            clbMappings.CheckOnClick = true;
            clbMappings.FormattingEnabled = true;
            clbMappings.Location = new Point(148, 131);
            clbMappings.Name = "clbMappings";
            clbMappings.Size = new Size(833, 292);
            clbMappings.TabIndex = 5;
            // 
            // btnGenerate
            // 
            btnGenerate.Location = new Point(147, 535);
            btnGenerate.Margin = new Padding(2);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(163, 27);
            btnGenerate.TabIndex = 11;
            btnGenerate.Text = "&Generate Binding Report";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += BtnGenerate_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(147, 442);
            txtOutputFile.Margin = new Padding(2);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(833, 23);
            txtOutputFile.TabIndex = 7;
            // 
            // lblOutputFile
            // 
            lblOutputFile.AutoSize = true;
            lblOutputFile.Location = new Point(11, 445);
            lblOutputFile.Margin = new Padding(2, 0, 2, 0);
            lblOutputFile.Name = "lblOutputFile";
            lblOutputFile.Size = new Size(66, 15);
            lblOutputFile.TabIndex = 6;
            lblOutputFile.Text = "Output File";
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new Point(11, 479);
            lblContent.Margin = new Padding(2, 0, 2, 0);
            lblContent.Name = "lblContent";
            lblContent.Size = new Size(71, 15);
            lblContent.TabIndex = 8;
            lblContent.Text = "List Content";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsStatus, tsProgress });
            statusStrip1.Location = new Point(0, 585);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1137, 22);
            statusStrip1.TabIndex = 12;
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
            cmbContent.Location = new Point(148, 476);
            cmbContent.Name = "cmbContent";
            cmbContent.Size = new Size(121, 23);
            cmbContent.TabIndex = 9;
            cmbContent.Text = "All";
            // 
            // chkIncludeUncategorised
            // 
            chkIncludeUncategorised.CheckAlign = ContentAlignment.MiddleRight;
            chkIncludeUncategorised.Location = new Point(11, 507);
            chkIncludeUncategorised.Name = "chkIncludeUncategorised";
            chkIncludeUncategorised.Size = new Size(151, 19);
            chkIncludeUncategorised.TabIndex = 10;
            chkIncludeUncategorised.Text = "Include Unrecognised";
            toolTip1.SetToolTip(chkIncludeUncategorised, "Checking this box includes an additional section on the report that lists control bindings that could not be matched to a Controls Options item");
            chkIncludeUncategorised.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1137, 607);
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
    }
}
