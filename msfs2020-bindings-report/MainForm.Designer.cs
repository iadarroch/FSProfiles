namespace FSControls
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
            btnRebuild = new Button();
            lblContent = new Label();
            statusStrip1 = new StatusStrip();
            tsStatus = new ToolStripStatusLabel();
            tsProgress = new ToolStripProgressBar();
            cmbContent = new ComboBox();
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
            btnBasePath.Size = new Size(118, 27);
            btnBasePath.TabIndex = 2;
            btnBasePath.Text = "Select Profiles Path";
            btnBasePath.UseVisualStyleBackColor = true;
            btnBasePath.Click += btnBasePath_Click;
            // 
            // btnProcessFolders
            // 
            btnProcessFolders.Location = new Point(485, 80);
            btnProcessFolders.Margin = new Padding(2);
            btnProcessFolders.Name = "btnProcessFolders";
            btnProcessFolders.Size = new Size(111, 27);
            btnProcessFolders.TabIndex = 3;
            btnProcessFolders.Text = "Process Folders";
            btnProcessFolders.UseVisualStyleBackColor = true;
            btnProcessFolders.Click += btnProcessFolders_Click;
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
            btnGenerate.Location = new Point(405, 533);
            btnGenerate.Margin = new Padding(2);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(157, 27);
            btnGenerate.TabIndex = 10;
            btnGenerate.Text = "Generate Binding List";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtOutputFile
            // 
            txtOutputFile.Location = new Point(87, 442);
            txtOutputFile.Margin = new Padding(2);
            txtOutputFile.Name = "txtOutputFile";
            txtOutputFile.Size = new Size(894, 23);
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
            // btnRebuild
            // 
            btnRebuild.Location = new Point(953, 533);
            btnRebuild.Margin = new Padding(2);
            btnRebuild.Name = "btnRebuild";
            btnRebuild.Size = new Size(157, 27);
            btnRebuild.TabIndex = 11;
            btnRebuild.Text = "Rebuild Known Mappings";
            btnRebuild.UseVisualStyleBackColor = true;
            btnRebuild.Visible = false;
            btnRebuild.Click += btnRebuild_Click;
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
            statusStrip1.Location = new Point(0, 613);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1123, 22);
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
            cmbContent.Location = new Point(87, 476);
            cmbContent.Name = "cmbContent";
            cmbContent.Size = new Size(121, 23);
            cmbContent.TabIndex = 9;
            cmbContent.Text = "All";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1123, 635);
            Controls.Add(cmbContent);
            Controls.Add(statusStrip1);
            Controls.Add(lblContent);
            Controls.Add(btnRebuild);
            Controls.Add(txtOutputFile);
            Controls.Add(lblOutputFile);
            Controls.Add(btnGenerate);
            Controls.Add(clbMappings);
            Controls.Add(lblMappings);
            Controls.Add(btnProcessFolders);
            Controls.Add(btnBasePath);
            Controls.Add(txtBasePath);
            Controls.Add(lblBasePath);
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
        private Button btnRebuild;
        private Label lblContent;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsStatus;
        private ToolStripProgressBar tsProgress;
        private ComboBox cmbContent;
    }
}
