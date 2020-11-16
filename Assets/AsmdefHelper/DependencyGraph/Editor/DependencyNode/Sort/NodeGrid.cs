using System.Collections.Generic;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public class NodeGrid {
        public readonly float GridSize;
        readonly int gridSideCount;
        public readonly int GridCount;

        public NodeGrid(float nodeWidth, float nodeDistance, int nodeCount) {
            GridSize = nodeDistance + nodeWidth / 2.0F;
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
                    x += GridSize;
                    index++;
                }
                y += GridSize;
            }
            return grids;
        }
    }
}
