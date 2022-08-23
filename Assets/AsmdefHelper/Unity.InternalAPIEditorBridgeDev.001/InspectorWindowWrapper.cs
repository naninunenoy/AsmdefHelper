using UnityEditor;


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
#if UNITY_2021_1_OR_NEWER
                var assetImporterEditor = editor as UnityEditor.AssetImporters.AssetImporterEditor;
#else
                var assetImporterEditor = editor as UnityEditor.Experimental.AssetImporters.AssetImporterEditor;
#endif

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
