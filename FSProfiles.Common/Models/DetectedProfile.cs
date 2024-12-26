using FSProfiles.Common.Classes;
using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Models
{
    public class DetectedProfile
    {
        public HostVersionType HostVersion { get; }
        public string HostVersionName { get; }
        public string Name { get; }
        public string Path { get; }
        public ControllerDefinition ControllerDefinition { get; }

        public DetectedProfile(HostVersionType hostVersion, string hostVersionName, string path, ControllerDefinition controllerDefinition)
        {
            HostVersion = hostVersion;
            HostVersionName = hostVersionName;
            Path = path;
            ControllerDefinition = controllerDefinition;
            Name = $"{HostVersionName} / {ControllerDefinition.Device.DeviceName} / {ControllerDefinition.FriendlyName.Text}";
        }

        public override string ToString()
        {
            return Name; 
        }

        public bool Is2024Version => HostVersion is HostVersionType.Native2024 or HostVersionType.Steam2024;
    }
}