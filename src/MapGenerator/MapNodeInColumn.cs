using System.Collections.Generic;

namespace ConnectionBuilder
{
    public class MapNodeInColumn
    {
        public int NodeId;
        public int Y;
    }

    public class Connection
    {
        public MapNodeInColumn From;
        public MapNodeInColumn To;
    }

    public interface IConnectionGenerator
    {
        IEnumerable<IEnumerable<Connection>> GeneratePossibilities(
            IEnumerable<MapNodeInColumn> fromNodes, 
            IEnumerable<MapNodeInColumn> toNodes);
    }
}
