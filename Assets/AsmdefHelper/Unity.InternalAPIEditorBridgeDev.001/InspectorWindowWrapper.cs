using UnityEditor;
using UnityEditor.Experimental.AssetImporters;

namespace AsmdefHelper.UnityInternal {
    public class InspectorWindowWrapper : EditorWindow{
        InspectorWindow inspectorWindow;

        public void GetInspectorWindow() {
            inspectorWindow = CreateWindow<InspectorWindow>();
        }

        public void Lock(bool isLock) {
            if (inspectorWindow != null) {
                inspectorWindow.isLocked = isLock;
            }
        }

        public void AllApply() {
            foreach (var editor in inspectorWindow.tracker.activeEditors) {
                var assetImporterEditor = editor as AssetImporterEditor;

                if (assetImporterEditor != null && assetImporterEditor.HasModified()) {
                    assetImporterEditor.ApplyAndImport();
                }
            }
        }

        public void CloseInspectorWindow() {
            if (inspectorWindow != null) {
                inspectorWindow.Close();
            }
        }
    }
}
