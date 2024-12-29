namespace FSProfiles
{
    partial class ErrorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorsForm));
            txtErrorList = new TextBox();
            btnOk = new Button();
            SuspendLayout();
            // 
            // txtErrorList
            // 
            txtErrorList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtErrorList.BackColor = SystemColors.Window;
            txtErrorList.Location = new Point(12, 12);
            txtErrorList.Multiline = true;
            txtErrorList.Name = "txtErrorList";
            txtErrorList.ReadOnly = true;
            txtErrorList.ScrollBars = ScrollBars.Both;
            txtErrorList.Size = new Size(776, 385);
            txtErrorList.TabIndex = 1;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.BackColor = Color.LightSkyBlue;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Font = new Font("Segoe UI", 9F);
            btnOk.Location = new Point(376, 410);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 29);
            btnOk.TabIndex = 0;
            btnOk.Text = "&Ok";
            btnOk.UseVisualStyleBackColor = false;
            // 
            // ErrorsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnOk);
            Controls.Add(txtErrorList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ErrorsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Errors Detected During Profile Scanning";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtErrorList;
        private Button btnOk;
    }
}