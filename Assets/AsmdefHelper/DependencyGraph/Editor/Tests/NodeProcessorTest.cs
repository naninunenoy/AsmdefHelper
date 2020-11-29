using System.Collections;
using System.Linq;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class NodeProcessorTest {

        [SetUp]
        public void SetUpBeforeEveryTest() {
            Nodes.Init();
        }

        [Test]
        public void TestSetRequireNodes() {
            var node = new HashSetDependencyNode(new NodeProfile(new NodeId(0), "node0"));
            var p1 = new NodeProfile(new NodeId(1), "node1");
            var p2 = new NodeProfile(new NodeId(2), "node2");
            var p3 = new NodeProfile(new NodeId(3), "node3");
            node.Sources.Add(p3);
            node.SetRequireNodes(new[] { p1, p2 });
            Assert.That(node.Destinations.Count, Is.EqualTo(2));
            Assert.That(node.Destinations.Any(x => x.Equals(p1)));
            Assert.That(node.Destinations.Any(x => x.Equals(p2)));
            Assert.That(node.Sources.Count, Is.Zero);
        }

        [Test]
        public void TestLinerNodeDependency() {
            // [0]--->[1]
            Nodes._0.SetRequireNodes(new[] { Profiles._1});
            NodeProcessor.SetBeRequiredNodes(new[] { Nodes._0, Nodes._1 });
            Assert.That(Nodes._1.Sources.Any(x => x.Equals(Profiles._0)));
        }

        [Test]
        public void TestSomeNodeDependency() {
            Nodes.SetSomeNodeDependency();
            NodeProcessor.SetBeRequiredNodes(Nodes.All);
            // 0
            Assert.That(Nodes._0.Sources, Is.Empty);
            Assert.That(Nodes._0.Destinations.Count, Is.EqualTo(2));
            Assert.That(Nodes._0.Destinations.Any(x => x == Profiles._1));
            Assert.That(Nodes._0.Destinations.Any(x => x == Profiles._2));
            // 1
            Assert.That(Nodes._1.Destinations.Count, Is.EqualTo(1));
            Assert.That(Nodes._1.Sources.Any(x => x == Profiles._0));
            Assert.That(Nodes._1.Destinations.Count, Is.EqualTo(1));
            Assert.That(Nodes._1.Destinations.Any(x => x == Profiles._4));
            // 2
            Assert.That(Nodes._2.Sources.Count, Is.EqualTo(1));
            Assert.That(Nodes._2.Sources.Any(x => x == Profiles._0));
            Assert.That(Nodes._2.Destinations.Count, Is.EqualTo(2));
            Assert.That(Nodes._2.Destinations.Any(x => x == Profiles._3));
            Assert.That(Nodes._2.Destinations.Any(x => x == Profiles._4));
            // 3
            Assert.That(Nodes._3.Sources.Count, Is.EqualTo(2));
            Assert.That(Nodes._3.Sources.Any(x => x == Profiles._2));
            Assert.That(Nodes._3.Sources.Any(x => x == Profiles._5));
            Assert.That(Nodes._3.Destinations, Is.Empty);
            // 4
            Assert.That(Nodes._4.Sources.Count, Is.EqualTo(2));
            Assert.That(Nodes._4.Sources.Any(x => x == Profiles._1));
            Assert.That(Nodes._4.Sources.Any(x => x == Profiles._2));
            Assert.That(Nodes._4.Destinations.Count, Is.EqualTo(1));
            Assert.That(Nodes._4.Destinations.Any(x => x == Profiles._5));
            // 5
            Assert.That(Nodes._5.Sources.Count, Is.EqualTo(1));
            Assert.That(Nodes._5.Sources.Any(x => x == Profiles._4));
            Assert.That(Nodes._5.Destinations.Count, Is.EqualTo(2));
            Assert.That(Nodes._5.Destinations.Any(x => x == Profiles._3));
            Assert.That(Nodes._5.Destinations.Any(x => x == Profiles._6));
            // 6
            Assert.That(Nodes._6.Sources.Count, Is.EqualTo(1));
            Assert.That(Nodes._6.Sources.Any(x => x == Profiles._5));
            Assert.That(Nodes._6.Destinations, Is.Empty);
            // 7
            Assert.That(Nodes._7.Sources, Is.Empty);
            Assert.That(Nodes._7.Destinations.Count, Is.EqualTo(1));
            Assert.That(Nodes._7.Destinations.Any(x => x == Profiles._8));
            // 8
            Assert.That(Nodes._8.Sources.Count, Is.EqualTo(1));
            Assert.That(Nodes._8.Sources.Any(x => x == Profiles._7));
            Assert.That(Nodes._8.Destinations, Is.Empty);
            // 9
            Assert.That(Nodes._9.Sources, Is.Empty);
            Assert.That(Nodes._9.Destinations, Is.Empty);
        }
    }
}
