using System.Collections;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {
    public class DomainUnitTest {

        [Test]
        public void TestDomainUnit_HasTopDomain() {
            var unit = new DomainUnit("abc.def.ghi.jk", '.');
            Assert.That(unit.FullName, Is.EqualTo("abc.def.ghi.jk"));
            Assert.That(unit.TopDomain, Is.EqualTo("abc"));
            Assert.That(unit.SubDomain, Is.EqualTo("def.ghi.jk"));
            Assert.That(unit.HasSubDomain, Is.True);
        }

        [Test]
        public void TestDomainUnit_HasNoTopDomain() {
            var unit = new DomainUnit("abcdefghijk", '.');
            Assert.That(unit.FullName, Is.EqualTo("abcdefghijk"));
            Assert.That(unit.TopDomain, Is.EqualTo("abcdefghijk"));
            Assert.That(unit.SubDomain, Is.Empty);
            Assert.That(unit.HasSubDomain, Is.False);
        }

        [Test]
        public void TestDomainUnit_StartWithSeparator() {
            var unit = new DomainUnit(".abc", '.');
            Assert.That(unit.FullName, Is.EqualTo(".abc"));
            Assert.That(unit.TopDomain, Is.EqualTo("abc"));
            Assert.That(unit.SubDomain, Is.Empty);
            Assert.That(unit.HasSubDomain, Is.False);
        }

        [Test]
        public void TestDomainUnit_EndWithSeparator() {
            var unit = new DomainUnit("abc.", '.');
            Assert.That(unit.FullName, Is.EqualTo("abc."));
            Assert.That(unit.TopDomain, Is.EqualTo("abc"));
            Assert.That(unit.SubDomain, Is.Empty);
            Assert.That(unit.HasSubDomain, Is.False);
        }

        [Test]
        public void TestDomainUnit_StartAndEndWithSeparator() {
            var unit = new DomainUnit(".abc.def.", '.');
            Assert.That(unit.FullName, Is.EqualTo(".abc.def."));
            Assert.That(unit.TopDomain, Is.EqualTo("abc"));
            Assert.That(unit.SubDomain, Is.EqualTo("def"));
            Assert.That(unit.HasSubDomain, Is.True);
        }

        [Test]
        public void TestDomainUnit_OnlySeparator() {
            var unit = new DomainUnit(".", '.');
            Assert.That(unit.FullName, Is.EqualTo("."));
            Assert.That(unit.TopDomain, Is.Empty);
            Assert.That(unit.SubDomain, Is.Empty);
            Assert.That(unit.HasSubDomain, Is.False);
        }

        [Test]
        public void TestDomainUnit_OnlySeparators() {
            var unit = new DomainUnit("...", '.');
            Assert.That(unit.FullName, Is.EqualTo("..."));
            Assert.That(unit.TopDomain, Is.Empty);
            Assert.That(unit.SubDomain, Is.Empty);
            Assert.That(unit.HasSubDomain, Is.False);
        }
    }
}
