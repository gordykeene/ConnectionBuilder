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
            => GenerateAllPermutations(GenerateAllConnections(fromNodes, toNodes))
                .Distinct()
                .Where(c => IsValidConnectionSet(c, fromNodes, toNodes));

        private static ConnectionSet GenerateAllConnections(
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
            => fromNodes.SelectMany(f =>
               toNodes.Select(t => f.MakeConnection(t))).ToConnectionSet();

        private static IEnumerable<ConnectionSet> GenerateAllPermutations(
            ConnectionSet connections)
        {
            var count = connections.Count();
            if (count > 0)
            {
                yield return connections;
                for (var skipIndex = 0; skipIndex < count; ++skipIndex)
                {
                    var perm = Permutation(connections, skipIndex);
                    var perms = GenerateAllPermutations(perm.ToConnectionSet()).Distinct();
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

        private static bool IsValidConnectionSet(
            ConnectionSet connections,
            IEnumerable<MapNodeInColumn> fromNodes,
            IEnumerable<MapNodeInColumn> toNodes)
        {
            // All "from" nodes must have a connection
            foreach (var n in fromNodes)
            {
                bool found() {
                    foreach (var c in connections) 
                        if (n.Y == c.From.Y) return true;
                    return false;
                };
                if (!found()) return false;
            }

            // Add "to" nodes must have a connection
            foreach (var n in toNodes)
            {
                bool found()
                {
                    foreach (var c in connections) 
                        if (n.Y == c.To.Y) return true;
                    return false;
                };
                if (!found()) return false;
            }

            // Connections must not cross
            foreach (var c0 in connections)
                foreach (var c1 in connections)
                    if (c0 != c1)
                    {
                        if (c0.From.Y < c1.From.Y
                            && c0.To.Y > c1.To.Y) return false;
                        if (c0.From.Y > c1.From.Y
                            && c0.To.Y < c1.To.Y) return false;
                    }

            // Winner winner chicken dinner
            return true;
        }
    }
}
