using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Tests {
    public class NodeProfileTest {

        [Test]
        public void TestNodeProfile() {
            var nodeProfile = new NodeProfile(new NodeId(123), "testNode");
            Assert.That(nodeProfile.id.value, Is.EqualTo(123));
            Assert.That(nodeProfile.name, Is.EqualTo("testNode"));
        }

        [Test]
        public void TestNodeProfileEquals() {
            var profile1 = new NodeProfile(new NodeId(111), "testNode1");
            var profile2 = new NodeProfile(new NodeId(222), "testNode1");
            var profile3 = new NodeProfile(new NodeId(111), "testNode2");
            var profile4 = new NodeProfile(new NodeId(111), "testNode1");
            Assert.That(profile1, Is.EqualTo(profile1));
            Assert.That(profile1, Is.Not.EqualTo(profile2));
            Assert.That(profile1, Is.Not.EqualTo(profile3));
            Assert.That(profile1, Is.EqualTo(profile4));
        }
    }
}
