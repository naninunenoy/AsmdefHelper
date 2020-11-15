using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public class HashSetDependencyNode : IDependencyNode {
        public NodeProfile Profile { get; }
        public ICollection<NodeProfile> Sources => sources;
        public ICollection<NodeProfile> Destinations => destinations;

        readonly HashSet<NodeProfile> sources;
        readonly HashSet<NodeProfile> destinations;

        public HashSetDependencyNode(NodeProfile profile) {
            Profile = profile;
            sources = new HashSet<NodeProfile>();
            destinations = new HashSet<NodeProfile>();
        }
    }
}
