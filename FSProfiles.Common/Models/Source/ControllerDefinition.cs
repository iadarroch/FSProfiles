
using System.Xml.Serialization;

namespace FSProfiles.Common.Models.Source
{
    // using System.Xml.Serialization;
// XmlSerializer serializer = new XmlSerializer(typeof(Root));
// using (StringReader reader = new StringReader(xml))
// {
//    var test = (Root)serializer.Deserialize(reader);
// }

    [XmlRoot(ElementName = "Version")]
    public class Version
    {

        [XmlAttribute(AttributeName = "Num")]
        public int Num { get; set; }
    }

    [XmlRoot(ElementName = "FriendlyName")]
    public class FriendlyName
    {

        [XmlAttribute(AttributeName = "PlatformAvailability")]
        public int PlatformAvailability { get; set; }

        [XmlText]
        public string Text { get; set; } = string.Empty;
    }

    [XmlRoot(ElementName = "Axis")]
    public class Axis
    {

        [XmlAttribute(AttributeName = "AxisName")]
        public string AxisName { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "AxisSensitivy")]
        public int AxisSensitivy { get; set; }

        [XmlAttribute(AttributeName = "AxisSensitivyMinus")]
        public int AxisSensitivyMinus { get; set; }

        [XmlAttribute(AttributeName = "AxisNeutral")]
        public int AxisNeutral { get; set; }

        [XmlAttribute(AttributeName = "AxisDeadZone")]
        public int AxisDeadZone { get; set; }

        [XmlAttribute(AttributeName = "AxisOutDeadZone")]
        public int AxisOutDeadZone { get; set; }

        [XmlAttribute(AttributeName = "AxisResponseRate")]
        public int AxisResponseRate { get; set; }
    }

    [XmlRoot(ElementName = "Axes")]
    public class Axes
    {

        [XmlElement(ElementName = "Axis")]
        public List<Axis> Axis { get; set; } = [];
    }

    [XmlRoot(ElementName = "KEY")]
    public class Key
    {

        [XmlAttribute(AttributeName = "Information")]
        public string Information { get; set; } = "";

        [XmlText]
        public int Text { get; set; }
    }

    [XmlRoot(ElementName = "Primary")]
    public class Primary
    {

        [XmlElement(ElementName = "KEY")]
        public List<Key> Keys { get; set; } = [];
    }

    [XmlRoot(ElementName = "Action")]
    public class Action
    {

        [XmlElement(ElementName = "Primary")]
        public Primary? Primary { get; set; }

        [XmlAttribute(AttributeName = "ActionName")]
        public string ActionName { get; set; } = "";

        [XmlAttribute(AttributeName = "Flag")]
        public int Flag { get; set; }

        [XmlText]
        public string? Text { get; set; }

        [XmlElement(ElementName = "Secondary")]
        public Secondary? Secondary { get; set; }
    }

    [XmlRoot(ElementName = "Context")]
    public class Context
    {

        [XmlElement(ElementName = "Action")] public List<Action> Actions { get; set; } = [];

        [XmlAttribute(AttributeName = "ContextName")]
        public string ContextName { get; set; } = "";

        [XmlText]
        public string? Text { get; set; }
    }

    [XmlRoot(ElementName = "Secondary")]
    public class Secondary
    {

        [XmlElement(ElementName = "KEY")]
        public List<Key> Keys { get; set; } = [];
    }

    [XmlRoot(ElementName = "Device")]
    public class Device
    {

        [XmlElement(ElementName = "Axes")] 
        public Axes Axes { get; set; } = new();

        [XmlElement(ElementName = "Context")] 
        public List<Context> Context { get; set; } = [];

        [XmlAttribute(AttributeName = "DeviceName")]
        public string DeviceName { get; set; } = "";

        [XmlAttribute(AttributeName = "GUID")]
        public string GUID { get; set; } = "";

        [XmlAttribute(AttributeName = "ProductID")]
        public int ProductId { get; set; }

        [XmlAttribute(AttributeName = "CompositeId")]
        public int CompositeId { get; set; }

        [XmlText]
        public string? Text { get; set; }
    }

    [XmlRoot(ElementName = "ControllerDefinition")]
    public class ControllerDefinition
    {

        [XmlElement(ElementName = "Version")]
        public Version Version { get; set; } = new();

        [XmlElement(ElementName = "FriendlyName")]
        public FriendlyName FriendlyName { get; set; } = new();

        [XmlElement(ElementName = "Device")]
        public Device Device { get; set; } = new();
    }
}