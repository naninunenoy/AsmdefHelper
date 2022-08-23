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
                window.minSize = new Vector2(200, 100);
                window.maxSize = new Vector2(2000, 100);
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

#if UNITY_2020_1_OR_NEWER
            VisualElement labelFromUXML = visualTree.Instantiate();
#else
            VisualElement labelFromUXML = visualTree.CloneTree();
#endif
            root.Add(labelFromUXML);

            // UI取得
            var pathTextField = root.Q<TextField>(className: "PathTextField");
            var nameTextField = root.Q<TextField>(className: "NameTextField");
            var rootNamespaceTextField = root.Q<TextField>(className: "RootNamespaceTextField");
            var createButton = root.Q<Button>(className: "RenameButton");

            // 既存のasmdef読み込み
            var orgText = File.ReadAllText(renameAsmdefPath);
            var asmdef = JsonUtility.FromJson<AssemblyDefinitionJson>(orgText);

            // 既存パラメータの反映
            pathTextField.value = asmdefDirectory;
            nameTextField.value = asmdef.name;
            var asmdefNameOrg = asmdef.name;

            // RootNamespace が設定できるのは2020.2以降
#if UNITY_2020_2_OR_NEWER
            rootNamespaceTextField.value = asmdef.rootNamespace;
#else
            root.Q<Box>(className: "Box").Remove(rootNamespaceTextField);
#endif

            // .asmdefのnameとファイル名を更新して閉じる
            createButton.clickable.clicked += () => {
                var asmdefName = nameTextField.value;
                asmdef.name = asmdefName;
#if UNITY_2020_2_OR_NEWER
                asmdef.rootNamespace = rootNamespaceTextField.value;
#endif
                var asmdefJson = JsonUtility.ToJson(asmdef, true);
                var newAsmdefPath = $"{asmdefDirectory}/{asmdefName}.asmdef";
                // 新asmdef作成
                File.WriteAllText(newAsmdefPath, asmdefJson, Encoding.UTF8);
                // ファイル名が変わった場合は旧asmdef削除
                if (asmdefNameOrg != asmdefName) {
                    FileUtil.DeleteFileOrDirectory(renameAsmdefPath);
                }
                AssetDatabase.Refresh();
                Close();
            };
        }
    }
}
