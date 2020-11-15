using UnityEditor.Graphs;

namespace AsmdefHelper.DependencyGraph.Editor.NodeView {
    public class UiElementsNodeView : Node, INodeView {

        string INodeView.Label {
            get => title;
            set => title = value;
        }

        float IRect.PositionX {
            get => position.x;
            set => position.x = value;
        }

        float IRect.PositionY {
            get => position.y;
            set => position.y = value;
        }

        float IRect.Height => position.height;

        float IRect.Width => position.width;
    }
}
