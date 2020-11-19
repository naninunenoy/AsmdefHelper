using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public class RandomSortStrategy : ISortStrategy {
        readonly Vector2 originPosition;
        readonly float basicDistance;
        readonly float nodeWidth;
        readonly float nodeHeight;

        public RandomSortStrategy(Vector2 originPosition, float basicDistance, float nodeWidth, float nodeHeight) {
            this.originPosition = originPosition;
            this.basicDistance = basicDistance;
            this.nodeWidth = nodeWidth;
            this.nodeHeight = nodeHeight;
        }
        public IEnumerable<SortedNode> Sort(IEnumerable<IDependencyNode> nodes) {
            var nodeArr = nodes
                .Select(x => new SortedNode { Profile = x.Profile, Position = originPosition })
                .ToArray();
            var nodeGrid = new NodeGrid(nodeWidth, nodeHeight, basicDistance, nodeArr.Length);
            var positions = nodeGrid.GridCenterPositions();
            var indexes = Enumerable.Range(0, positions.Count).ToList();

            foreach (var node in nodeArr) {
                var randomIndex = indexes[Random.Range(0, indexes.Count)];
                node.Position = positions[randomIndex] + originPosition;
                indexes.Remove(randomIndex);
            }

            return nodeArr;
        }
    }
}
