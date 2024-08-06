using System.Xml.Serialization;
using FSProfiles.Common.Classes;

namespace FSProfiles.Common.Models
{
    public class BindingReport
    {
        public ContentMode ContentMode { get; set; }

        public List<SelectedController> SelectedControllers { get; set; } = [];

        public BindingList BindingList { get; set; } = new();

        [XmlArrayItem(ElementName = "Context")]
        public List<UnrecognisedContext> UnrecognisedContexts { get; set; } = [];
    }

    [XmlRoot(ElementName = "Context")]
    public class UnrecognisedContext : ColoredItem
    {
        [XmlAttribute]
        public string? ContextName { get; set; }

        [XmlElement(ElementName = "Action")]
        public List<UnrecognisedAction> Actions { get; set; } = [];
    }

    [XmlRoot(ElementName = "Action")]
    public class UnrecognisedAction : ColoredItem
    {
        [XmlAttribute]
        public string? ActionName { get; set; }

        [XmlElement(ElementName = "Binding")]
        public List<UnrecognisedBinding> Bindings { get; set; } = [];
    }


    [XmlRoot(ElementName = "Binding")]
    public class UnrecognisedBinding
    {
        [XmlAttribute]
        public string? Controller { get; set; }

        [XmlAttribute]
        public string? Profile { get; set; }

        [XmlAttribute]
        public Priority Priority { get; set; }

        [XmlIgnore]
        public List<string> Keys { get; set; } = [];

        [XmlAttribute]
        public string KeyCombo
        {
            get => string.Join(" + ", Keys);
            set => Keys = value.Split('+').Select(k => k.Trim()).ToList();
        }
    }

}
