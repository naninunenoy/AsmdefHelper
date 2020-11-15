using System;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public class NodeProfile : IEquatable<NodeProfile> {
        public NodeId Id { get; }
        public string Name { set; get; }

        public NodeProfile(NodeId id, string name) {
            Id = id;
            Name = name;
        }

        public bool Equals(NodeProfile other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NodeProfile) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(NodeProfile x, NodeProfile y) {
            return x.Equals(y);
        }

        public static bool operator !=(NodeProfile x, NodeProfile y) {
            return !(x == y);
        }
    }
}
