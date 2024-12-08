using FSProfiles.Common.Classes;

namespace FSProfiles
{
    public partial class InstallDirs : Form
    {
        private readonly Dictionary<HostVersionType, FolderProcessorInstance> _hostVersions;

        public InstallDirs(Dictionary<HostVersionType, FolderProcessorInstance> hostVersions)
        {
            InitializeComponent();
            _hostVersions = hostVersions;
            InitialiseLocations();
        }

        public void InitialiseLocations()
        {
            txtNative2020.Text = _hostVersions[HostVersionType.Native2020].Path;
            txtNative2024.Text = _hostVersions[HostVersionType.Native2024].Path;
            txtSteam2020.Text = _hostVersions[HostVersionType.Steam2020].Path;
            txtSteam2024.Text = _hostVersions[HostVersionType.Steam2024].Path;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _hostVersions[HostVersionType.Native2020].Path = txtNative2020.Text;
            _hostVersions[HostVersionType.Native2024].Path = txtNative2024.Text;
            _hostVersions[HostVersionType.Steam2020].Path = txtSteam2020.Text;
            _hostVersions[HostVersionType.Steam2024].Path = txtSteam2024.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            foreach (var hostVersion in _hostVersions)
            {
                hostVersion.Value.SetDefaultPath();
            }

            InitialiseLocations();
        }
    }
}
