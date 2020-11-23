using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ConnectionBuilder
{
    public class MapNodeInColumn : IEquatable<MapNodeInColumn>, IEqualityComparer<MapNodeInColumn>
    {
        public int NodeId;
        public int Y;

        public bool Equals(MapNodeInColumn other)
            => NodeId.Equals(other.NodeId)
            && Y.Equals(other.Y);
        public override bool Equals(object obj)
            => obj is MapNodeInColumn other
            && Equals(other);
        public override int GetHashCode()
            => HashCode.Combine(NodeId, Y);
        public static bool operator ==(MapNodeInColumn left, MapNodeInColumn right)
            => left.Equals(right);
        public static bool operator !=(MapNodeInColumn left, MapNodeInColumn right)
            => !left.Equals(right);

        public bool Equals([AllowNull] MapNodeInColumn left, [AllowNull] MapNodeInColumn right)
            => left.Equals(right);
        public int GetHashCode([DisallowNull] MapNodeInColumn obj)
            => obj.GetHashCode();
    }

    public class Connection : IEquatable<Connection>, IEqualityComparer<Connection>
    {
        public MapNodeInColumn From;
        public MapNodeInColumn To;

        public bool Equals(Connection other)
            => From.Equals(other.From)
            && To.Equals(other.To);
        public override bool Equals(object obj)
            => obj is Connection other
            && Equals(other);
        public override int GetHashCode()
            => HashCode.Combine(From, To);
        public static bool operator ==(Connection left, Connection right)
            => left.Equals(right);
        public static bool operator !=(Connection left, Connection right)
            => !left.Equals(right);

        public bool Equals([AllowNull] Connection left, [AllowNull] Connection right)
            => left.Equals(right);
        public int GetHashCode([DisallowNull] Connection obj)
            => obj.GetHashCode();

        public override string ToString()
            => $"{From.Y} ==> {To.Y}";
    }

    public class ConnectionSet : IEnumerable<Connection>, IEquatable<ConnectionSet>, IEqualityComparer<ConnectionSet>
    {
        public IEnumerable<Connection> Collection;

        public IEnumerator<Connection> GetEnumerator()
            => Collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public bool Equals(ConnectionSet other)
            => Collection
                .OrderBy(c => c.From.Y)
                .ThenBy(c => c.To.Y)
                .SequenceEqual(other.Collection
                    .OrderBy(c => c.From.Y)
                    .ThenBy(c => c.To.Y));
        public override bool Equals(object obj)
            => obj is ConnectionSet other
            && Equals(other);
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            foreach (var item in Collection) hash.Add(item);
            return hash.ToHashCode();
        }
        public static bool operator ==(ConnectionSet left, ConnectionSet right)
            => left.Equals(right);
        public static bool operator !=(ConnectionSet left, ConnectionSet right)
            => !left.Equals(right);

        public bool Equals([AllowNull] ConnectionSet left, [AllowNull] ConnectionSet right)
            => left.Equals(right);
        public int GetHashCode([DisallowNull] ConnectionSet obj)
            => obj.GetHashCode();
    }


    public interface IConnectionGenerator
    {
        IEnumerable<IEnumerable<Connection>> GeneratePossibilities(
            IEnumerable<MapNodeInColumn> fromNodes, 
            IEnumerable<MapNodeInColumn> toNodes);
    }
}
