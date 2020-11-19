using System.Collections.Generic;
using System.Linq;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public static class NodeProcessor {

        public static void SetBeRequiredNodes(IEnumerable<IDependencyNode> nodes) {
            var array = nodes as IDependencyNode[] ?? nodes.ToArray();
            var dict = array.ToDictionary(x => x.Profile.Id);
            foreach (var n in array) {
                foreach (var d in n.Destinations) {
                    dict[d.Id].Sources.Add(n.Profile);
                }
            }
        }

        public static void SetRequireNodes(this IDependencyNode node, IEnumerable<NodeProfile> requireNodes) {
            node.Sources.Clear();
            node.Destinations.Clear();
            foreach (var d in requireNodes) {
                node.Destinations.Add(d);
            }
        }
    }
}
