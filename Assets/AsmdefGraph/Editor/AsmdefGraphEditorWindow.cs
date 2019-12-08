using UnityEditor;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("Window/Open Asmdef Graph Window")]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("AsmdefGraphWindow");
        }

        void OnEnable() {
            var graphView = new AsmdefGraphView() {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);
        }
    }
}
