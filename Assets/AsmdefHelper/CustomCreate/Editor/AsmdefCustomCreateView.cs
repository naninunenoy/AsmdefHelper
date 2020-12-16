using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AsmdefHelper.CustomCreate.Editor {
    // original: https://github.com/baba-s/UniAssemblyDefinitionCreator
    public class AsmdefCustomCreateView : EditorWindow {
        [MenuItem("Assets/AsmdefHelper/Create custom asmdef")]
        public static void ShowWindow() {
            var window = GetWindow<AsmdefCustomCreateView>();
            window.titleContent = new GUIContent("AsmdefCustomCreateView");
            window.minSize = new Vector2(200, 180);
            window.maxSize = new Vector2(2000, 180);
        }

        public void OnEnable() {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/AsmdefHelper/CustomCreate/Editor/AsmdefCustomCreateView.uxml");
            if (visualTree == null) {
                visualTree =
                    AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                        "Packages/dev.n5y.asmdefhelper/AsmdefHelper/CustomCreate/Editor/AsmdefCustomCreateView.uxml");
            }

            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            // UI取得
            var PathTextField = root.Q<TextField>(className: "PathTextField");
            var NameTextField = root.Q<TextField>(className: "NameTextField");
            var AllowUnsafeToggle = root.Q<Toggle>(className: "AllowUnsafeToggle");
            var AutoReferencedToggle = root.Q<Toggle>(className: "AutoReferencedToggle");
            var NoEngineReferencesToggle = root.Q<Toggle>(className: "NoEngineReferencesToggle");
            var OverrideReferencesToggle = root.Q<Toggle>(className: "OverrideReferencesToggle");
            var RootNamespaceTextField = root.Q<TextField>(className: "RootNamespaceTextField");
            var IsEditorToggle = root.Q<Toggle>(className: "IsEditorToggle");
            var CreateButton = root.Q<Button>(className: "CreateButton");

            // PathとNameの初期値
            var asset = Selection.activeObject;
            var assetPath = AssetDatabase.GetAssetPath(asset);
            var directory = string.IsNullOrWhiteSpace(assetPath) ? "Assets/" : assetPath;
            PathTextField.value = directory;
            var defaultName = directory.Replace("Assets/", "").Replace('/', '.');
            NameTextField.value = defaultName;

            // RootNamespace が設定できるのは2020.2以降
#if UNITY_2020_2_OR_NEWER
            RootNamespaceTextField.value = defaultName;
#else
            root.Q<Box>(className: "Box").Remove(RootNamespaceTextField);
#endif
            // .asmdefを作成して閉じる
            CreateButton.clickable.clicked += () => {
                var asmdefName = NameTextField.value;
                var asmdef = new AssemblyDefinitionJson {
                    name = asmdefName,
#if UNITY_2020_2_OR_NEWER
                    rootNamespace = RootNamespaceTextField.value,
#endif
                    allowUnsafeCode = AllowUnsafeToggle.value,
                    autoReferenced = AutoReferencedToggle.value,
                    overrideReferences = OverrideReferencesToggle.value,
                    noEngineReferences = NoEngineReferencesToggle.value,
                    includePlatforms = IsEditorToggle.value ? new[] { "Editor" } : new string[0]
                };
                var asmdefJson = JsonUtility.ToJson(asmdef, true);
                var asmdefPath = $"{directory}/{asmdefName}.asmdef";
                File.WriteAllText(asmdefPath, asmdefJson, Encoding.UTF8);
                AssetDatabase.Refresh();
                Close();
            };
        }
    }
}
