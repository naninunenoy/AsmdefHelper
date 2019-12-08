using System.IO;
using System.Linq;
using UnityEditor;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("Window/Open Asmdef Graph Window")]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("AsmdefGraphWindow");
        }

        void OnEnable() {
            // プロジェクトのasmdefを全検索
            var asmdefs = Directory.EnumerateFiles(
                Directory.GetCurrentDirectory(), "*.asmdef", SearchOption.AllDirectories);
            var asmdefNames = asmdefs
                .Select(x => x.Split('\\').LastOrDefault())
                .Select(x => x.Replace(".asmdef", ""))
                .Where(x => !string.IsNullOrEmpty(x));
            var graphView = new AsmdefGraphView(asmdefNames) {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);
        }
    }
}
