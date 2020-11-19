using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public interface ISortStrategy {
        IEnumerable<SortedNode> Sort(IEnumerable<IDependencyNode> nodes);
    }
}
