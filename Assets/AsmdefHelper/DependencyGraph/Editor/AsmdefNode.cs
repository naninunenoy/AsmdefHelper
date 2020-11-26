using AsmdefHelper.DependencyGraph.Editor.NodeView;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefNode : UiElementsNodeView, IAsmdefNodeView {
        readonly GraphViewPort leftPort;
        readonly GraphViewPort rightPort;
        public IPort LeftPort => leftPort;
        public IPort RightPort => rightPort;

        public AsmdefNode(string nodeName, VisualElement parentContentContainer) {
            Label = nodeName;

            leftPort = new GraphViewPort(parentContentContainer, Direction.Input) { Label = "Ref By" };
            inputContainer.Add(LeftPort as Port); // as right side

            rightPort = new GraphViewPort(parentContentContainer, Direction.Output) { Label = "Ref To" };
            outputContainer.Add(RightPort as Port); // as left side
        }

        public override bool Visibility {
            get => base.Visibility;
            set {
                base.Visibility = value;
                // right(output)
                foreach (var edge in rightPort.connections) {
                    edge.visible = edge.input.node.visible & visible;
                }
                // left(input)
                foreach (var edge in leftPort.connections) {
                    edge.visible = edge.output.node.visible & visible;
                }
            }
        }
    }
}
