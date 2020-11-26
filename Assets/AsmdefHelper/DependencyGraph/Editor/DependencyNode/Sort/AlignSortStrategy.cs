using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//https://github.com/TORISOUP/AnimatorControllerLayouter/blob/master/Assets/TORISOUP/AnimatorControllerLayouter/Editor/LayoutHelper.cs
namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public class AlignSortStrategy : ISortStrategy {
        readonly AlignParam alignParam;
        readonly Vector2 originPosition;

        public AlignSortStrategy(AlignParam alignParam, Vector2 originPosition) {
            this.alignParam = alignParam;
            this.originPosition = originPosition;
        }

        public IEnumerable<SortedNode> Sort(IEnumerable<IDependencyNode> nodes) {
            var nodeArr = nodes.ToArray();
            var posDict = nodeArr.ToDictionary(x => x.Profile, _ => originPosition);

            // まず順番に整列させる
            nodeArr = nodeArr.OrderBy(x => x.Profile.Name).ToArray();
            var nodeGrid = new NodeGrid(alignParam.nodeWidth, alignParam.nodeHeight, alignParam.basicDistance, nodeArr.Length);
            for (var i = 0; i < nodeArr.Length; i++) {
                posDict[nodeArr[i].Profile] += nodeGrid.GridCenterPositions()[i];
            }

            // ばねによる整列
            var tryCount = alignParam.tryCount;
            while (tryCount-- > 0) {
                foreach (var node in nodeArr) {
                    var target = posDict[node.Profile];
                    var force = Vector2.zero;
                    foreach (var innerNode in nodeArr) {
                        var other = posDict[innerNode.Profile];
                        // ばねの計算
                        if (node.IsConnectedTo(innerNode.Profile)) {
                            force += alignParam.接続したノード同士はばねによって引き合う(target, other);
                        }

                        force += alignParam.全ノードは互いに斥力が発生する(target, other);
                    }

                    posDict[node.Profile] = target + force * 1.0f;
                }
            }

            // 接続数に応じて左右に移動させる
            // ref to が多いものが左に、ref by が多いものが右に
            const float factor = 60.0F;
            foreach (var dep in nodeArr) {
                var score = (dep.Sources.Count - dep.Destinations.Count) * factor;
                var p = posDict[dep.Profile];
                posDict[dep.Profile] = new Vector2(p.x + score, p.y);
            }

            return posDict.Select(x => new SortedNode { Profile = x.Key, Position = x.Value });
        }
    }
}
