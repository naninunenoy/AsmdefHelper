using System;
using System.Collections;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class SquareValueProviderTest {

        [Test]
        public void TestProvideNearestSquareValue() {
            var ans = SquareValueProvider.ProvideNearestSquareValue(0);
            Assert.That(ans, Is.EqualTo(0));
            ans = SquareValueProvider.ProvideNearestSquareValue(1);
            Assert.That(ans, Is.EqualTo(1));
            ans = SquareValueProvider.ProvideNearestSquareValue(4);
            Assert.That(ans, Is.EqualTo(2));
            ans = SquareValueProvider.ProvideNearestSquareValue(8);
            Assert.That(ans, Is.EqualTo(3));
            ans = SquareValueProvider.ProvideNearestSquareValue(9);
            Assert.That(ans, Is.EqualTo(3));
            ans = SquareValueProvider.ProvideNearestSquareValue(10);
            Assert.That(ans, Is.EqualTo(4));
            ans = SquareValueProvider.ProvideNearestSquareValue(10001);
            Assert.That(ans, Is.EqualTo(100));
            ans = SquareValueProvider.ProvideNearestSquareValue(-1);
            Assert.That(ans, Is.EqualTo(0));
        }
    }
}
