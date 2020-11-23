using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectionBuilder
{
    public static class ExtensionMethods
    {
        public static Connection MakeConnection(
            this MapNodeInColumn from,
            MapNodeInColumn to)
            => new Connection {
                From = from,
                To = to
            };

        public static string ToFormattedString(
            this IEnumerable<IEnumerable<Connection>> connectionSets)
            => $"{connectionSets.Count()} Set(s):\n---\n"
                + string.Join("\n", connectionSets
                    .OrderBy(s => s.Count())
                    .ThenBy(s => s.ToFormattedString())
                    .Select(s => s.ToFormattedString()));

        public static string ToFormattedString(
            this IEnumerable<Connection> connectionSet)
            => $"{connectionSet.Count()} Connection(s):\n"
                + string.Join("\n", connectionSet
                    .OrderBy(c => c.From.Y)
                    .ThenBy(c => c.To.Y)
                    .Select(c => $"  {c}"));

        public static ConnectionSet ToConnectionSet(this IEnumerable<Connection> set)
            => new ConnectionSet { Collection = set };
    }
}
