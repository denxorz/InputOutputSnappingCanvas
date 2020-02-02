using System.Windows;

namespace Denxorz
{
    public interface ISnapOutput
    {
        ISnapInput SnappedInput { get; set; }

        object Attached { get; }

        object GetLinkedObject();
    }
}
