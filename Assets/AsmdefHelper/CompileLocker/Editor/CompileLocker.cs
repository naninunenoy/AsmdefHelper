using UnityEngine;
using UnityEditor;

/// <summary>
/// original from: https://gist.github.com/decoc/bde047ac7ad8c9bfce7eb408f2712424
/// This editor utility can lock/unlock unity script compile from menu item.
/// See more https://raspberly.hateblo.jp/entry/InvalidateUnityCompile
/// </summary>
namespace AsmdefHelper.CompileLocker.Editor {
    public static class CompileLocker {
        const string menuPath = "Window/Asmdef Helper/Compile Lock";

        [MenuItem("Window/Asmdef Helper/Compile Lock", false, 1)]
        [MenuItem("Assets/Asmdef Helper/Compile Lock", false, 1000)]
        static void Lock() {
            var isLocked = Menu.GetChecked(menuPath);
            if (isLocked) {
                Debug.Log("Compile Unlocked");
                EditorApplication.UnlockReloadAssemblies();
                Menu.SetChecked(menuPath, false);
            } else {
                Debug.Log("Compile Locked");
                EditorApplication.LockReloadAssemblies();
                Menu.SetChecked(menuPath, true);
            }
        }
    }
}
