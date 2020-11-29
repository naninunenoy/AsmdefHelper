using System.Collections;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class DependencyNodeExtensionsTest {

        [SetUp]
        public void SetUpBeforeEveryTest() {
            Nodes.Init();
            // [0]--->[1]--->[2]<---[3]    [4]
            Nodes._0.SetRequireNodes(new[] { Profiles._1 });
            Nodes._1.SetRequireNodes(new[] { Profiles._2 });
            Nodes._3.SetRequireNodes(new[] { Profiles._2 });
            NodeProcessor.SetBeRequiredNodes(new[] { Nodes._0, Nodes._1, Nodes._2, Nodes._3, Nodes._4 });
        }

        [Test]
        public void TestIsSourceEmpty() {
            Assert.That(Nodes._0.IsSourceEmpty(), Is.True);
            Assert.That(Nodes._1.IsSourceEmpty(), Is.False);
            Assert.That(Nodes._2.IsSourceEmpty(), Is.False);
            Assert.That(Nodes._3.IsSourceEmpty(), Is.True);
            Assert.That(Nodes._4.IsSourceEmpty(), Is.True);
        }

        [Test]
        public void TestIsDestinationEmpty() {
            Assert.That(Nodes._0.IsDestinationEmpty(), Is.False);
            Assert.That(Nodes._1.IsDestinationEmpty(), Is.False);
            Assert.That(Nodes._2.IsDestinationEmpty(), Is.True);
            Assert.That(Nodes._3.IsDestinationEmpty(), Is.False);
            Assert.That(Nodes._4.IsDestinationEmpty(), Is.True);
        }

        [Test]
        public void TestIsEmptyDependency() {
            Assert.That(Nodes._0.IsEmptyDependency(), Is.False);
            Assert.That(Nodes._1.IsEmptyDependency(), Is.False);
            Assert.That(Nodes._2.IsEmptyDependency(), Is.False);
            Assert.That(Nodes._3.IsEmptyDependency(), Is.False);
            Assert.That(Nodes._4.IsEmptyDependency(), Is.True);
        }

        [Test]
        public void TestCountDependencies() {
            Assert.That(Nodes._0.CountDependencies(), Is.EqualTo(1));
            Assert.That(Nodes._1.CountDependencies(), Is.EqualTo(2));
            Assert.That(Nodes._2.CountDependencies(), Is.EqualTo(2));
            Assert.That(Nodes._3.CountDependencies(), Is.EqualTo(1));
            Assert.That(Nodes._4.CountDependencies(), Is.EqualTo(0));
        }
    }
}
