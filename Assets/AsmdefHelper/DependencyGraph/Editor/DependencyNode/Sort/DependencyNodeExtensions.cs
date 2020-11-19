using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public static class DependencyNodeExtensions {
        public static bool IsSourceEmpty(this IDependencyNode node) {
            return !node.Sources.Any();
        }
        public static bool IsDestinationEmpty(this IDependencyNode node) {
            return !node.Destinations.Any();
        }
        public static bool IsEmptyDependency(this IDependencyNode node) {
            return node.IsSourceEmpty() && node.IsDestinationEmpty();
        }
        public static int CountDependencies(this IDependencyNode node) {
            return node.Sources.Count + node.Destinations.Count;
        }
        public static bool ContainsSelfReference(this IDependencyNode node) {
            return node.Sources.Any(x => x == node.Profile) ||
                   node.Destinations.Any(x => x == node.Profile);
        }
        public static IEnumerable<NodeProfile> ConnectedNodes(this IDependencyNode node) {
            return node.Sources.Concat(node.Destinations);
        }
        public static bool IsConnectedTo(this IDependencyNode node, NodeProfile nodeProfile) {
            return node.Sources.Contains(nodeProfile) || node.Destinations.Contains(nodeProfile);
        }
    }
}
