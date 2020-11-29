

using System;
using System.Linq;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public class DomainUnit {
        public readonly string FullName;
        public readonly string TopDomain;
        public readonly string SubDomain;

        public DomainUnit(string fullName, char separator) {
            this.FullName = fullName;
            if (string.IsNullOrEmpty(FullName)) return;
            var split = fullName.Split(separator).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (split.Length < 2) {
                TopDomain = fullName.Replace(separator.ToString(), "");
                SubDomain = string.Empty;
            } else {
                TopDomain = split[0];
                SubDomain = split.Skip(1).Aggregate((a, b) => $"{a}{separator}{b}");
            }
        }

        public bool HasSubDomain() => !string.IsNullOrEmpty(SubDomain);
    }
}
