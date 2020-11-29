using System.Collections;
using System.Linq;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class DomainGroupTest {

        readonly string[] inputs ={
            "unity.hoge",
            "unity.fuga",
            "unity.foo.bar",
            "UniRx",
            "UniRx.Async",
            "zenject",
            "zenject-test-framework",
            "MyAsmdef",
            "MyAsmdef2_Test",
            "MyAsmdef3.hoge",
            "MyAsmdef3-hoge",
        };

        [Test]
        public void TestDomainGroup() {
            var group = new DomainGroup();
                group.Create(inputs);
            Assert.That(group.GetTopDomains().Count(), Is.EqualTo(6));
            Assert.That(group.GetSubDomains("unity").Count(), Is.EqualTo(3));
            Assert.That(group.GetSubDomains("UniRx").Count(), Is.EqualTo(2));
            Assert.That(group.GetSubDomains("zenject").Count(), Is.EqualTo(2));
            Assert.That(group.GetSubDomains("MyAsmdef").Count(), Is.EqualTo(1));
            Assert.That(group.GetSubDomains("MyAsmdef2_Test").Count(), Is.EqualTo(1));
            Assert.That(group.GetSubDomains("MyAsmdef3").Count(), Is.EqualTo(2));

            Assert.That(group.GetTopDomainsWithSomeSubDomains().Count(), Is.EqualTo(4));
            Assert.That(group.GetSoloDomains().Count(), Is.EqualTo(2));

            Assert.That(group.GetSubDomains("unity").Any(x => x.SubDomain == "hoge"), Is.True);
            Assert.That(group.GetSubDomains("unity").Any(x => x.SubDomain == "fuga"), Is.True);
            Assert.That(group.GetSubDomains("unity").Any(x => x.SubDomain == "foo.bar"), Is.True);
            Assert.That(group.GetSubDomains("UniRx").Any(x => x.SubDomain == ""), Is.True);
            Assert.That(group.GetSubDomains("UniRx").Any(x => x.SubDomain == "Async"), Is.True);
            Assert.That(group.GetSubDomains("zenject").Any(x => x.SubDomain == ""), Is.True);
            Assert.That(group.GetSubDomains("zenject").Any(x => x.SubDomain == "test-framework"), Is.True);
            Assert.That(group.GetSubDomains("MyAsmdef").Any(x => x.SubDomain == ""), Is.True);
            Assert.That(group.GetSubDomains("MyAsmdef2_Test").Any(x => x.SubDomain == ""), Is.True);
            Assert.That(group.GetSubDomains("MyAsmdef3").Any(x => x.SubDomain == "hoge"), Is.True);
            Assert.That(group.GetSubDomains("MyAsmdef3").Count(x => x.SubDomain == "hoge"), Is.EqualTo(2));
        }
    }
}
