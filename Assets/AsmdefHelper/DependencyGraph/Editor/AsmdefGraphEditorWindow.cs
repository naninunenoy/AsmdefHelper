using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("Window/Asmdef Helper/Open DependencyGraph", priority = 2000)]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("Asmdef Dependency");
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
