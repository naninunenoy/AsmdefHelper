using UnityEditor.Experimental.GraphView;

namespace AsmdefGraph.Editor {
    public class AsmdefNode : Node {
        public AsmdefNode(string nodeName) {
            title = nodeName;

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
            inputPort.portName = "In";
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            outputPort.portName = "Out";
            outputContainer.Add(outputPort);
        }
    }
}
