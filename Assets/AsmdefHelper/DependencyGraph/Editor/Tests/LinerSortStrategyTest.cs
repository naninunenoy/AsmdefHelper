using System.Collections;
using System.Linq;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class LinerSortStrategyTest {
        const float d = 10.0F;
        const float w = 10.0F;
        const float h = 10.0F;
        const float e = 0.0001F;
        ISortStrategy sortStrategy;

        [SetUp]
        public void SetUpBeforeEveryTest() {
            Nodes.Init();
            sortStrategy = new LinerSortStrategy(Vector2.zero, d, w, h);
        }

        [Test]
        public void TestLinerNodeDependency() {
            var nodes = new[] { Nodes._0, Nodes._1 };
            // [0]--->[1]
            Nodes._0.SetRequireNodes(new[] { Profiles._1});
            NodeProcessor.SetBeRequiredNodes(nodes);

            var result = sortStrategy.Sort(nodes).ToArray();
            var node0 = result.FirstOrDefault(x => x.Profile == Profiles._0);
            var node1 = result.FirstOrDefault(x => x.Profile == Profiles._1);

            Assert.That(node0, Is.Not.Null);
            Assert.That(node0.Position.x, Is.Zero);
            Assert.That(node0.Position.y, Is.Zero);
            Assert.That(node1, Is.Not.Null);
            Assert.That(node1.Position.x, Is.EqualTo(node0.Position.x + d + (w / 2.0F)).Within(e));
            Assert.That(node1.Position.y, Is.EqualTo(node0.Position.y).Within(e));
        }
    }
}
