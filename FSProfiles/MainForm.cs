using FSProfiles.Common.Classes;
using FSProfiles.Common.Models;

namespace FSProfiles
{
    public partial class MainForm : Form
    {
        private readonly ProgramArguments _programArguments;
        public MainLogic Logic { get; }
        public List<DetectedProfile> _profileList;

        public MainForm(ProgramArguments programArguments)
        {
            _programArguments = programArguments;
            InitializeComponent();
            Logic = new MainLogic(_programArguments);
            Logic.OnStart += OnStart;
            Logic.OnProgress += OnProgress;
            Logic.OnStop += OnStop;
            _profileList = new List<DetectedProfile>();
            //Developer option: uncomment the following line to rebuild the list of "Known" bindings
            //It will output the new file in C:\Temp\KnownBindings.xml. You will need to manually move
            //to correct program location
            btnRebuild.Visible = _programArguments.Rebuild;
        }

        private void OnStart(object? sender, ProgressEvent e)
        {
            tsStatus.Text = e.Message;
            tsProgress.Maximum = e.Progress ?? 0;
            tsProgress.Visible = true;
            Application.DoEvents();
        }

        private void OnProgress(object? sender, ProgressEvent e)
        {
            if (!string.IsNullOrEmpty(e.Message))
            {
                tsStatus.Text = e.Message;
            }

            if (e.Progress.HasValue)
            {
                tsProgress.Value = e.Progress.Value;
            }
            Application.DoEvents();
        }

        private void OnStop(object? sender, ProgressEvent e)
        {
            tsStatus.Text = e.Message;
            tsProgress.Visible = false;
            Application.DoEvents();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            var defaultFound = Logic.GetProfilePath(out var basePath, out var errorMessage);
            txtBasePath.Text = basePath;
            if (defaultFound)
            {
                //if able to determine the path, automatically process it and set focus to list
                btnProcessFolders.PerformClick();
                foreach (var profileNumber in _programArguments.ProfileSelection)
                {
                    clbMappings.SetItemChecked(profileNumber, true);
                }
                clbMappings.Focus();
            }
            else
            {
                MessageBox.Show(errorMessage, "Data Path Detection Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnBasePath.Focus();
            }

            txtOutputFile.Text = Logic.GetDefaultOutputFile();
        }

        private void btnBasePath_Click(object sender, EventArgs e)
        {
            SelectBasePath();
        }

        private void btnProcessFolders_Click(object sender, EventArgs e)
        {
            _profileList = Logic.ProcessFolders(txtBasePath.Text);
            clbMappings.Items.Clear();
            foreach (var profile in _profileList)
            {
                clbMappings.Items.Add(profile);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var contentMode = (ContentMode)cmbContent.SelectedIndex;
            var generateList = new List<DetectedProfile>();
            for (var index = 0; index < clbMappings.Items.Count; index++)
            {
                if (clbMappings.GetItemChecked(index))
                {
                    generateList.Add(_profileList[index]);
                }
            }

            Logic.GenerateBindingReport(txtOutputFile.Text, generateList, contentMode);
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            Logic.RebuildKnownBindings(@"C:\Temp\KnownBindings.xml", _profileList);
        }

        public void SelectBasePath()
        {
            fbdBasePath.Description = "Choose path to base of MSFS 2020 files";
            fbdBasePath.RootFolder = Environment.SpecialFolder.MyComputer;
            fbdBasePath.InitialDirectory = txtBasePath.Text;
            var dialogResult = fbdBasePath.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtBasePath.Text = fbdBasePath.SelectedPath;
            }
        }
    }
}