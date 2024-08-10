using System.Xml.Serialization;

namespace FSProfiles.Common.Models
{
    public enum Priority {Primary, Secondary}

    [XmlRoot("BindingList")]
    public class BindingList
    {
        [XmlElement(nameof(Section), typeof(Section))]
        public List<Section> Sections { get; set; } = [];
    }

    public class SelectedController
    {
        public string? DeviceName { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfilePath { get; set; }
    }

    [XmlRoot(ElementName = "Section")]
    public class Section : ColoredItem
    {
        [XmlAttribute]
        public string? SectionName { get; set; }

        [XmlElement(nameof(SubSection), typeof(SubSection))]
        [XmlElement(nameof(SectionAction), typeof(SectionAction))]
        public List<SectionItem> Items { get; set; } = [];
    }

    [XmlRoot(ElementName = "SectionItem")]
    public class SectionItem : ColoredItem
    { }

    [XmlRoot(ElementName = "SubSection")]
    public class SubSection : SectionItem
    {
        [XmlAttribute]
        public string? SubSectionName { get; set; }

        [XmlElement(ElementName = "SectionAction")]
        public List<SectionAction> Actions { get; set; } = [];
    }


    [XmlRoot(ElementName = "SectionAction")]
    public class SectionAction : SectionItem
    {
        [XmlAttribute] public string ActionName { get; set; } = string.Empty;

        [XmlElement(ElementName = "ActionInput")]
        public List<ActionInput> Inputs { get; set; } = [];
    }

    public class ActionInput
    {
        [XmlAttribute] 
        public string InputKey { get; set; } = string.Empty;

        [XmlElement(ElementName = "Binding")]
        public List<InputBinding> Bindings { get; set; } = [];
    }

    [XmlRoot(ElementName = "Binding")]
    public class InputBinding
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