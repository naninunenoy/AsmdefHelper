namespace AsmdefHelper.DependencyGraph.Editor {
    public interface IToggle {
        bool IsOn { set; get; }
        string Name { set; get; }
    }
}
