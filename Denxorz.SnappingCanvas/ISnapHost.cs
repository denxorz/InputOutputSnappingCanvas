using System.Collections.Generic;

namespace Denxorz.SnappingCanvas
{
    public interface ISnapHost
    {
        IReadOnlyCollection<IConnectionOutput> Outputs { get; }
        IReadOnlyCollection<IConnectionInput> Inputs { get; }
    }
}
