namespace AsmdefHelper.DependencyGraph.Editor.NodeView {
    public interface INodeView : IRect, IVisible {
        string Label { set; get; }
    }
}
