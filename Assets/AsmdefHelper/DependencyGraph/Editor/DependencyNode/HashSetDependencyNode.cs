using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public class HashSetDependencyNode : IDependencyNode {
        public NodeProfile Profile { get; }
        public ICollection<NodeProfile> Sources { get; }
        public ICollection<NodeProfile> Destinations { get; }

        public HashSetDependencyNode(NodeProfile profile) {
            Profile = profile;
            Sources = new HashSet<NodeProfile>();
            Destinations = new HashSet<NodeProfile>();
        }
    }
}
