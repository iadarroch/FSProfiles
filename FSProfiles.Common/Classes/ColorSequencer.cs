using System.Drawing;

namespace FSProfiles.Common.Classes
{
    public class ColorSequencer
    {
        private readonly List<Color> _defaultColors =
        [Color.Aquamarine, Color.LightCoral, Color.LightSkyBlue, Color.Wheat,
            Color.Plum,Color.LemonChiffon, Color.SandyBrown, Color.LightPink,
            Color.LightGreen, Color.Bisque, Color.Thistle, Color.PowderBlue,
            Color.LightGoldenrodYellow, Color.Lavender, Color.NavajoWhite,
            Color.RosyBrown, Color.LightSalmon, Color.GreenYellow, Color.PaleTurquoise,
            Color.Tan];
        private int _colorIndex;

        public void ResetDefaultColor()
        {
            _colorIndex = 0;
        }

        public Color NextDefaultColor()
        {
            var value = _defaultColors[_colorIndex++];
            if (_colorIndex >= _defaultColors.Count) _colorIndex = 0;
            return value;
        }
    }
}
