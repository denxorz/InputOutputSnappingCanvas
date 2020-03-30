using System.Collections.Generic;

namespace Denxorz.InputOutputSnappingCanvas
{
    public interface ISnapHost
    {
        IReadOnlyCollection<IConnectionOutput> Outputs { get; }
        IReadOnlyCollection<IConnectionInput> Inputs { get; }
        object DataContext { get; }
    }
}
