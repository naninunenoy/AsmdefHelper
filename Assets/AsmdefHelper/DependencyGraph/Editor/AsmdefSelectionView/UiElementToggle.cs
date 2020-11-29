using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class UiElementToggle : IToggle {
        public readonly Toggle Toggle;
        public UiElementToggle(Toggle toggle) => Toggle = toggle;
        public string Name { get => Toggle.text; set => Toggle.text = value; }
        public bool IsOn { get => Toggle.value; set => Toggle.value = value; }
    }
}
