using System.Collections;
using System.Linq;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class RandomSortStrategyTest {
        const float d = 10.0F;
        const float w = 10.0F;
        const float h = 10.0F;
        const float e = 0.0001F;
        ISortStrategy sortStrategy;

        [SetUp]
        public void SetUpBeforeEveryTest() {
            Nodes.Init();
            sortStrategy = new RandomSortStrategy(Vector2.zero, d, w, h);
        }

        [Test]
        public void TestLinerNodeDependency() {
            var result = sortStrategy.Sort(Nodes.All).ToArray();
            foreach (var node in result) {
                var others = result.Where(x => x.Profile != node.Profile);
                foreach (var other in others) {
                    Assert.That(node.Position, Is.Not.EqualTo(other.Position).Within(e));
                }
            }
        }
    }
}
