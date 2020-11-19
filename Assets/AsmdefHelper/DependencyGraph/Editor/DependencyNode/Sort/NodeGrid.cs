using System.Collections.Generic;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public class NodeGrid {
        public readonly float GridWidth;
        public readonly float GridHeight;
        public readonly float Distance;
        readonly int gridSideCount;
        public readonly int GridCount;

        public NodeGrid(float nodeWidth, float nodeHeight, float nodeDistance, int nodeCount) {
            GridWidth = nodeWidth;
            GridHeight = nodeHeight;
            Distance = nodeDistance;
            gridSideCount = SquareValueProvider.ProvideNearestSquareValue(nodeCount);
            GridCount = gridSideCount * gridSideCount;
        }

        public IReadOnlyList<Vector2> GridCenterPositions() {
            var grids = new Vector2[GridCount];
            var index = 0;
            var y = 0.0F;
            for (var i = 0; i < gridSideCount; i++) {
                var x = 0.0F;
                for (var j = 0; j < gridSideCount; j++) {
                    grids[index] = new Vector2(x, y);
                    x += (GridWidth/2.0F) + Distance;
                    index++;
                }
                y += (GridHeight/2.0F) + Distance;;
            }
            return grids;
        }
    }
}
