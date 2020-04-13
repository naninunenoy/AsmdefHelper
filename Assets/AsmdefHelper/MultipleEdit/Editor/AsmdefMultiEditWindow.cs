using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEditorInternal;
using UnityEngine;

namespace AsmdefMultiEdit.Editor
{
    public class AsmdefMultiEditWindow : EditorWindow
    {
        static IList<InspectorWindow> windows = new List<InspectorWindow>();

        [MenuItem("Window/Asmdef Multiple Edit/1.Search asmdef in project")]
        public static void Search()
        {
            var projectBrowser = GetWindow<ProjectBrowser>();
            projectBrowser.SetSearch("t:AssemblyDefinitionAsset");
        }

        [MenuItem("Window/Asmdef Multiple Edit/2.Open selected asmdef inspector view")]
        public static void Open()
        {
            var asmdefs = Selection.GetFiltered(typeof(AssemblyDefinitionAsset), SelectionMode.TopLevel);
            if (!asmdefs.Any())
            {
                Debug.Log("no AssemblyDefinitionAsset");
                return;
            }

            CloseWindows();
            foreach (var adf in asmdefs)
            {
                Selection.objects = new[] { adf };
                var w = CreateWindow<InspectorWindow>();
                // LockすることでInspectorWindowの表示を固定する
                w.isLocked = true;
                windows.Add(w);
            }
        }

        [MenuItem("Window/Asmdef Multiple Edit/3.All apply and close")]
        public static void Apply()
        {
            foreach (var w in windows)
            {
                foreach (var editor in w.tracker.activeEditors)
                {
                    var assetImporterEditor = editor as AssetImporterEditor;

                    if (assetImporterEditor != null && assetImporterEditor.HasModified())
                    {
                        assetImporterEditor.ApplyAndImport();
                    }
                }
                w.Close();
            }
            windows.Clear();
        }

        static void CloseWindows()
        {
            foreach (var w in windows)
            {
                w.Close();
            }
            windows.Clear();
        }
    }
}
