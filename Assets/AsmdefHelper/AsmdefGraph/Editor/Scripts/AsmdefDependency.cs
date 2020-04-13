using System.Collections.Generic;

namespace AsmdefGraph.Editor {
    public class AsmdefDependency {
        public string DependsFrom { get; }
        public IEnumerable<string> DependsTo { get; }

        public AsmdefDependency(string key, IEnumerable<string> value) {
            DependsFrom = key;
            DependsTo = value;
        }
    }
}
