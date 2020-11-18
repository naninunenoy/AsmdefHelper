using AsmdefHelper.DependencyGraph.Editor.NodeView;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefNode : UiElementsNodeView, IAsmdefNodeView {
        public IPort LeftPort { get; }
        public IPort RightPort { get; }

        public AsmdefNode(string nodeName, VisualElement parentContentContainer) {
            Label = nodeName;

            LeftPort = new GraphViewPort(parentContentContainer, Direction.Input) { Label = "Ref By" };
            inputContainer.Add(LeftPort as Port); // as right side

            RightPort = new GraphViewPort(parentContentContainer, Direction.Output) { Label = "Ref To" };
            outputContainer.Add(RightPort as Port); // as left side
        }
    }
}
