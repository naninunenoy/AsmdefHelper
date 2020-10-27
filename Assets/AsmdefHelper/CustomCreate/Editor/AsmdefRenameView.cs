using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AsmdefHelper.CustomCreate.Editor {
    public class AsmdefRenameView : EditorWindow {
        static string renameAsmdefPath = "";
        static string asmdefDirectory = "";

        [MenuItem("Assets/AsmdefHelper/Rename asmdef")]
        public static void ShowWindow() {
            // PathとNameの初期値
            var asset = Selection.activeObject;
            renameAsmdefPath = AssetDatabase.GetAssetPath(asset);
            asmdefDirectory = Path.GetDirectoryName(renameAsmdefPath);
            // asmdefが選択されている時のみ開く
            var extension = renameAsmdefPath.Split('.').LastOrDefault();
            if (extension == "asmdef") {
                var window = GetWindow<AsmdefRenameView>();
                window.titleContent = new GUIContent("AsmdefRenameView");
                window.minSize = new Vector2(200, 80);
                window.maxSize = new Vector2(2000, 80);
            }
        }

        public void OnEnable() {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/AsmdefHelper/CustomCreate/Editor/AsmdefRenameView.uxml");
            if (visualTree == null) {
                visualTree =
                    AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                        "Packages/dev.n5y.asmdefhelper/AsmdefHelper/CustomCreate/Editor/AsmdefRenameView.uxml");
            }

            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            // UI取得
            var PathTextField = root.Q<TextField>(className: "PathTextField");
            var NameTextField = root.Q<TextField>(className: "NameTextField");
            var CreateButton = root.Q<Button>(className: "RenameButton");

            // 既存のasmdef読み込み
            var orgText = File.ReadAllText(renameAsmdefPath);
            var asmdef = JsonUtility.FromJson<AssemblyDefinitionJson>(orgText);

            // 既存パラメータの反映
            PathTextField.value = asmdefDirectory;
            NameTextField.value = asmdef.name;

            // .asmdefのnameとファイル名を更新して閉じる
            CreateButton.clickable.clicked += () => {
                // nameのみ更新
                var asmdefName = NameTextField.value;
                asmdef.name = asmdefName;
                var asmdefJson = JsonUtility.ToJson(asmdef, true);
                var newAsmdefPath = $"{asmdefDirectory}/{asmdefName}.asmdef";
                // 新asmdef作成
                File.WriteAllText(newAsmdefPath, asmdefJson, Encoding.UTF8);
                // 旧asmdef削除
                File.Delete(renameAsmdefPath);
                AssetDatabase.Refresh();
                Close();
            };
        }
    }
}
