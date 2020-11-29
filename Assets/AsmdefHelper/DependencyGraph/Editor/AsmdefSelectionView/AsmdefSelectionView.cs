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

        Dictionary<string, (bool, IToggle)> topToggleDict;
        Dictionary<string, (bool, IToggle)> toggleDict;
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

            VisualElement labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);
        }

        public void SetAsmdef(Assembly[] assemblies, IToggleCheckDelegate toggleDelegate_) {
            toggleDict = new Dictionary<string, (bool, IToggle)>(assemblies.Length);
            var sortedAssemblies = assemblies.OrderBy(x => x.name).ToArray();
            var scrollView = rootVisualElement.Q<ScrollView>(className: "ScrollView");
            for (var i = 0; i < toggleCount; i++) {
                var toggle = rootVisualElement.Q<Toggle>(className: $"toggle{i}");
                if (i < sortedAssemblies.Length) {
                    var assemblyName = sortedAssemblies[i].name;
                    toggle.text = assemblyName;
                    toggle.value = true;
                    toggleDict.Add(assemblyName, (true, new UiElementToggle(toggle)));
                } else {
                    scrollView.Remove(toggle);
                }
            }

            // グループに分ける
            topToggleDict = new Dictionary<string, (bool, IToggle)>();
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
                    if (toggleDict.TryGetValue(domain.FullName, out var x)) {
                        var (_, toggle) = x;
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

                topToggleDict.Add(top, (true, toggleGroup));
            }

            toggleDelegate = toggleDelegate_;
        }

        void OnGUI() {
            var updated = new Dictionary<string, (bool, IToggle)>();
            foreach (var pair in topToggleDict) {
                var (prev, toggle) = pair.Value;
                var current = toggle.IsOn;
                if (prev != current) {
                    // groupのtopのtoggleのisOnを明示的にsetすることで、子のtoggleにも反映される
                    toggle.IsOn = current;
                    updated.Add(pair.Key, (current, toggle));
                }
            }
            // 状態更新
            if (updated.Any()) {
                foreach (var pair in updated) {
                    topToggleDict[pair.Key] = pair.Value;
                }
            }
            // toggle の更新を監視 (onValueChangedが無さそう)
            updated.Clear();
            foreach (var pair in toggleDict) {
                var (prev, toggle) = pair.Value;
                var current = toggle.IsOn;
                if (prev != current) {
                    toggleDelegate?.OnSelectionChanged(pair.Key, current);
                    updated.Add(pair.Key, (current, toggle));
                }
            }
            // 状態更新
            if (updated.Any()) {
                foreach (var pair in updated) {
                    toggleDict[pair.Key] = pair.Value;
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
