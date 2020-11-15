using System;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public readonly struct NodeId : IEquatable<NodeId> {
        public readonly int value;

        public NodeId(int value) => this.value = value;

        public bool Equals(NodeId other) {
            return value == other.value;
        }

        public override bool Equals(object obj) {
            return obj is NodeId other && Equals(other);
        }

        public override int GetHashCode() {
            return value;
        }

        public override string ToString() {
            return value.ToString();
        }

        public static bool operator ==(NodeId x, NodeId y) {
            return x.Equals(y);
        }

        public static bool operator !=(NodeId x, NodeId y) {
            return !(x == y);
        }
    }
}
