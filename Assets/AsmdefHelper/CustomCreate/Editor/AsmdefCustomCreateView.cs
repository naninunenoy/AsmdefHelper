using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class AsmdefCustomCreateView : EditorWindow
{
    [MenuItem("Window/Project/AsmdefCustomCreateView")]
    public static void ShowExample()
    {
        AsmdefCustomCreateView wnd = GetWindow<AsmdefCustomCreateView>();
        wnd.titleContent = new GUIContent("AsmdefCustomCreateView");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/AsmdefHelper/CustomCreate/Editor/AsmdefCustomCreateView.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/AsmdefHelper/CustomCreate/Editor/AsmdefCustomCreateView.uss");
        VisualElement labelWithStyle = new Label("Hello World! With Style");
        labelWithStyle.styleSheets.Add(styleSheet);
        root.Add(labelWithStyle);
    }
}