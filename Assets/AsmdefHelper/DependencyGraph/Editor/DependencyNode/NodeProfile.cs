using System;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public readonly struct NodeProfile : IEquatable<NodeProfile> {
        public readonly NodeId id;
        public readonly string name;

        public NodeProfile(NodeId id, string name) {
            this.id = id;
            this.name = name;
        }

        public bool Equals(NodeProfile other) {
            return id.Equals(other.id) && name == other.name;
        }

        public override bool Equals(object obj) {
            return obj is NodeProfile other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (id.GetHashCode() * 397) ^ (name != null ? name.GetHashCode() : 0);
            }
        }
    }
}
