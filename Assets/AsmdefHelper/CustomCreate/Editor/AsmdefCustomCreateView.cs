using System.IO;
using System.Text;
using AsmdefHelper.CustomCreate.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


// original: https://github.com/baba-s/UniAssemblyDefinitionCreator
public class AsmdefCustomCreateView : EditorWindow {
    [MenuItem("Assets/Asmdef Helper/Create custom asmdef", false, 0)]
    public static void ShowWindow() {
        var window = GetWindow<AsmdefCustomCreateView>();
        window.titleContent = new GUIContent("AsmdefCustomCreateView");
        window.minSize = new Vector2(200,180);
        window.maxSize = new Vector2(2000,180);
    }

    public void OnEnable() {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree =
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                "Assets/AsmdefHelper/CustomCreate/Editor/AsmdefCustomCreateView.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // UI取得
        var PathTextField = root.Q<TextField>(className: "PathTextField");
        var NameTextField = root.Q<TextField>(className: "NameTextField");
        var AllowUnsafeToggle = root.Q<Toggle>(className: "AllowUnsafeToggle");
        var AutoReferencedToggle = root.Q<Toggle>(className: "AutoReferencedToggle");
        var OverrideReferencesToggle = root.Q<Toggle>(className: "OverrideReferencesToggle");
        var NoEngineReferencesToggle = root.Q<Toggle>(className: "NoEngineReferencesToggle");
        var IsEditorToggle = root.Q<Toggle>(className: "IsEditorToggle");
        var CreateButton = root.Q<Button>(className: "CreateButton");

        // PathとNameの初期値
        var asset = Selection.activeObject;
        var assetPath = AssetDatabase.GetAssetPath(asset);
        var directory = string.IsNullOrWhiteSpace(assetPath) ? "Assets/" : assetPath;
        PathTextField.value = directory;
        NameTextField.value = directory.Replace("Assets/", "").Replace('/', '.');

        // .asmdefを作成して閉じる
        CreateButton.clickable.clicked += () => {
            var asmdefName = NameTextField.value;
            var asmdef = new AssemblyDefinitionJson {
                name = asmdefName,
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
