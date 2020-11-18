using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("AsmdefHelper/Open DependencyGraph", priority = 2000)]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("Asmdef Dependency");
        }

        void OnEnable() {
            // .asmdefをすべて取得
            var asmdefs = CompilationPipeline.GetAssemblies();
            // viewの作成
            var graphView = new AsmdefGraphView(asmdefs) {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);
        }
    }
}
