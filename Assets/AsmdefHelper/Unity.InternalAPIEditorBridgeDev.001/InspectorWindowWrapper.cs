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
    }
}
