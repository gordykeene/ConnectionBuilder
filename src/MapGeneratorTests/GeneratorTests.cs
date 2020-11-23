#pragma warning disable xUnit1026
using ConnectionBuilder;
using Shouldly;
using System;
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
        public void ValidateTestCase(
            int testCase,
            MapNodeInColumn[] fromNodes,
            MapNodeInColumn[] toNodes,
            List<IEnumerable<Connection>> expected)
        {
            var solutions = new ConnectionGenerator()
                .GeneratePossibilities(fromNodes, toNodes);
#if USE
            solutions.ToFormattedString().ShouldBe(expected.ToFormattedString());
#else
            var s = solutions.ToFormattedString();
            var e = expected.ToFormattedString();
            s.ShouldBe(e);
#endif
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
#if USE
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
#endif
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
        }
    }
}
