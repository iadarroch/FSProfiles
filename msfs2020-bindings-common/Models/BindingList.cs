using System.Drawing;
using System.Xml.Serialization;

namespace msfs2020_bindings_common.Models;

public enum Priority {Primary, Secondary}

[XmlRoot("BindingList")]
public class BindingList : List<Context>
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

public class Context
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
    public List<Action> Actions { get; set; }
}

[XmlRoot(ElementName = "Action")]
public class Action
{
    [XmlAttribute]
    public string? ActionName { get; set; }

    [XmlElement(ElementName = "Binding")]
    public List<Binding> Bindings { get; set; }

}

[XmlRoot(ElementName = "Binding")]
public class Binding
{
    [XmlAttribute]
    public string? Controller { get; set; }


    [XmlAttribute]
    public List<string> Keys { get; set; } = [];

    [XmlAttribute]
    public Priority Priority { get; set; }

    public string KeyCombo => string.Join(" + ", Keys);
}