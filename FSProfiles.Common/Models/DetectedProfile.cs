using MSFS2020.Profiles.Common.Models.Source;

namespace MSFS2020.Profiles.Common.Models
{
    public class DetectedProfile
    {
        public string Name { get; }
        public string Path { get; }
        public ControllerDefinition ControllerDefinition { get; }

        public DetectedProfile(string path, ControllerDefinition controllerDefinition) 
        {
            Path = path;
            ControllerDefinition = controllerDefinition;
            Name = $"{ControllerDefinition.Device.DeviceName} / {ControllerDefinition.FriendlyName.Text}";
        }

        public override string ToString()
        {
            return Name; 
        }
    }
}