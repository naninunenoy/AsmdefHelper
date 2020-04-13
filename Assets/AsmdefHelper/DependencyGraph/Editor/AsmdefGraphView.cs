using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefGraphView : GraphView {
        public AsmdefGraphView(IEnumerable<AsmdefDependency> asmdefs) : base() {
            // zoom可能に
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            // 背景を黒に
            Insert(0, new GridBackground());
            // ドラッグによる移動可能に
            this.AddManipulator(new SelectionDragger());
            // ノードの追加
            var asmdefNodeDict = new Dictionary<string, AsmdefNode>();
            foreach (var asmdef in asmdefs) {
                var node = new AsmdefNode(asmdef.DependsFrom);
                AddElement(node);
                asmdefNodeDict.Add(node.title, node);
            }
            // 依存先にラインを追加
            var nodeApapter = new NodeAdapter();
            foreach (var asmdef in asmdefs) {
                if (!asmdefNodeDict.TryGetValue(asmdef.DependsFrom, out var fromNode)) {
                    continue;
                }
                foreach (var dependents in asmdef.DependsTo) {
                    if (!asmdefNodeDict.TryGetValue(dependents, out var toNode)) {
                        continue;
                    }
                    var edge = fromNode.RightPort.ConnectTo(toNode.LeftPort);
                    contentContainer.Add(edge);// これが無いと線が表示されない
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
            return ports.ToList();
        }
    }
}
