using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefSelectionView : EditorWindow {
        const int toggleCount = 1000;
        static EditorWindow graphWindow;
        public void OnEnable() {
            graphWindow = GetWindow<AsmdefGraphEditorWindow>();

            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/AsmdefHelper/DependencyGraph/Editor/AsmdefSelectionView/AsmdefSelectionView.uxml");
            if (visualTree == null) {
                visualTree =
                    AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                        "Packages/dev.n5y.asmdefhelper/AsmdefHelper/DependencyGraph/Editor/AsmdefSelectionView/AsmdefSelectionView.uxml");
            }

            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);
        }

        public void SetAsmdef(Assembly[] assemblies) {
            var sortedAssemblies = assemblies.OrderBy(x => x.name).ToArray();
            var scrollView = rootVisualElement.Q<ScrollView>(className: "ScrollView");
            for (var i = 0; i < toggleCount; i++) {
                var toggle = rootVisualElement.Q<Toggle>(className: $"toggle{i}");
                if (i < sortedAssemblies.Length) {
                    toggle.text = sortedAssemblies[i].name;
                    toggle.value = true;
                } else {
                    scrollView.Remove(toggle);
                }
            }
        }

        // 片方を閉じる
        async void OnDestroy() {
            // 同フレームで OnDestroy を呼ぶと警告が出るので遅延実行
            await Task.Delay(1);
            if (graphWindow != null) {
                graphWindow.Close();
            }
            graphWindow = null;
        }
    }
}
