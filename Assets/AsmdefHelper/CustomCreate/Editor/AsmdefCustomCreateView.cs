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
            window.minSize = new Vector2(200, 200);
            window.maxSize = new Vector2(2000, 2000);
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

#if UNITY_2020_1_OR_NEWER
            VisualElement labelFromUXML = visualTree.Instantiate();
#else
            VisualElement labelFromUXML = visualTree.CloneTree();
#endif
            root.Add(labelFromUXML);

            // UI取得
            var pathTextField = root.Q<TextField>(className: "PathTextField");
            var nameTextField = root.Q<TextField>(className: "NameTextField");
            var allowUnsafeToggle = root.Q<Toggle>(className: "AllowUnsafeToggle");
            var autoReferencedToggle = root.Q<Toggle>(className: "AutoReferencedToggle");
            var noEngineReferencesToggle = root.Q<Toggle>(className: "NoEngineReferencesToggle");
            var overrideReferencesToggle = root.Q<Toggle>(className: "OverrideReferencesToggle");
            var rootNamespaceTextField = root.Q<TextField>(className: "RootNamespaceTextField");
            var isEditorToggle = root.Q<Toggle>(className: "IsEditorToggle");
            var createButton = root.Q<Button>(className: "CreateButton");

            // PathとNameの初期値
            var asset = Selection.activeObject;
            var assetPath = AssetDatabase.GetAssetPath(asset);
            var directory = string.IsNullOrWhiteSpace(assetPath) ? "Assets/" : assetPath;
            pathTextField.value = directory;
            var defaultName = directory.Replace("Assets/", "").Replace('/', '.');
            nameTextField.value = defaultName;

            // RootNamespace が設定できるのは2020.2以降
#if UNITY_2020_2_OR_NEWER
            rootNamespaceTextField.value = defaultName;
#else
            root.Q<Box>(className: "Box").Remove(rootNamespaceTextField);
#endif
            // .asmdefを作成して閉じる
            createButton.clickable.clicked += () => {
                var asmdefName = nameTextField.value;
                var asmdef = new AssemblyDefinitionJson {
                    name = asmdefName,
#if UNITY_2020_2_OR_NEWER
                    rootNamespace = rootNamespaceTextField.value,
#endif
                    allowUnsafeCode = allowUnsafeToggle.value,
                    autoReferenced = autoReferencedToggle.value,
                    overrideReferences = overrideReferencesToggle.value,
                    noEngineReferences = noEngineReferencesToggle.value,
                    includePlatforms = isEditorToggle.value ? new[] { "Editor" } : new string[0]
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
