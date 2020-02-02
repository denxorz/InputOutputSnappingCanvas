using System.Collections.Generic;

namespace Denxorz
{
    public interface ISnapHost
    {
        IReadOnlyCollection<ISnapOutput> Outputs { get; }
        IReadOnlyCollection<ISnapInput> Inputs { get; }
    }
}
