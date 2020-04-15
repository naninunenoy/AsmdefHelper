using System.Collections.Generic;
using System.Linq;
using AsmdefHelper.UnityInternal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AsmdefHelper.MultipleEdit.Editor {
    public class AsmdefMultiEditWindow : EditorWindow {
        static IList<InspectorWindowWrapper> windows = new List<InspectorWindowWrapper>();

        [MenuItem("Window/Asmdef Helper/Find all asmdef in project")]
        public static void Search() {
            var browser = CreateInstance<ProjectBrowserWrapper>();
            browser.GetProjectBrowser();
            browser.SetSearch("t:AssemblyDefinitionAsset");
        }

        [MenuItem("Window/Asmdef Helper/Open selected asmdef inspector view")]
        [MenuItem("Assets/Asmdef Helper/Open selected asmdef inspector view")]
        public static void Open() {
            var asmdefs = Selection.GetFiltered(typeof(AssemblyDefinitionAsset), SelectionMode.TopLevel);
            if (!asmdefs.Any()) {
                Debug.Log("no AssemblyDefinitionAsset");
                return;
            }

            CloseWindows();
            foreach (var adf in asmdefs) {
                Selection.objects = new[] { adf };
                var w = CreateInstance<InspectorWindowWrapper>();
                w.GetInspectorWindow();
                // LockすることでInspectorWindowの表示を固定する
                w.Lock(true);
                windows.Add(w);
            }
        }

        [MenuItem("Window/Asmdef Helper/Apply all asmdef and close")]
        public static void Apply() {
            foreach (var w in windows) {
                w.AllApply();
                w.CloseInspectorWindow();
            }
            windows.Clear();
        }

        static void CloseWindows() {
            foreach (var w in windows) {
                w.CloseInspectorWindow();
            }
            windows.Clear();
        }
    }
}
