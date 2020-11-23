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
            var allCombinations = GenerateAllConnections(fromNodes, toNodes);
            
            var result = Permutations(allCombinations)
                .Distinct()
                .ToList();
            
            // TODO: Add filter above here

            return result;
        }

        // private static ConnectionSet GenerateAllConnections(
        private static ConnectionSet GenerateAllConnections(
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
            => fromNodes.SelectMany(f =>
               toNodes.Select(t => f.MakeConnection(t))).ToConnectionSet();


        private static IEnumerable<ConnectionSet> Permutations(
            ConnectionSet connections)
        {
            var count = connections.Count();
            if (count > 0)
            {
                yield return connections;
                for (var skipIndex = 0; skipIndex < count; ++skipIndex)
                {
                    var perm = Permutation(connections, skipIndex);
                    // yield return perm;
                    var perms = Permutations(perm.ToConnectionSet()).Distinct();
                    foreach (var p in perms) yield return p;
                }
            }
        }

        private static IEnumerable<Connection> Permutation(
            ConnectionSet connections,
            int skipIndex)
        {
            var connectionsArray = connections.ToArray();
            var count = connectionsArray.Count();
            for (var i = 0; i < count; ++i)
                if (i != skipIndex) yield return connectionsArray[i];
        }
    }
}
