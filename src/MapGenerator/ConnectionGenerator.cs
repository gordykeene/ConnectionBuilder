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
            var allCombinations = GenerateAllCombinations(fromNodes, toNodes);
#if !FAKE_IT
            return new[] { allCombinations };
#else 
            var validCombinations = allCombinations
                .Aggregate(
                    new[] { Enumerable.Empty<Connection>() },
                    (accumulator, sequence) =>
                        fromNodes.SelectMany(f =>
                            toNodes.Select(t => f.MakeConnection(t)));

            return validCombinations;
#endif
        }

        public static IEnumerable<Connection> GenerateAllCombinations(
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
            => fromNodes.SelectMany(f =>
               toNodes.Select(t => f.MakeConnection(t)));
    }
}
