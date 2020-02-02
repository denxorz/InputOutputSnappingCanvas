using System.Windows;

namespace Denxorz
{
    public interface ISnapInput
    {
        ISnapOutput SnappedOutput { get; set; }

        object Attached { get; }

        object GetLinkedObject();
    }
}
