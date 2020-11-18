using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.NodeView {
    public interface IPort {
        string Label { set; get; }
        Vector2 Position { get; }
        void Connect(IPort port);
    }
}
