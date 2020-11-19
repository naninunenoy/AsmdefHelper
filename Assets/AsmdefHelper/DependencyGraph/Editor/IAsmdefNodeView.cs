using AsmdefHelper.DependencyGraph.Editor.NodeView;

namespace AsmdefHelper.DependencyGraph.Editor {
    public interface IAsmdefNodeView : INodeView {
        IPort LeftPort { get; }
        IPort RightPort { get; }
    }
}
