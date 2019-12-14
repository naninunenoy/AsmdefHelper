using UnityEditor.Experimental.GraphView;

namespace AsmdefGraph.Editor {
    public class AsmdefNode : Node {
        public readonly Port InPort;
        public readonly Port OutPort;

        public AsmdefNode(string nodeName) {
            title = nodeName;

            InPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
            InPort.portName = "In";
            inputContainer.Add(InPort);
            
            OutPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
            OutPort.portName = "Out";
            outputContainer.Add(OutPort);
        }
    }
}
