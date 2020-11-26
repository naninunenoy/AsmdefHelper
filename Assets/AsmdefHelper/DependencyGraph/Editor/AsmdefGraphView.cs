using System.Collections.Generic;
using System.Linq;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using AsmdefHelper.DependencyGraph.Editor.NodeView;
using UnityEditor.Compilation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public sealed class AsmdefGraphView : GraphView {
        readonly Dictionary<string, IAsmdefNodeView> asmdefNodeDict;
        public AsmdefGraphView(IEnumerable<Assembly> assemblies) {
            var assemblyArr = assemblies.ToArray();
            // zoom可能に
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            // 背景を黒に
            Insert(0, new GridBackground());
            // ドラッグによる移動可能に
            this.AddManipulator(new SelectionDragger());
            // ドラッグで描画範囲を動かせるように
            this.AddManipulator(new ContentDragger());
            // ノードの追加
            asmdefNodeDict = new Dictionary<string, IAsmdefNodeView>();
            foreach (var asm in assemblyArr) {
                var node = new AsmdefNode(asm.name, contentContainer);
                AddElement(node);
                asmdefNodeDict.Add(node.title, node);
            }
            // 依存の整理
            var nodeProfiles = assemblyArr
                .Select((x, i) => new NodeProfile(new NodeId(i), x.name))
                .ToDictionary(x => x.Name);
            var dependencies = new List<IDependencyNode>(nodeProfiles.Count);
            // 依存先の設定
            foreach (var asm in assemblyArr) {
                if (nodeProfiles.TryGetValue(asm.name, out var profile)) {
                    var requireProfiles = asm.assemblyReferences
                        .Where(x => nodeProfiles.ContainsKey(x.name))
                        .Select(x => nodeProfiles[x.name]);
                    var dep = new HashSetDependencyNode(profile);
                    dep.SetRequireNodes(requireProfiles);
                    dependencies.Add(dep);
                }
            }
            // 依存元の設定
            NodeProcessor.SetBeRequiredNodes(dependencies);

            // 依存先にのみラインを追加
            foreach (var dep in dependencies) {
                if (!asmdefNodeDict.TryGetValue(dep.Profile.Name, out var fromNode)) {
                    continue;
                }
                foreach (var dest in dep.Destinations) {
                    if (!asmdefNodeDict.TryGetValue(dest.Name, out var toNode)) {
                        continue;
                    }
                    fromNode.RightPort.Connect(toNode.LeftPort);
                }
            }

            // Portに接続数を追記
            foreach (var dep in dependencies) {
                if (asmdefNodeDict.TryGetValue(dep.Profile.Name, out var node)) {
                    node.LeftPort.Label = $"RefBy({dep.Sources.Count})";
                    node.RightPort.Label = $"RefTo({dep.Destinations.Count})";
                }
            }

            // ノードの場所を整列
            var sortStrategy = new AlignSortStrategy(AlignParam.Default(),  Vector2.zero);
            var sortedNode = sortStrategy.Sort(dependencies);
            foreach (var node in sortedNode) {
                if (asmdefNodeDict.TryGetValue(node.Profile.Name, out var nodeView)) {
                    nodeView.SetPositionXY(node.Position);
                }
            }
        }

        public void SetNodeVisibility(string nodeName, bool visible_) {
            if (asmdefNodeDict.TryGetValue(nodeName, out var node)) {
                node.Visibility = visible_;
            }
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
            return ports.ToList();
        }
    }
}
