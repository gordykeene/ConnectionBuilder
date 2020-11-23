#pragma warning disable xUnit1026
using ConnectionBuilder;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConnectionBuilderTests
{
    public class ConnectionGeneratorTests
    {
        [Fact]
        public void BasicTwoNodes()
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

            solutions.ToFormattedString().ShouldBe(expected.ToFormattedString());
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ValidateTestCases(
            int testCase,
            MapNodeInColumn[] fromNodes,
            MapNodeInColumn[] toNodes,
            List<IEnumerable<Connection>> expected)
        {
            var solutions = new ConnectionGenerator()
                .GeneratePossibilities(fromNodes, toNodes);
            solutions.ToFormattedString().ShouldBe(expected.ToFormattedString());
        }

        public static IEnumerable<object[]> Data()
        {
            var f = Enumerable
                .Range(0, 5)
                .Select(n => n.MakeNode())
                .ToArray();
            var t = Enumerable
                .Range(0, 5)
                .Select(n => n.MakeNode())
                .ToArray();

            // 1FromAnd1To_OnePossibility
            yield return new object[] {
                1001,
                new[] { f[0] },
                new[] { t[0] },
                new List<IEnumerable<Connection>> {
                    new List<Connection> { 
                        f[0].MakeConnection(t[0]) ,
                    },
                }
            };

            // 2FromAnd1To_OnePossibility()
            yield return new object[] {
                1002,
                new[] { f[0], f[1] },
                new[] { t[0] },
                new List<IEnumerable<Connection>> {
                    new List<Connection> {
                        f[0].MakeConnection(t[0]),
                        f[1].MakeConnection(t[0]),
                    },
                }
            };

            yield return new object[] {
                1003,
                new[] { f[0] },
                new[] { t[0], t[1] },
                new List<IEnumerable<Connection>> {
                    new List<Connection> {
                        f[0].MakeConnection(t[0]),
                        f[0].MakeConnection(t[1]),
                    },
                }
            };

            // 2FromAnd2ToPerfectlyLinedUp_ThreePossibilities()
            yield return new object[] {
                1004,
                new[] { f[0], f[1] },
                new[] { t[0], t[1] },
                new List<IEnumerable<Connection>> {
                    new List<Connection> {
                        f[0].MakeConnection(t[0]),
                        f[1].MakeConnection(t[1]),
                    },
                    new List<Connection> {
                        f[0].MakeConnection(t[0]),
                        f[1].MakeConnection(t[0]),
                        f[1].MakeConnection(t[1]),
                    },
                    new List<Connection> {
                        f[0].MakeConnection(t[0]),
                        f[0].MakeConnection(t[1]),
                        f[1].MakeConnection(t[1]),
                    },
                }
            };

            //// 2FromAnd2ToDiamondShape_TwoPossibilities
            //yield return new object[] {
            //    1004,
            //    new[] { f[1], f[3] },
            //    new[] { t[2], f[4] },
            //    new List<IEnumerable<Connection>> {
            //        new List<Connection> {
            //            f[1].MakeConnection(t[3]),
            //            f[2].MakeConnection(t[4]),
            //        },
            //        new List<Connection> {
            //            f[1].MakeConnection(t[3]),
            //            f[2].MakeConnection(t[4]),
            //            f[2].MakeConnection(t[3]),
            //        },
            //    }
            //};


        }
    }
}
