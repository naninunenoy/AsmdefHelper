using System.Collections;
using System.Collections.Generic;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class NodeIdTest {
        [Test]
        public void TestNodeIdValue() {
            var nodeId = new NodeId(123);
            Assert.That(nodeId.value, Is.EqualTo(123));
        }

        [Test]
        public void TestNodeIdEquals() {
            var id1 = new NodeId(111);
            var id2 = new NodeId(222);
            var id3 = new NodeId(111);
            Assert.That(id1, Is.EqualTo(id1));
            Assert.That(id1, Is.Not.EqualTo(id2));
            Assert.That(id1, Is.EqualTo(id3));
        }

        [Test]
        public void TestNodeIdToString() {
            var nodeId = new NodeId(999);
            Assert.That(nodeId.ToString(), Is.EqualTo("999"));
        }
    }
}
