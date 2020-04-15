using UnityEditor;
using UnityEditor.VisualStudioIntegration;

namespace AsmdefHelper.UnityInternal {
    public static class SolutionSynchronizerWrapper {

        static readonly SolutionSynchronizer synchronizer;

        static SolutionSynchronizerWrapper() {
            synchronizer = SyncVS.Synchronizer;
        }

        public static void SyncSolution() {
            SyncVS.SyncSolution();
        }


        public static void SynchronizerSync() {
            synchronizer?.Sync();
        }
    }
}
