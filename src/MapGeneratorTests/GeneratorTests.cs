using ConnectionBuilder;
using System;
using System.Collections.Generic;
using Xunit;

namespace ConnectionBuilderTests
{
    public class ConnectionGeneratorTests
    {
        [Fact]
        public void Test1()
        {
            var f0 = 0.MakeNode();
            var t0 = 0.MakeNode();
            var fromNodes
                = new[] { f0 };
            var toNodes
                = new[] { t0 };
            var expected = new List<IEnumerable<Connection>> {
                new List<Connection> { f0.MakeConnection(t0) }
            };

            var solutions = new ConnectionGenerator()
                .GeneratePossibilities(fromNodes, toNodes);

            solutions.AreEquivalent(expected);
        }
    }
}
