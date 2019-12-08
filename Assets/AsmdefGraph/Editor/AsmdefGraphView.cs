using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphView : GraphView {
        public AsmdefGraphView() : base() {
            AddElement(new AsmdefNode());
            AddElement(new AsmdefNode());
            this.AddManipulator(new SelectionDragger());
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
            return ports.ToList();
        }
    }
}
