using System.Drawing;
using System.Xml.Serialization;

namespace FSProfiles.Common.Models
{
    public class ColoredItem
    {
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

    }
}
