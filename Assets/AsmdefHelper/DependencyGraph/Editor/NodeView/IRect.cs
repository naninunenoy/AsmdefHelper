using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.NodeView {
    public interface IRect {
        float PositionX { set; get; }
        float PositionY { set; get; }
        float Height { get; }
        float Width { get; }
    }

    public static class ViewExtension {
        public static Vector2 GetPositionXY(this IRect rect) {
            return new Vector2(rect.PositionX, rect.PositionY);
        }
        public static void SetPositionXY(this IRect rect, Vector2 pos) {
            rect.PositionX = pos.x;
            rect.PositionY = pos.y;
        }
        public static Rect AsRect(this IRect rect) {
            return new Rect(rect.PositionX, rect.PositionY, rect.Width, rect.Height);
        }
    }
}
