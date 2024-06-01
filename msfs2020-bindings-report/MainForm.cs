using FSControls.Classes;

namespace FSControls
{
    public partial class MainForm : Form
    {
        public enum FocusControl { btnBasePath, btnProcessFolders }

        public MainLogic Logic { get; }

        public MainForm()
        {
            InitializeComponent();
            Logic = new MainLogic(this);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Logic.GetDefaultPath())
            {
                btnProcessFolders.Focus();
                btnProcessFolders.PerformClick();
                Application.DoEvents();
                clbMappings.SetItemChecked(1, true);
                clbMappings.SetItemChecked(3, true);
                clbMappings.SetItemChecked(6, true);
                clbMappings.SetItemChecked(9, true);
            }
            else
            {
                btnBasePath.Focus();
            }

            txtOutputFile.Text = Logic.GetDefaultOutputFile();
        }

        private void btnBasePath_Click(object sender, EventArgs e)
        {
            Logic.SelectBasePath(txtBasePath, fbdBasePath);
        }

        private void btnProcessFolders_Click(object sender, EventArgs e)
        {
            Logic.ProcessFolders(txtBasePath, clbMappings);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Logic.GenerateMappingList(clbMappings);
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            Logic.RebuildKnownBindings(clbMappings);
        }

        public void StartProgress(string message, int progressMax)
        {
            tsStatus.Text = message;
            tsProgress.Maximum = progressMax;
            tsProgress.Visible = true;
            Application.DoEvents();
        }

        public void UpdateProgress(string? message = null, int? progress = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                tsStatus.Text = message;
            }

            if (progress.HasValue)
            {
                tsProgress.Value = progress.Value;
            }
            Application.DoEvents();
        }

        public void StopProgress(string message)
        {
            tsStatus.Text = message;
            tsProgress.Visible = false;
            Application.DoEvents();
        }
    }
}
