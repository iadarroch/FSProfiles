using FSControls.Models.Source;

namespace FSControls.Models
{
    public class DetectedController
    {
        public string Name { get; }
        public ControllerDefinition ControllerDefinition { get; }

        public DetectedController(ControllerDefinition controllerDefinition) 
        {
            ControllerDefinition = controllerDefinition;
            Name = $"{ControllerDefinition.Device.DeviceName} / {ControllerDefinition.FriendlyName.Text}";
        }

        public override string ToString()
        {
            return Name; 
        }
    }
}
