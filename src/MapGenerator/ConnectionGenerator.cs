using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectionBuilder
{
    public class ConnectionGenerator : IConnectionGenerator
    {
        public IEnumerable<IEnumerable<Connection>> GeneratePossibilities(
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
        {
            var combinations = GenerateAllCombinations(fromNodes, toNodes);

            var result = new List<IEnumerable<Connection>>();
            result.Add(combinations);
            return result;
        }

        public static IEnumerable<Connection> GenerateAllCombinations(
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
            => fromNodes.SelectMany(f =>
                    toNodes.Select(t => f.MakeConnection(t)));
    }
}
