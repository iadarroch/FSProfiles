using System.Drawing;
using System.Globalization;
using System.Text;
using FSProfiles.Common.Models;

namespace FSProfiles.Common.Classes
{
    public interface IOutputFormatter
    {
        void ConvertToHtml(BindingList bindingList, string fileName);

    }

    public class HtmlFormatter : IOutputFormatter
    {
        private static readonly TextInfo TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
        private static List<Color> _defaultColors = 
        [Color.Aquamarine, Color.LightCoral, Color.LightSkyBlue, Color.Wheat, 
            Color.Plum,Color.LemonChiffon, Color.SandyBrown, Color.LightPink,
            Color.LightGreen, Color.Bisque, Color.Thistle, Color.PowderBlue,
            Color.LightGoldenrodYellow, Color.Lavender, Color.NavajoWhite, 
            Color.RosyBrown, Color.LightSalmon, Color.GreenYellow, Color.PaleTurquoise,
            Color.Tan];
        private int _colorIndex;

        public HtmlFormatter()
        {
        }

        public void ConvertToHtml(BindingList bindingList, string fileName)
        {
            _colorIndex = 0;
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine(Header);
            sb.AppendLine("<body>");

            //Add document headers
            sb.AppendLine("<h1>Microsoft Flight Simulator 2020 Bindings Report</h1>");
            AddControllers(sb, bindingList);

            //Now do main bindings table
            AddBindingsTable(sb, bindingList);

            sb.AppendLine("<div>");
            sb.AppendLine("<p>Copyright 2024, Ian Darroch<br/>");
            sb.AppendLine("Source repository: <a href=\"https://github.com/iadarroch/msfs2020-bindings-report\" target=\"_blank\">https://github.com/iadarroch/msfs2020-bindings-report</a>");
            sb.AppendLine("</p>");
            sb.AppendLine("</div>");
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

        public void AddControllers(StringBuilder sb, BindingList bindingList)
        {
            sb.AppendLine("<table>");
            sb.AppendLine("<tr bgcolor=\"antiquewhite\">");
            sb.AppendLine("<th class=\"tableTitle\" colspan=\"3\">Selected Controller Profiles</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr bgcolor=\"antiquewhite\">");
            sb.AppendLine("<th>Controller</th>");
            sb.AppendLine("<th>Profile</th>");
            sb.AppendLine("<th>Folder</th>");
            sb.AppendLine("</tr>");

            foreach (var controller in bindingList.SelectedControllers)
            {
                sb.AppendLine("<tr bgcolor=\"floralwhite\">");
                sb.AppendLine($"<td>{controller.DeviceName}</td>");
                sb.AppendLine($"<td>{controller.ProfileName}</td>");
                sb.AppendLine($"<td>{controller.ProfilePath}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
        }

        public void AddBindingsTable(StringBuilder sb, BindingList bindingList)
        {
            sb.AppendLine("<br />");
            sb.AppendLine("<table>");
            sb.Append("<tr bgcolor=\"lightgrey\">");
            sb.Append("<th class=\"tableTitle\" colspan=\"4\">Controller Bindings</th>");
            sb.Append("</tr>");
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
        }

        public void AddSection(StringBuilder sb, FSContext context)
        {
            var htmlColor = context.BackColor ?? NextDefaultColor();
            sb.AppendLine($"<tr bgcolor=\"{htmlColor.ToHtml()}\">");
            sb.AppendLine($"<td class=\"section\" align=\"center\" colspan=\"4\">{TitleCase(context.ContextName)}</td>");
            sb.AppendLine("</tr>");
            var rowEven = $"<tr bgcolor=\"{htmlColor.Lighter(0.70f).ToHtml()}\">";
            var rowOdd = $"<tr bgcolor=\"{htmlColor.Lighter(0.85f).ToHtml()}\">";
            var actionNum = 0;
            foreach (var action in context.Actions)
            {
                var rowTag = actionNum++ % 2 == 0 ? rowEven : rowOdd;
                sb.AppendLine(rowTag);
                var rowSpan = action.Bindings.Count > 1 ? $" rowspan=\"{action.Bindings.Count}\"" : "";
                sb.AppendLine($"<td align=\"center\"{rowSpan}>{TitleCase(action.ActionName, true)}</td>");
                for (var index = 0; index < action.Bindings.Count; index++) 
                {
                    var binding = action.Bindings[index];
                    if (index > 0) sb.AppendLine(rowTag);
                    sb.AppendLine($"<td align=\"center\">{TitleCase(binding.Controller)}</td>");
                    sb.AppendLine($"<td align=\"center\">{TitleCase(binding.KeyCombo)}</td>");
                    sb.AppendLine($"<td align=\"center\">{binding.Priority}</td>");
                    if (index < action.Bindings.Count - 1) sb.AppendLine("</tr>");
                }
                sb.AppendLine("</tr>");
            }
        }

        private static string TitleCase(string? value, bool removeKey = false)
        {
            if (value == null) return "";
            if (removeKey && value.StartsWith("KEY_", true, CultureInfo.CurrentCulture))
            {
                value = value.Substring(4);
            }
            return TextInfo.ToTitleCase(value.Replace("_", " ").ToLower());
        }

        private const string Header =
            @"<head>
    <style>
        h1 {text-align: center;}
        p {text-align: right;}
        table {text-align: center; border: 1px solid; border-collapse: collapse; width:90%; margin-left:5%; margin-right:5%; margin-bottom: 25px}
        div {margin-left:5%; margin-right: 5%; margin-top: 10px; margin-bottom: 0px}
        th {border: 1px solid dimgrey}
        th.tableTitle {font-size: 18pt}
        td {border: 1px solid darkgrey}
        td.section {font-size: 15pt}
    </style>
</head>";
    }
}