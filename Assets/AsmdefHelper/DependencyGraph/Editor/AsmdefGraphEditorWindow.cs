using UnityEditor;
using UnityEditor.Compilation;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow, IToggleCheckDelegate {
        static AsmdefSelectionView selectionWindow;
        AsmdefGraphView graphView;

        [MenuItem("AsmdefHelper/Open DependencyGraph", priority = 2000)]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("Asmdef Dependency");
        }

        void OnEnable() {
            // .asmdefをすべて取得
            var asmdefs = CompilationPipeline.GetAssemblies();
            // viewの作成
            graphView = new AsmdefGraphView(asmdefs) {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);

            // 選択ウィンドウも作成
            selectionWindow = GetWindow<AsmdefSelectionView>("Asmdef Selection");
            selectionWindow.SetAsmdef(asmdefs, this);
        }

        // 片方を閉じる
        void OnDestroy() {
            if (selectionWindow != null) {
                selectionWindow.Close();
            }
            selectionWindow = null;
        }

        void IToggleCheckDelegate.OnSelectionChanged(string label, bool isChecked) {
            graphView.SetNodeVisibility(label, isChecked);
        }
    }
}
