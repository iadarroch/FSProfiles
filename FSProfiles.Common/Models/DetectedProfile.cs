using FSProfiles.Common.Models.Source;

namespace FSProfiles.Common.Models
{
    public class DetectedProfile
    {
        public string HostVersionName { get; }
        public string Name { get; }
        public string Path { get; }
        public ControllerDefinition ControllerDefinition { get; }

        public DetectedProfile(string hostVersionName, string path, ControllerDefinition controllerDefinition)
        {
            HostVersionName = hostVersionName;
            Path = path;
            ControllerDefinition = controllerDefinition;
            Name = $"{HostVersionName} / {ControllerDefinition.Device.DeviceName} / {ControllerDefinition.FriendlyName.Text}";
        }

        public override string ToString()
        {
            return Name; 
        }
    }
}