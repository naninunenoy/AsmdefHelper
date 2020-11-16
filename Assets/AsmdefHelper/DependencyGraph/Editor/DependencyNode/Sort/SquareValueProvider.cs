namespace AsmdefHelper.DependencyGraph.Editor.DependencyNode.Sort {
    public static class SquareValueProvider {
        public static int ProvideNearestSquareValue(int count) {
            if (count < 1) return 0;
            if (count == 1) return 1;
            var value = 1;
            while (value < 100) {
                var square = value * value;
                var nextValue = value + 1;
                var nextSquare = nextValue * nextValue;
                value = nextValue;
                if (square <= count && count <= nextSquare) {
                    break;
                }
            }
            return value;
        }
    }
}
