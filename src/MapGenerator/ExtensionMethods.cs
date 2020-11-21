using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectionBuilder
{
    public static class ExtensionMethods
    {
        public static Connection MakeConnection(
            this MapNodeInColumn from,
            MapNodeInColumn to)
            => new Connection
            {
                From = from,
                To = to
            };
    }
}
