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
            btnDefaultLocations.PerformClick();  //automatically try to process default folder locations
            txtOutputFile.Text = Logic.GetDefaultOutputFile();
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

        private void LinkHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const string Link = "https://github.com/iadarroch/FSProfiles/wiki";

            Process.Start(new ProcessStartInfo
            {
                FileName = Link,
                UseShellExecute = true
            });
        }

        private void BtnDefaultLocations_Click(object sender, EventArgs e)
        {
            SetButtonHighlight(sender);
            Logic.SetDefaultLocations();
            ScanFolders();
        }

        private void BtnCustomLocations_Click(object sender, EventArgs e)
        {
            SetButtonHighlight(sender);
            var customSelect = new InstallDirs(Logic.HostVersions);
            if (customSelect.ShowDialog(this) == DialogResult.OK)
            {
                ScanFolders();
            }
        }

        private void SetButtonHighlight(object sender)
        {
            var activeColor = Color.LimeGreen;
            var inactiveColor = Color.LightSkyBlue;
            btnDefaultLocations.BackColor = sender == btnDefaultLocations ? activeColor : inactiveColor;
            btnCustomLocations.BackColor = sender == btnCustomLocations ? activeColor : inactiveColor;
        }

        private void ScanFolders()
        {
            //Now scan folders
            _profileList = Logic.ProcessHostVersions();
            clbMappings.Items.Clear();
            foreach (var profile in _profileList)
            {
                clbMappings.Items.Add(profile);
            }
        }
    }
}