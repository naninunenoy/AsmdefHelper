using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public class LinerSortStrategy : ISortStrategy {
        readonly Vector2 originPosition;
        readonly float basicDistance;
        readonly float nodeWidth;
        readonly float nodeHeight;

        public LinerSortStrategy(Vector2 originPosition, float basicDistance, float nodeWidth, float nodeHeight) {
            this.originPosition = originPosition;
            this.basicDistance = basicDistance;
            this.nodeWidth = nodeWidth;
            this.nodeHeight = nodeHeight;
        }

        IEnumerable<SortedNode> ISortStrategy.Sort(IEnumerable<IDependencyNode> nodes) {
            var arr = nodes.ToArray();
            var posDict = arr
                .ToDictionary(x => x.Profile, _ => originPosition);
            // 参照元がないNodeを原点に
            var left = arr.FirstOrDefault(x => x.IsSourceEmpty());
            posDict[left.Profile] = originPosition;
            var right = arr.FirstOrDefault(x => x.Profile != left.Profile);
            posDict[right.Profile] = new Vector2(nodeWidth / 2.0F + basicDistance, 0.0F) + originPosition;
            return posDict.Select(x => new SortedNode { Profile = x.Key, Position = x.Value });
        }
    }
}
