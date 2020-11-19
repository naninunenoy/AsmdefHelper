using UnityEngine;

namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public struct AlignParam {
        public readonly int tryCount;

        public readonly float basicDistance;
        public readonly float nodeWidth;
        public readonly float nodeHeight;

        public readonly float relationK;
        public readonly float relationNaturalLength;
        public readonly float repulsivePower;
        public readonly float threshold;

        public AlignParam(int tryCount, float basicDistance, float nodeWidth, float nodeHeight,
            float relationK, float relationNaturalLength, float repulsivePower, float threshold) {
            this.tryCount = tryCount;
            this.basicDistance = basicDistance;
            this.nodeWidth = nodeWidth;
            this.nodeHeight = nodeHeight;
            this.relationK = relationK;
            this.relationNaturalLength = relationNaturalLength;
            this.repulsivePower = repulsivePower;
            this.threshold = threshold;
        }

        public static AlignParam Default() => new AlignParam(1000, 100, 600, 100, -0.01F, 300, 0.01F, 300.0F);
    }

    public static class AlignParamEx {
        public static Vector2 接続したノード同士はばねによって引き合う(this AlignParam align, Vector2 target, Vector2 other) {
            var k = align.relationK;
            var nl = align.relationNaturalLength;

            var l = (target - other).magnitude;
            var delta = l - nl;

            return -(delta * k * (other - target).normalized);
        }
        public static Vector2 全ノードは互いに斥力が発生する(this AlignParam align, Vector2 target, Vector2 other) {
            var l = (other - target).magnitude;
            if (l < align.threshold)
            {
                return -(other - target).normalized * ((align.threshold - l) * align.repulsivePower);
            }
            return Vector2.zero;
        }
    }
}
