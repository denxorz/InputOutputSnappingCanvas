using System;
using System.Linq;
using System.Windows.Media;

namespace Sample
{
    public class ColorProvider
    {
        public event EventHandler ColorUpdated;

        public SolidColorBrush Color { get; private set; }

        internal void UpdateColor(SolidColorBrush color)
        {
            Color = color;
            ColorUpdated?.Invoke(this, EventArgs.Empty);
        }

        internal bool UpdateColor(string colorName)
        {
            try
            {
                UpdateColor(new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorName)));
                return true;
            }
            catch(FormatException)
            {
                return false;
            }
        }

        internal void RemoveColor()
        {
            UpdateColor((SolidColorBrush)null);
        }

        internal static string GetColorName(SolidColorBrush brush)
        {
            if (brush == null)
            {
                return string.Empty;
            }

            var results = typeof(Colors)
                .GetProperties()
                .Where(p => (Color)p.GetValue(null, null) == brush.Color)
                .Select(p => p.Name);

            return results.Any() ? results.First() : string.Empty;
        }
    }
}
