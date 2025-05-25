using FSProfiles.Common.Classes;
using FSProfiles.Common.Models.Source;
using System.Globalization;

namespace FSProfiles.Common.Models
{
    public class DetectedProfile
    {
        public HostVersionType HostVersion { get; }
        public string HostVersionName { get; }
        public string Name { get; }
        public string Path { get; }
        public string Type { get; }
        public ControllerDefinition ControllerDefinition { get; }

        public DetectedProfile(HostVersionType hostVersion, string hostVersionName, string path, ControllerDefinition controllerDefinition)
        {
            HostVersion = hostVersion;
            HostVersionName = hostVersionName;
            Path = path;
            ControllerDefinition = controllerDefinition;
            Type = !Is2024Version
                ? "2020"
                : !string.IsNullOrEmpty(ControllerDefinition.Device.AircraftInfo.AircraftPackageName)
                    ? ToTitleCase(ControllerDefinition.Device.AircraftInfo.AircraftPackageName)
                    : !string.IsNullOrEmpty(ControllerDefinition.Device.AircraftInfo.CategoryName)
                        ? ToTitleCase(ControllerDefinition.Device.AircraftInfo.CategoryName)
                        : "General";

            Name = Is2024Version 
                ? $"{HostVersionName} / {ControllerDefinition.Device.DeviceName} / {Type} / {ControllerDefinition.FriendlyName.Text}"
                : $"{HostVersionName} / {ControllerDefinition.Device.DeviceName} / {ControllerDefinition.FriendlyName.Text}";
        }

        public override string ToString()
        {
            return Name; 
        }

        public bool Is2024Version => HostVersion is HostVersionType.Native2024 or HostVersionType.Steam2024;

        private string ToTitleCase(string source)
        {
            source = source.Replace('-', ' ');
            return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(source.ToLower());
        }

    }
}