using System.Diagnostics;
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
            _profileList = [];
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
        }

        private void BtnBasePath_Click(object sender, EventArgs e)
        {
            SelectBasePath();
        }

        private void BtnProcessFolders_Click(object sender, EventArgs e)
        {
            _profileList = Logic.ProcessFolders(txtBasePath.Text);
            clbMappings.Items.Clear();
            foreach (var profile in _profileList)
            {
                clbMappings.Items.Add(profile);
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
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

            Logic.GenerateBindingReport(txtOutputFile.Text, generateList, contentMode, chkIncludeUncategorised.Checked);
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

        private void SetButtonHighlight(object sender)
        {
            var activeColor = Color.LimeGreen;
            BtnNative20.BackColor = sender == BtnNative20 ? activeColor : SystemColors.Control;
            BtnNative24.BackColor = sender == BtnNative24 ? activeColor : SystemColors.Control;
            BtnSteam20.BackColor = sender == BtnSteam20 ? activeColor : SystemColors.Control;
            BtnSteam24.BackColor = sender == BtnSteam24 ? activeColor : SystemColors.Control;
        }

        public void InstallTypeSelected(object sender, HostVersion hostVersion)
        {
            Logic.HostVersion = hostVersion;
            SetButtonHighlight(sender);
            var defaultFound = Logic.GetBasePath(out var basePath, out var errorMessage);
            txtBasePath.Text = basePath;
            var profileFound = false;
            if (defaultFound)
            {
                profileFound = Logic.GetProfilePath(basePath, out var profilePath, out errorMessage);
                txtBasePath.Text = profilePath;
            }
            if (profileFound)
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

        private void LinkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const string Link = "https://github.com/iadarroch/FSProfiles/wiki";

            Process.Start(new ProcessStartInfo
            {
                FileName = Link,
                UseShellExecute = true
            });
        }

        private void BtnNative20_Click(object sender, EventArgs e)
        {
            InstallTypeSelected(sender, new HostVersion{Host = InstallHost.Native, Version = InstallVersion.FS2020});
        }

        private void BtnSteam20_Click(object sender, EventArgs e)
        {
            InstallTypeSelected(sender, new HostVersion { Host = InstallHost.Steam, Version = InstallVersion.FS2020 });
        }

        private void BtnNative24_Click(object sender, EventArgs e)
        {
            InstallTypeSelected(sender, new HostVersion { Host = InstallHost.Native, Version = InstallVersion.FS2024 });
        }

        private void BtnSteam24_Click(object sender, EventArgs e)
        {
            InstallTypeSelected(sender, new HostVersion { Host = InstallHost.Steam, Version = InstallVersion.FS2024 });
        }
    }
}