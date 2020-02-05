using System.Windows.Media;

namespace Sample
{
    public interface IColorProvider
    {
        SolidColorBrush Color { get; }
    }
}
