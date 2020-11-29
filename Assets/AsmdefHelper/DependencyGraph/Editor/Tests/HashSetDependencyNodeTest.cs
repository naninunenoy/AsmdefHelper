using System.Collections;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class HashSetDependencyNodeTest {

        [Test]
        public void TestHashSetDependencyNode() {
            var node = new HashSetDependencyNode(new NodeProfile(new NodeId(1), "node"));
            Assert.That(node, Is.InstanceOf<IDependencyNode>());
            Assert.That(node.Profile.Id.value, Is.EqualTo(1));
            Assert.That(node.Profile.Name, Is.EqualTo("node"));
            Assert.That(node.Sources, Is.Empty);
            Assert.That(node.Destinations, Is.Empty);
        }
    }
}
