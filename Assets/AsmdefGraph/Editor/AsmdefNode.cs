using UnityEditor.Experimental.GraphView;

namespace AsmdefGraph.Editor {
    public class AsmdefNode : Node {
        public AsmdefNode() {
            title = "AsmdefNode";

            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
            inputContainer.Add(inputPort);

            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
            outputContainer.Add(outputPort);
        }
    }
}
