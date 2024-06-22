using System.Drawing;
using System.Xml.Serialization;

namespace FSProfiles.Common.Models
{
    public enum Priority {Primary, Secondary}

    [XmlRoot("BindingList")]
    public class BindingList
    {
        public List<SelectedController> SelectedControllers { get; set; }
        [XmlElement(ElementName = "Context")]
        public List<FSContext> Contexts { get; set; }
    }

    public class SelectedController
    {
        public string? DeviceName { get; set; }
        public string? ProfileName { get; set; }
        public string? ProfilePath { get; set; }
    }

    [XmlRoot(ElementName = "Context")]
    public class FSContext
    {
        [XmlAttribute]
        public string? ContextName { get; set; }

        [XmlIgnore]
        public Color? BackColor { get; set; }

        [XmlAttribute(AttributeName = "BackColor")]
        public string BackColorAsArgb
        {
            get
            {
                if (this.BackColor == null) return "";
                return ColorTranslator.ToHtml(BackColor.Value);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    BackColor = null;
                }
                else
                {
                    BackColor = ColorTranslator.FromHtml(value);
                }
            }
        }

        [XmlElement(ElementName = "Action")]
        public List<FSAction> Actions { get; set; }
    }

    [XmlRoot(ElementName = "Action")]
    public class FSAction
    {
        [XmlAttribute]
        public string? ActionName { get; set; }

        [XmlIgnore]
        public Color? BackColor { get; set; }

        [XmlAttribute(AttributeName = "BackColor")]
        public string BackColorAsArgb
        {
            get
            {
                if (this.BackColor == null) return "";
                return ColorTranslator.ToHtml(BackColor.Value);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    BackColor = null;
                }
                else
                {
                    BackColor = ColorTranslator.FromHtml(value);
                }
            }
        }

        [XmlElement(ElementName = "Binding")]
        public List<FSBinding> Bindings { get; set; }

    }

    [XmlRoot(ElementName = "Binding")]
    public class FSBinding
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