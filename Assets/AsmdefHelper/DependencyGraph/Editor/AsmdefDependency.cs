using System.Collections.Generic;

namespace AsmdefHelper.DependencyGraph.Editor {
    public class AsmdefDependency {
        public string DependsFrom { get; }
        public IEnumerable<string> DependsTo { get; }

        public AsmdefDependency(string key, IEnumerable<string> value) {
            DependsFrom = key;
            DependsTo = value;
        }
    }
}
