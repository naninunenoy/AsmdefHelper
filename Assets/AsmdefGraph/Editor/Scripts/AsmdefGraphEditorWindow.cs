using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphEditorWindow : EditorWindow {
        [MenuItem("Window/Open Asmdef Graph Window")]
        public static void Open() {
            GetWindow<AsmdefGraphEditorWindow>("AsmdefGraphWindow");
        }

        void OnEnable() {
            var asmdefs = new List<AsmdefFile>();
            var projectPath = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar;
            // プロジェクトのasmdefを全検索
            var fullPathes = Directory.EnumerateFiles(projectPath, "*.asmdef", SearchOption.AllDirectories);
            // asmdefの内容取得
            foreach (var fullPath in fullPathes) {
                var assetPath = fullPath.Replace(projectPath, "");
                var asmdef = new AsmdefFile();
                asmdef.LoadFromPath(fullPath, assetPath);
                asmdefs.Add(asmdef);
            }
            // viewの作成
            var graphView = new AsmdefGraphView(asmdefs) {
                style = { flexGrow = 1 }
            };
            rootVisualElement.Add(graphView);
        }
    }
}
