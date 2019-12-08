using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphView : GraphView {
        public AsmdefGraphView(IEnumerable<string> asmdefs) : base() {
            // zoom可能に
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            // 背景を黒に
            Insert(0, new GridBackground());
            // ドラッグによる移動可能に
            this.AddManipulator(new SelectionDragger());
            foreach (var asmdef in asmdefs) {
                AddElement(new AsmdefNode(asmdef));
            }
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
            return ports.ToList();
        }
    }
}
