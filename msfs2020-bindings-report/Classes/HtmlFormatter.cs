using System;
using System.Globalization;
using System.Text;
using FSControls.Models;

namespace FSControls.Classes
{
    public class HtmlFormatter
    {
        private static readonly TextInfo TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
        private static List<Color> _defaultColors = 
            [Color.Aquamarine, Color.LightCoral, Color.LightSkyBlue, Color.Wheat, 
             Color.Plum,Color.LemonChiffon, Color.SandyBrown, Color.LightPink,
             Color.LightGreen, Color.Bisque, Color.Thistle, Color.PowderBlue,
             Color.LightGoldenrodYellow, Color.Lavender, Color.NavajoWhite, 
             Color.RosyBrown, Color.LightSalmon, Color.GreenYellow, Color.PaleTurquoise,
             Color.Tan, Color.DarkMagenta];
        private int _colorIndex = 0;

        public HtmlFormatter()
        {
        }

        public void ConvertToHtml(BindingList bindingList, string fileName)
        {
            _colorIndex = 0;
            var sb = new StringBuilder();
            sb.AppendLine("<html>");
            sb.AppendLine(Header);
            sb.AppendLine("<body>");

            //Add document headers
            sb.AppendLine("<h1>Microsoft Flight Simulator 2020 Bindings Report</h1>");
            sb.AppendLine("<div>");
            sb.AppendLine("<p>Selected Controller Profiles:</p>");
            sb.AppendLine("<ul>");  
            foreach (var controller in bindingList.SelectedControllers)
            {
                sb.AppendLine($"<li>{controller.DeviceName} -> {controller.ProfileName} </li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");

            //Now do main bindings table
            sb.AppendLine("<br />");
            sb.AppendLine("<table>");
            sb.Append("<tr bgcolor=\"lightgrey\">");
            sb.Append("<th rowspan=\"2\">Action</th>");
            sb.Append("<th colspan=\"3\">Binding Information</th>");
            sb.Append("</tr>");
            sb.Append("<tr bgcolor=\"lightgrey\">");
            sb.Append("<th >Controller</th>");
            sb.Append("<th >Binding</th>");
            sb.Append("<th >Priority</th>");
            sb.Append("</tr>");

            foreach (var context in bindingList)
            {
                AddSection(sb, context);
            }
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(fileName, sb.ToString());
        }

        public Color NextDefaultColor()
        {
            var value = _defaultColors[_colorIndex++];
            if (_colorIndex >= _defaultColors.Count) _colorIndex = 0;
            return value;
        }

        public void AddSection(StringBuilder sb, Context context)
        {
            var htmlColor = context.BackColor ?? NextDefaultColor();
            sb.AppendLine($"<tr bgcolor=\"{htmlColor.ToHtml()}\">");
            sb.AppendLine($"<td align=\"center\" colspan=\"4\">{TitleCase(context.ContextName)}</td>");
            sb.AppendLine("</tr>");
            var rowEven = $"<tr bgcolor=\"{htmlColor.Lighter(0.70f).ToHtml()}\">";
            var rowOdd = $"<tr bgcolor=\"{htmlColor.Lighter(0.85f).ToHtml()}\">";
            var actionNum = 0;
            foreach (var action in context.Actions)
            {
                var rowTag = actionNum++ % 2 == 0 ? rowEven : rowOdd;
                sb.AppendLine(rowTag);
                var rowSpan = action.Bindings.Count > 1 ? $" rowspan=\"{action.Bindings.Count}\"" : "";
                sb.AppendLine($"<td align=\"center\"{rowSpan}>{TitleCase(action.ActionName)}</td>");
                for (var index = 0; index < action.Bindings.Count; index++) 
                {
                    var binding = action.Bindings[index];
                    if (index > 0) sb.AppendLine(rowTag);
                    sb.AppendLine($"<td align=\"center\">{TitleCase(binding.Controller)}</td>");
                    sb.AppendLine($"<td align=\"center\">{TitleCase(binding.Key)}</td>");
                    sb.AppendLine($"<td align=\"center\">{binding.Priority}</td>");
                    if (index < action.Bindings.Count - 1) sb.AppendLine("</tr>");
                }
                sb.AppendLine("</tr>");
            }
        }

        private static string TitleCase(string? value)
        {
            return value == null ? "" : TextInfo.ToTitleCase(value.Replace("_", " ").ToLower());
        }
/*
            sb.AppendLine("<head>");
   sb.AppendLine("<style>");
   sb.AppendLine("h1 {text-align: center;}");
   sb.AppendLine("table {text-align: center; border: 1px solid;}");
   sb.AppendLine("</style>");
   sb.AppendLine("</head>");

 */
        private const string Header =
@"<head>
    <style>
        h1 {text-align: center;}
        table {text-align: center; border: 1px solid; border-collapse: collapse; width:90%; margin-left:5%; margin-right:5%}
        div {margin-left:5%; margin-right: 5%; margin-top: 10px; margin-bottom: 10px}
        th {border: 1px solid dimgrey}
        td {border: 1px solid darkgrey}
    </style>
</head>";
    }
}
