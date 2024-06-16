using System.Drawing;
using System.Xml.Serialization;

namespace FSProfiles.Common.Models
{
    public enum Priority {Primary, Secondary}

    [XmlRoot("BindingList")]
    public class BindingList : List<FSContext>
    {
        [XmlIgnore]
        public List<SelectedController> SelectedControllers { get; set; }
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

        [XmlElement(ElementName = "Binding")]
        public List<FSBinding> Bindings { get; set; }

    }

    [XmlRoot(ElementName = "Binding")]
    public class FSBinding
    {
        [XmlAttribute]
        public string? Controller { get; set; }


        [XmlAttribute]
        public List<string> Keys { get; set; } = [];

        [XmlAttribute]
        public Priority Priority { get; set; }

        public string KeyCombo => string.Join(" + ", Keys);
    }
}