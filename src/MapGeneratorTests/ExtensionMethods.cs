using ConnectionBuilder;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionBuilderTests
{
    public static class ExtensionMethods
    {
        private static int _nodeId = 1000;

        public static IEnumerable<MapNodeInColumn> MakeNodes(this int[] values)
            => values.Select(v => v.MakeNode());

        public static MapNodeInColumn MakeNode(this int value)
            => new MapNodeInColumn {
                NodeId = _nodeId++.ToString(),
                Y = value
            };

        public static bool AreEquivalent(
            this IEnumerable<IEnumerable<Connection>> x,
            IEnumerable<IEnumerable<Connection>> y)
        {
            return x.SequenceEqual(y);
        }
    }
}
