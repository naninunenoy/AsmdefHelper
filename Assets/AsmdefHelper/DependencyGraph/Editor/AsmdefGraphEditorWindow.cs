using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("Window/Open Asmdef Graph Window")]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("AsmdefGraphWindow");
        }

        void OnEnable() {
            // .asmdefをすべて取得
            var asmdefs = CompilationPipeline.GetAssemblies();
            var allDependencies = new List<AsmdefDependency>();
            foreach (var asmdef in asmdefs) {
                allDependencies.Add(
                    new AsmdefDependency(
                        asmdef.name, 
                        asmdef.assemblyReferences?.Select(x => x.name) ?? new string[0])
                    );
            }
            // viewの作成
            var graphView = new AsmdefGraphView(allDependencies) {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);
        }
    }
}
