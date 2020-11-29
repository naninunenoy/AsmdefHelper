using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class ToggleGroup : IToggle {
        readonly IToggle masterToggle;
        readonly IEnumerable<IToggle> slaveToggles;

        public ToggleGroup(IToggle masterToggle, IEnumerable<IToggle> slaveToggles) {
            this.masterToggle = masterToggle;
            this.slaveToggles = slaveToggles;
        }

        public string Name {
            get => masterToggle.Name;
            set => masterToggle.Name = value;
        }

        public bool IsOn {
            get => masterToggle.IsOn;
            set {
                masterToggle.IsOn = value;
                foreach (var toggle in slaveToggles) {
                    toggle.IsOn = value;
                }
            }
        }
    }
}
