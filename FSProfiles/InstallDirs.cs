using FSProfiles.Common.Classes;
using FSProfiles.Common.Models;

namespace FSProfiles
{
    public partial class InstallDirs : Form
    {
        private readonly Dictionary<HostVersionType, FolderProcessorInstance> _hostVersions;
        private readonly IUserSettingsRepo _userSettingsRepo;
        private readonly UserSettings _userSettings;

        public InstallDirs(Dictionary<HostVersionType, FolderProcessorInstance> hostVersions, IUserSettingsRepo userSettingsRepo)
        {
            InitializeComponent();
            _hostVersions = hostVersions;
            _userSettingsRepo = userSettingsRepo;
            _userSettings = _userSettingsRepo.GetSettings();
            InitialiseLocations(true);
        }

        private string LocationPath(bool useSaved, HostVersionType hostVersionType)
        {
            if (useSaved && 
                !string.IsNullOrEmpty(_userSettings.HostVersionPath[hostVersionType]))
                return _userSettings.HostVersionPath[hostVersionType];
            return _hostVersions[hostVersionType].ProfilePath;
        }

        public void InitialiseLocations(bool useSaved)
        {
            txtNative2020.Text = LocationPath(useSaved, HostVersionType.Native2020);
            txtNative2024.Text = LocationPath(useSaved, HostVersionType.Native2024);
            txtSteam2020.Text = LocationPath(useSaved, HostVersionType.Steam2020);
            txtSteam2024.Text = LocationPath(useSaved, HostVersionType.Steam2024);
        }

        public void SelectBasePath(FolderProcessorInstance processorInstance, TextBox txtPath)
        {
            fldCustomPath.Description = $"Choose path to base of {processorInstance.FolderProcessor.HostVersionName} files";
            fldCustomPath.RootFolder = Environment.SpecialFolder.MyComputer;
            fldCustomPath.InitialDirectory = txtPath.Text;
            var dialogResult = fldCustomPath.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtPath.Text = fldCustomPath.SelectedPath;
            }
        }

        private void BtnNative2020_Click(object sender, EventArgs e)
        {
            SelectBasePath(_hostVersions[HostVersionType.Native2020], txtNative2020);
        }

        private void BtnNative2024_Click(object sender, EventArgs e)
        {
            SelectBasePath(_hostVersions[HostVersionType.Native2024], txtNative2024);
        }

        private void BtnSteam2020_Click(object sender, EventArgs e)
        {
            SelectBasePath(_hostVersions[HostVersionType.Steam2020], txtSteam2020);
        }

        private void BtnSteam2024_Click(object sender, EventArgs e)
        {
            SelectBasePath(_hostVersions[HostVersionType.Steam2024], txtSteam2024);
        }

        private void SavePath(HostVersionType hostVersionType, string path)
        {
            _hostVersions[hostVersionType].ProfilePath = path;
            _userSettings.HostVersionPath[hostVersionType] = path;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePath(HostVersionType.Native2020, txtNative2020.Text);
            SavePath(HostVersionType.Native2024, txtNative2024.Text);
            SavePath(HostVersionType.Steam2020, txtSteam2020.Text);
            SavePath(HostVersionType.Steam2024, txtSteam2024.Text);
            _userSettingsRepo.SaveSettings(_userSettings);
            DialogResult = DialogResult.OK;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            foreach (var hostVersion in _hostVersions)
            {
                hostVersion.Value.SetDefaultPath();
            }

            InitialiseLocations(false);
        }
    }
}
