using System.Collections.Generic;
using System.Linq;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class ToggleDict {
        class TogglePair {
            public bool prevToggleValue;
            public readonly IToggle toggle;

            public TogglePair(IToggle toggle) {
                this.toggle = toggle;
                prevToggleValue = toggle.IsOn;
            }
        }

        readonly IDictionary<string, TogglePair> dict;

        public ToggleDict() {
            dict = new Dictionary<string, TogglePair>();
        }

        public void Add(string key, IToggle toggle) {
            dict.Add(key, new TogglePair(toggle));
        }

        public IEnumerable<(string, bool)> ScanUpdate() {
            return dict
                .Where(x => x.Value.prevToggleValue != x.Value.toggle.IsOn)
                .Select(x => {
                    // 更新も行う
                    dict[x.Key].prevToggleValue = x.Value.toggle.IsOn;
                    return (x.Key, x.Value.toggle.IsOn);
                });
        }

        public void OverwriteToggles(IEnumerable<string> updateKeys) {
            // UI 上の Toggle　の check が変わってもisOnには反映されないのでここで設定
            foreach (var keys in updateKeys) {
                var val = dict[keys];
                val.toggle.IsOn = val.toggle.IsOn;
            }
        }

        public bool TryGetToggle(string key, out IToggle toggle) {
            if (dict.TryGetValue(key, out var x)) {
                toggle = x.toggle;
                return true;
            }

            toggle = default;
            return false;
        }

        public void Clear() {
            dict.Clear();
        }
    }
}
