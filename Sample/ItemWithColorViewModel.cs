using System.Windows.Media;
using Denxorz.InputOutputSnappingCanvas;

namespace Sample
{
    public class ItemWithColorViewModel
    {
        public SolidColorBrush Color { get; } = Brushes.Aqua;
        public double Top { get; set; }
        public double Left { get; set; }

        public ColorProvider ColorProvider { get; } = new ColorProvider();

        public ItemWithColorViewModel() { }

        public ItemWithColorViewModel(double left, double top, SolidColorBrush color)
            : this()
        {
            Left = left;
            Top = top;
            Color = color;

            UpdateColor();
        }

        public override string ToString()
        {
            return $"Color {Color}";
        }

        private void UpdateColor()
        {
            ColorProvider.UpdateColor(Color);
        }
    }
}
