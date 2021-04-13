using System.Collections.Generic;

namespace Sample
{
    public record TreeViewItem(string Name, IReadOnlyCollection<TreeViewItem> Members);
}
