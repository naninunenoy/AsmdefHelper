using UnityEditor;

namespace AsmdefHelper.UnityInternal {
    public class ProjectBrowserWrapper : EditorWindow {
        ProjectBrowser projectBrowser;

        public void GetProjectBrowser() {
            projectBrowser = GetWindow<ProjectBrowser>();
        }

        public void SetSearch(string searchText) {
            if (projectBrowser != null) {
                projectBrowser.SetSearch(searchText);
            }
        }
    }
}
