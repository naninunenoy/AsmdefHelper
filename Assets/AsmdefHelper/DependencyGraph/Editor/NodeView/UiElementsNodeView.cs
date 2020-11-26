using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.NodeView {
    public class UiElementsNodeView : Node, INodeView {

        public string Label {
            get => title;
            set => title = value;
        }

        public float PositionX {
            get => transform.position.x;
            set => transform.position = new Vector3(value, PositionY, transform.position.z);
        }

        public float PositionY {
            get => transform.position.y;
            set => transform.position = new Vector3(PositionX, value, transform.position.z);
        }

        public float Height => contentRect.height;

        public float Width => contentRect.width;

        public virtual bool Visibility {
            get => visible;
            set => visible = value;
        }
    }
}
