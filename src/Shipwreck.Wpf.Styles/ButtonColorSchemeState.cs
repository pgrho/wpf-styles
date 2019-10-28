using System.ComponentModel;
using System.Windows.Media;

namespace Shipwreck.Wpf.Styles
{
    public sealed class ButtonColorSchemeState
    {
        internal const int FOREGROUND_INDEX = 0;
        internal const int BACKGROUND_INDEX = 1;
        internal const int BORDER_BRUSH_INDEX = 2;

        [DefaultValue(null)]
        public Brush? Foreground { get; set; }

        [DefaultValue(null)]
        public Brush? Background { get; set; }

        [DefaultValue(null)]
        public Brush? BorderBrush { get; set; }

        internal Brush? GetBrush(int index)
        {
            switch (index)
            {
                case FOREGROUND_INDEX:
                    return Foreground;

                case BACKGROUND_INDEX:
                    return Background;

                case BORDER_BRUSH_INDEX:
                    return BorderBrush;
            }
            return null;
        }

        internal static ButtonColorSchemeState? Create(uint? foreground = null, uint? background = null, uint? borderBrush = null)
        {
            static SolidColorBrush? getBrush(uint? color)
            {
                if (color != null)
                {
                    var b = new SolidColorBrush(Color.FromArgb((byte)(color.Value >> 24), (byte)(color.Value >> 16), (byte)(color.Value >> 8), (byte)color.Value));
                    b.Freeze();
                    return b;
                }
                return null;
            }

            if (foreground != null || background != null || borderBrush != null)
            {
                return new ButtonColorSchemeState
                {
                    Foreground = getBrush(foreground),
                    Background = getBrush(background),
                    BorderBrush = getBrush(borderBrush),
                };
            }

            return null;
        }
    }
}
