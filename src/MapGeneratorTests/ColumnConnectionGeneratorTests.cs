using ConnectionBuilder;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConnectionBuilderTests
{
    public class ColumnConnectionGeneratorTests
    {
        // private readonly ColumnConnectionGenerator _generator = new ColumnConnectionGenerator();

        [Fact]
        public void A_GeneratePossibilities_NoNodes_NoPossibilities()
            => TestConnectionGenerator(new MapNodeInColumn[0], new MapNodeInColumn[0], new List<Tuple<int, int>[]>());

        [Fact]
        public void B_GeneratePossibilities_NoFromNodes_NoPossibilities()
            => TestConnectionGenerator(new MapNodeInColumn[0], new[] { new MapNodeInColumn { NodeId = "1", Y = 1 } }, new List<Tuple<int, int>[]>());

        [Fact]
        public void C_GeneratePossibilities_NoToNodes_NoPossibilities()
            => TestConnectionGenerator(new[] { new MapNodeInColumn { NodeId = "1", Y = 1 } }, new MapNodeInColumn[0], new List<Tuple<int, int>[]>());

        [Fact]
        public void D_GeneratePossibilities_1FromAnd1To_OnePossibility()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 } },
                new[] { new MapNodeInColumn { NodeId = "2", Y = 1 } },
                new List<Tuple<int, int>[]> { new Tuple<int, int>[] { new Tuple<int, int>(1, 2) } });

        [Fact]
        public void E_GeneratePossibilities_1FromAnd2To_OnePossibility()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 } },
                new[] { new MapNodeInColumn { NodeId = "2", Y = 1 }, new MapNodeInColumn { NodeId = "3", Y = 2 } },
                new List<Tuple<int, int>[]> { new Tuple<int, int>[] { new Tuple<int, int>(1, 2), new Tuple<int, int>(1, 3) } });

        [Fact]
        public void F_GeneratePossibilities_2FromAnd1To_OnePossibility()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 }, new MapNodeInColumn { NodeId = "2", Y = 2 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 1 } },
                new List<Tuple<int, int>[]> { new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 3) } });

        [Fact]
        public void G_GeneratePossibilities_2FromAnd2ToPerfectlyLinedUp_ThreePossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 }, new MapNodeInColumn { NodeId = "2", Y = 2 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 1 }, new MapNodeInColumn { NodeId = "4", Y = 2 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4),  new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 4) },
                });

        [Fact]
        public void H_GeneratePossibilities_2FromAnd2ToDiamondShape_TwoPossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 }, new MapNodeInColumn { NodeId = "2", Y = 3 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 2 }, new MapNodeInColumn { NodeId = "4", Y = 4 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4), new Tuple<int, int>(2, 3),  },
                });

        [Fact]
        public void J_GeneratePossibilities_2FromOuterAnd2ToInner_ThreePossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 }, new MapNodeInColumn { NodeId = "2", Y = 4 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 2 }, new MapNodeInColumn { NodeId = "4", Y = 3 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4),  new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 4) },
                });

        [Fact]
        public void K_GeneratePossibilities_2FromInnerAnd2ToOuter_ThreePossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 2 }, new MapNodeInColumn { NodeId = "2", Y = 3 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 1 }, new MapNodeInColumn { NodeId = "4", Y = 4 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4),  new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 4) },
                });

        [Fact]
        public void M_GeneratePossibilities_2FromOuterAnd3ToInner_FivePossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 1 }, new MapNodeInColumn { NodeId = "2", Y = 5 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 2 }, new MapNodeInColumn { NodeId = "4", Y = 3 }, new MapNodeInColumn { NodeId = "5", Y = 4 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 5), new Tuple<int, int>(1, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 5), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 5), new Tuple<int, int>(1, 4), new Tuple<int, int>(1, 5) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 5), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 4) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(2, 5), new Tuple<int, int>(2, 3), new Tuple<int, int>(2, 4) },
                });

        [Fact]
        public void N_GeneratePossibilities_2FromInnerAnd4ToOuter_ThreePossibilities()
            => TestConnectionGenerator(
                new[] { new MapNodeInColumn { NodeId = "1", Y = 3 }, new MapNodeInColumn { NodeId = "2", Y = 4 } },
                new[] { new MapNodeInColumn { NodeId = "3", Y = 1 }, new MapNodeInColumn { NodeId = "4", Y = 2 }, new MapNodeInColumn { NodeId = "5", Y = 5 }, new MapNodeInColumn { NodeId = "6", Y = 6 } },
                new List<Tuple<int, int>[]>
                {
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 5), new Tuple<int, int>(2, 6) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 5), new Tuple<int, int>(2, 6), new Tuple<int, int>(1, 5) },
                    new Tuple<int, int>[] { new Tuple<int, int>(1, 3), new Tuple<int, int>(1, 4), new Tuple<int, int>(2, 5), new Tuple<int, int>(2, 6), new Tuple<int, int>(2, 4) },
                });

        private void TestConnectionGenerator(
            MapNodeInColumn[] fromNodes, 
            MapNodeInColumn[] toNodes, 
            List<Tuple<int, int>[]> expectedPossibilities)
        {
            var expected = new List<IEnumerable<Connection>>();
            foreach (var ep in expectedPossibilities)
            {
                var connections = new List<Connection>();
                foreach (var c in ep) 
                    connections.Add(new Connection
                    {
                        From = fromNodes.First(n => n.NodeId == c.Item1.ToString()),
                        To = toNodes.First(n => n.NodeId == c.Item2.ToString()),
                    });
                expected.Add(connections);
            }

            var solutions = new ConnectionGenerator()
                .GeneratePossibilities(fromNodes, toNodes);
            solutions.ToFormattedString().ShouldBe(expected.ToFormattedString());
        }
    }
}