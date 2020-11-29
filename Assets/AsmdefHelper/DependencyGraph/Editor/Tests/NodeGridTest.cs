using System.Collections;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class NodeGridTest {

        [Test]
        public void TestNodeGrid() {
            const float e = 0.000001F;
            var nodeGrid = new NodeGrid(10.0F, 10.0F, 10.0F, 4);
            Assert.That(nodeGrid.GridCount, Is.EqualTo(4));
            var grids = nodeGrid.GridCenterPositions();
            Assert.That(grids[0].x, Is.EqualTo(0.0F).Within(e));
            Assert.That(grids[0].y, Is.EqualTo(0.0F).Within(e));
            Assert.That(grids[1].x, Is.EqualTo(15.0F).Within(e));
            Assert.That(grids[1].y, Is.EqualTo(0.0F).Within(e));
            Assert.That(grids[2].x, Is.EqualTo(0.0F).Within(e));
            Assert.That(grids[2].y, Is.EqualTo(15.0F).Within(e));
            Assert.That(grids[3].x, Is.EqualTo(15.0F).Within(e));
            Assert.That(grids[3].y, Is.EqualTo(15.0F).Within(e));
        }
    }
}
