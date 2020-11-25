using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefSelectionView : EditorWindow {
        static EditorWindow graphWindow;
        public void OnEnable() {
            graphWindow = GetWindow<AsmdefGraphEditorWindow>();
        }

        VisualTreeAsset LoadVisualTreeAsset() {
            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/AsmdefHelper/DependencyGraph/Editor/AsmdefSelectionView/AsmdefSelectionView.uxml");
            if (visualTree == null) {
                visualTree =
                    AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                        "Packages/dev.n5y.asmdefhelper/AsmdefHelper/DependencyGraph/Editor/AsmdefSelectionView/AsmdefSelectionView.uxml");
            }
            return visualTree;
        }

        public void SetAsmdef(Assembly[] assemblies) {
            VisualElement scroll = new ScrollView();
            rootVisualElement.Add(scroll);
            var sorted = assemblies.OrderBy(x => x.name);
            foreach (var assembly in sorted) {
                var elm = LoadVisualTreeAsset().Instantiate();
                var toggle = elm.Q<Toggle>(className: "CheckBox");
                var label = elm.Q<Label>(className: "NameLabel");
                toggle.value = true;
                label.text = assembly.name;
                scroll.Add(elm);
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
