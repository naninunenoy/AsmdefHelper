using System.Collections.Generic;
using System.Linq;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode {
    public class DomainGroup {
        readonly Dictionary<string, List<DomainUnit>> dict;

        public DomainGroup() {
            dict = new Dictionary<string, List<DomainUnit>>();
        }

        public void Create(IEnumerable<string> all) {
            dict.Clear();
            foreach (var str in all) {
                var unit = new DomainUnit(str, '.');
                if (!unit.HasSubDomain()) {
                    unit = new DomainUnit(str, '-');
                }
                if (!unit.HasSubDomain()) {
                    unit = new DomainUnit(str, '_');
                }

                if (!unit.HasSubDomain()) {
                    dict.Add(unit.FullName, new List<DomainUnit> { unit });
                } else {
                    if (dict.TryGetValue(unit.TopDomain, out var list)) {
                        list.Add(unit);
                    } else {
                        dict.Add(unit.TopDomain, new List<DomainUnit> { unit });
                    }
                }
            }
            // 1つしかなかったものを単独とする
            var soloKeys = GetSoloDomains().ToArray();
            foreach (var key in soloKeys) {
                var unit = dict[key].FirstOrDefault();
                if (unit == null || key == unit.FullName) {
                    continue;
                }

                dict.Remove(key);
                var newKey = unit.FullName;
                if (dict.ContainsKey(newKey)) {
                    dict[newKey].Add(unit);
                } else {
                    dict.Add(newKey, new List<DomainUnit> { new DomainUnit(unit.FullName, '\0') });
                }
            }
        }

        public IEnumerable<string> GetTopDomains() => dict.Keys;
        public IEnumerable<string> GetSoloDomains() => dict
            .Where(x => x.Value.Count == 1)
            .Select(x => x.Key);

        public IEnumerable<string> GetTopDomainsWithSomeSubDomains() => dict
            .Keys
            .Except(GetSoloDomains());

        public IEnumerable<DomainUnit> GetSubDomains(string topDomain) {
            if (dict.TryGetValue(topDomain, out var list)) {
                return list;
            }
            return new DomainUnit[0];
        }
    }
}
