using UnityEditor.Experimental.GraphView;

namespace AsmdefGraph.Editor {
    public class AsmdefGraphView : GraphView {
        public AsmdefGraphView() : base() {
            AddElement(new AsmdefNode());
        }
    }
}
