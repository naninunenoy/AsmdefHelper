using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.UIElements;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefSelectionView : EditorWindow {
        const int toggleCount = 1000;
        static EditorWindow graphWindow;
        readonly ToggleDict groupMasterToggleDict = new ToggleDict();
        readonly ToggleDict toggleDict = new ToggleDict();
        IToggleCheckDelegate toggleDelegate;

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

#if UNITY_2020_1_OR_NEWER
            VisualElement labelFromUXML = visualTree.Instantiate();
#else
            VisualElement labelFromUXML = visualTree.CloneTree();
#endif
            root.Add(labelFromUXML);
        }

        public void SetAsmdef(Assembly[] assemblies, IToggleCheckDelegate toggleDelegate_) {
            var sortedAssemblies = assemblies.OrderBy(x => x.name).ToArray();
            var scrollView = rootVisualElement.Q<ScrollView>(className: "ScrollView");
            toggleDict.Clear();
            for (var i = 0; i < toggleCount; i++) {
                var toggle = rootVisualElement.Q<Toggle>(className: $"toggle{i}");
                if (i < sortedAssemblies.Length) {
                    var assemblyName = sortedAssemblies[i].name;
                    toggle.text = assemblyName;
                    toggle.value = true;
                    toggleDict.Add(assemblyName, new UiElementToggle(toggle));
                } else {
                    scrollView.Remove(toggle);
                }
            }

            // グループに分ける
            var group = new DomainGroup();
            group.Create(sortedAssemblies.Select(x => x.name));
            var tops = group.GetTopDomainsWithSomeSubDomains().ToArray();
            foreach (var top in tops) {
                var topToggle = new Toggle { text = top, value = true};
                var slaveToggles = new List<IToggle>();
                Toggle firstToggle = null;
                var domains = group.GetSubDomains(top);
                foreach (var domain in domains) {
                    var isLast = domains.Last() == domain;
                    if (toggleDict.TryGetToggle(domain.FullName, out var toggle)) {
                        var keisen = isLast ? "└" : "├";
                        toggle.Name = domain.HasSubDomain() ? $"{keisen} {domain.SubDomain}" : toggle.Name;
                        slaveToggles.Add(toggle);
                        if (firstToggle == null && toggle is UiElementToggle y) {
                            firstToggle = y.Toggle;
                        }
                    }
                }

                var toggleGroup = new ToggleGroup(new UiElementToggle(topToggle), slaveToggles);
                if (firstToggle != null) {
                    var index = scrollView.IndexOf(firstToggle);
                    // グループに属する toggle は box に入れる
                    var box = new Box();
                    scrollView.Insert(index, topToggle);
                    scrollView.Insert(index + 1, box);
                    foreach (var slaveToggle in slaveToggles) {
                        if (slaveToggle is UiElementToggle x) {
                            box.Add(x.Toggle);
                        }
                    }
                }

                groupMasterToggleDict.Add(top, toggleGroup);
            }

            toggleDelegate = toggleDelegate_;
        }

        void OnGUI() {
            // toggle の更新を監視 (onValueChangedが無さそう)
            // ToggleGroup の長
            var updatedGroups = groupMasterToggleDict.ScanUpdate().ToArray();
            groupMasterToggleDict.OverwriteToggles(updatedGroups.Select(x => x.Item1));
            // 普通の Toggle達
            var updated = toggleDict.ScanUpdate().ToArray();
            foreach (var x in updated) {
                var (key, current) = x;
                toggleDelegate?.OnSelectionChanged(key, current);
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
