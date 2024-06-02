using msfs2020_bindings_common.Models.Source;

namespace msfs2020_bindings_common.Models;

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