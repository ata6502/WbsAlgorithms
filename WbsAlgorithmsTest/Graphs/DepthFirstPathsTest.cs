using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class DepthFirstPathsTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void GetPathToTest(string graphFile, int sourceVertex, int[] reachableVertices, PathFromSourceToDestination[] paths)
        {
            var g = DataReader.ReadGraph(graphFile);
            var alg = new DepthFirstPaths(g, sourceVertex);

            foreach(var v in reachableVertices)
                Assert.That(alg.HasPathTo(v), Is.True);

            foreach(var path in paths)
            {
                var i = 0;
                foreach (var v in alg.GetPathTo(path.DestinationVertex))
                {
                    Assert.That(v, Is.EqualTo(path.Path[i]));
                    ++i;
                }
            }
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(
                @"Data\UndirectedGraph7.txt", 0, new[] { 0, 1, 2, 3, 4, 5 },
                new PathFromSourceToDestination[]
                {
                    new PathFromSourceToDestination(0, new[] { 0 }),
                    new PathFromSourceToDestination(1, new[] { 0, 5, 3, 4, 2, 1 }),
                    new PathFromSourceToDestination(2, new[] { 0, 5, 3, 4, 2 }),
                    new PathFromSourceToDestination(3, new[] { 0, 5, 3 }),
                    new PathFromSourceToDestination(4, new[] { 0, 5, 3, 4 }),
                    new PathFromSourceToDestination(5, new[] { 0, 5 })
                }).SetName("Graph7");

            yield return new TestCaseData(
                @"Data\UndirectedGraph8.txt", 0, new[] { 0, 1, 2, 3, 4, 5, 6 },
                new PathFromSourceToDestination[]
                {
                    new PathFromSourceToDestination(0, new[] { 0 }),
                    new PathFromSourceToDestination(1, new[] { 0, 1 }),
                    new PathFromSourceToDestination(2, new[] { 0, 2 }),
                    new PathFromSourceToDestination(3, new[] { 0, 6, 4, 5, 3 }),
                    new PathFromSourceToDestination(4, new[] { 0, 6, 4 }),
                    new PathFromSourceToDestination(5, new[] { 0, 6, 4, 5 }),
                    new PathFromSourceToDestination(6, new[] { 0, 6 })
                }).SetName("Graph8_0");

            yield return new TestCaseData(
                @"Data\UndirectedGraph8.txt", 7, new[] { 7, 8 },
                new PathFromSourceToDestination[]
                {
                    new PathFromSourceToDestination(7, new[] { 7 }),
                    new PathFromSourceToDestination(8, new[] { 7, 8 })
                }).SetName("Graph8_7");

            yield return new TestCaseData(
                @"Data\UndirectedGraph8.txt", 9, new[] { 9, 10, 11, 12 },
                new PathFromSourceToDestination[]
                {
                    new PathFromSourceToDestination(9, new[] { 9 }),
                    new PathFromSourceToDestination(10, new[] { 9, 10 }),
                    new PathFromSourceToDestination(11, new[] { 9, 11 }),
                    new PathFromSourceToDestination(12, new[] { 9, 11, 12 })
                }).SetName("Graph8_9");
        }

        public class PathFromSourceToDestination
        {
            public int DestinationVertex { get; }
            public int[] Path { get; }

            public PathFromSourceToDestination(int destinationVertex, int[] path)
            {
                DestinationVertex = destinationVertex;
                Path = path;
            }
        }
    }
}
