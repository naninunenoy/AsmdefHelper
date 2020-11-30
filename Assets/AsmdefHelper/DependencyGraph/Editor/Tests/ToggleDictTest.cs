using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class ToggleDictTest {

        [Test]
        public void TestScanUpdate() {
            var dict = new ToggleDict();
            var toggle0 = new ToggleMock { IsOn = false };
            var toggle1 = new ToggleMock { IsOn = false };
            dict.Add("key0", toggle0);
            dict.Add("key1", toggle1);
            toggle0.IsOn = true;

            // toggleの更新が取得できるか
            var updated = dict.ScanUpdate().ToArray();
            Assert.That(updated.Length, Is.EqualTo(1));
            var (key0, val0) = updated[0];
            Assert.That(key0, Is.EqualTo("key0"));
            Assert.That(val0, Is.True);
        }

        [Test]
        public void TestTryGetToggle() {
            var dict = new ToggleDict();
            var toggle0 = new ToggleMock { IsOn = false };
            var toggle1 = new ToggleMock { IsOn = false };
            dict.Add("key0", toggle0);
            dict.Add("key1", toggle1);

            var ret = dict.TryGetToggle("key0", out var getToggle);
            Assert.That(ret, Is.True);
            Assert.That(getToggle, Is.EqualTo(toggle0));
            ret = dict.TryGetToggle("key1", out getToggle);
            Assert.That(ret, Is.True);
            Assert.That(getToggle, Is.EqualTo(toggle1));

            dict.Clear();
            ret = dict.TryGetToggle("key0", out getToggle);
            Assert.That(ret, Is.False);
            Assert.That(getToggle, Is.Null);
        }
    }
}
