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
                Assert.IsTrue(alg.HasPathTo(v));

            foreach(var path in paths)
            {
                var i = 0;
                foreach (var v in alg.GetPathTo(path.DestinationVertex))
                {
                    Assert.AreEqual(path.Path[i], v);
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
