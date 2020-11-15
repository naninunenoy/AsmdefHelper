using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public interface IDependencyNode {
        NodeProfile Profile { get; }
        ICollection<NodeProfile> Sources  { get; }
        ICollection<NodeProfile> Destinations { get; }
    }
}
