using UnityEditor.Experimental.GraphView;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefNode : Node {
        public readonly Port LeftPort;
        public readonly Port RightPort;

        public AsmdefNode(string nodeName) {
            title = nodeName;

            LeftPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            LeftPort.portName = "Ref By";
            outputContainer.Add(LeftPort); // as right side

            RightPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
            RightPort.portName = "Ref To";
            inputContainer.Add(RightPort); // as left side
        }
    }
}
