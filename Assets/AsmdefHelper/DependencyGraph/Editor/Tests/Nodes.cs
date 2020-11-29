using System.Collections.Generic;
using AsmdefHelper.DependencyGraph.Editor.DependencyNode;

namespace AsmdefHelper.DependencyGraph.Editor.Tests {

    public static class Ids {
        public static readonly NodeId _0 = new NodeId(0);
        public static readonly NodeId _1 = new NodeId(1);
        public static readonly NodeId _2 = new NodeId(2);
        public static readonly NodeId _3 = new NodeId(3);
        public static readonly NodeId _4 = new NodeId(4);
        public static readonly NodeId _5 = new NodeId(5);
        public static readonly NodeId _6 = new NodeId(6);
        public static readonly NodeId _7 = new NodeId(7);
        public static readonly NodeId _8 = new NodeId(8);
        public static readonly NodeId _9 = new NodeId(9);
    }
    public static class Profiles {
        public static readonly NodeProfile _0 = new NodeProfile(Ids._0, "node0");
        public static readonly NodeProfile _1 = new NodeProfile(Ids._1, "node1");
        public static readonly NodeProfile _2 = new NodeProfile(Ids._2, "node2");
        public static readonly NodeProfile _3 = new NodeProfile(Ids._3, "node3");
        public static readonly NodeProfile _4 = new NodeProfile(Ids._4, "node4");
        public static readonly NodeProfile _5 = new NodeProfile(Ids._5, "node5");
        public static readonly NodeProfile _6 = new NodeProfile(Ids._6, "node6");
        public static readonly NodeProfile _7 = new NodeProfile(Ids._7, "node7");
        public static readonly NodeProfile _8 = new NodeProfile(Ids._8, "node8");
        public static readonly NodeProfile _9 = new NodeProfile(Ids._9, "node9");
    }
    public static class Nodes {
        public static readonly IDependencyNode _0 = new HashSetDependencyNode(Profiles._0);
        public static readonly IDependencyNode _1 = new HashSetDependencyNode(Profiles._1);
        public static readonly IDependencyNode _2 = new HashSetDependencyNode(Profiles._2);
        public static readonly IDependencyNode _3 = new HashSetDependencyNode(Profiles._3);
        public static readonly IDependencyNode _4 = new HashSetDependencyNode(Profiles._4);
        public static readonly IDependencyNode _5 = new HashSetDependencyNode(Profiles._5);
        public static readonly IDependencyNode _6 = new HashSetDependencyNode(Profiles._6);
        public static readonly IDependencyNode _7 = new HashSetDependencyNode(Profiles._7);
        public static readonly IDependencyNode _8 = new HashSetDependencyNode(Profiles._8);
        public static readonly IDependencyNode _9 = new HashSetDependencyNode(Profiles._9);

        public static readonly IEnumerable<IDependencyNode> All = new[] {
            _0, _1, _2, _3, _4, _5, _6, _7, _8, _9
        };

        public static void Init() {
            foreach (var n in All) {
                n.Sources.Clear();
                n.Destinations.Clear();
            }
        }

        public static void SetSomeNodeDependency() {
            /*
             *     ┌->[1]-┐
             * [0]-┴->[2]-┴->[4]--->[5]--->[6]
             *         │             │
             *         V             │
             *        [3]<-----------┘
             *
             * [7]--->[8]    [9]
             *
             */
            Nodes._0.SetRequireNodes(new[] { Profiles._1, Profiles._2 });
            Nodes._1.SetRequireNodes(new[] { Profiles._4 });
            Nodes._2.SetRequireNodes(new[] { Profiles._3, Profiles._4 });
            Nodes._4.SetRequireNodes(new[] { Profiles._5 });
            Nodes._5.SetRequireNodes(new[] { Profiles._3, Profiles._6 });
            Nodes._7.SetRequireNodes(new[] { Profiles._8 });
        }
    }
}
