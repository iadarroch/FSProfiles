using System.Globalization;

namespace FSProfiles.Common.Classes
{
    public static class StringExtensions
    {
        private static readonly TextInfo TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

        public static string TitleCase(this string value, bool removeKey = false)
        {
            if (value == string.Empty) return "";
            if (removeKey && value.StartsWith("KEY_", true, CultureInfo.CurrentCulture))
            {
                value = value.Substring(4);
            }
            return TextInfo.ToTitleCase(value.Replace("_", " ").ToLower());
        }

    }
}
