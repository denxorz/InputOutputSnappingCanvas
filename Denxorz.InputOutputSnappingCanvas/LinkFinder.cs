using System.Collections.Generic;
using System.Linq;

namespace Denxorz.InputOutputSnappingCanvas
{
    internal class LinkFinder
    {
        private readonly IReadOnlyCollection<ISnapHost> all;
        private readonly List<ISnapHost> linkedItems = new();

        private LinkFinder(IReadOnlyCollection<ISnapHost> allHosts)
        {
            all = allHosts;
        }    

        public static IReadOnlyCollection<ISnapHost> Find(IReadOnlyCollection<ISnapHost> allHosts, ISnapHost startItem)
        {
            var finder = new LinkFinder(allHosts);
            finder.Find(startItem);
            return finder.linkedItems;
        }

        private void Find(ISnapHost startItem)
        {
            linkedItems.Add(startItem);

            foreach (var output in startItem.Outputs.Where(o => o.ConnectedInput != null))
            {
                var item = all.First(s => s.Inputs.Contains(output.ConnectedInput));
                if (!linkedItems.Contains(item))
                {
                    Find(item);
                }
            }

            foreach (var input in startItem.Inputs.Where(o => o.ConnectedOutput != null))
            {
                var item = all.First(s => s.Outputs.Contains(input.ConnectedOutput));
                if (!linkedItems.Contains(item))
                {
                    Find(item);
                }
            }
        }
    }
}
