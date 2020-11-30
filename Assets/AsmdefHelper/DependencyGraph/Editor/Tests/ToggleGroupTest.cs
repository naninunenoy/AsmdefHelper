using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class ToggleGroupTest {
        [Test]
        public void TestToggleGroup() {
            var toggle0 = new ToggleMock();
            var toggle1 = new ToggleMock();
            var toggle2 = new ToggleMock();
            var toggleGroup = new ToggleGroup(toggle0, new[] { toggle1, toggle2 });

            toggleGroup.IsOn = true;
            toggle0.IsOn = true;
            Assert.That(toggle1.IsOn, Is.True);
            Assert.That(toggle2.IsOn, Is.True);
        }
    }
    public class ToggleMock : IToggle {
        public bool IsOn { set; get; }
        public string Name { get; set; }
    }
}
