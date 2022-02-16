using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class ShortestPathTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void FindShortestPathsTest(string graphFile, int sourceVertex, Dictionary<int, int> expectedDistances)
        {
            var g = DataReader.ReadGraph(graphFile);
            var actualDistances = ShortestPath.FindShortestPaths(g, sourceVertex);
            CollectionAssert.AreEqual(expectedDistances, actualDistances);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 1, 
                new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 1 }, { 4, 2 }, { 5, 3 }, { 6, 2 } }).SetName("GraphDAG2_start1");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 4,
                new Dictionary<int, int> { { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, 0 }, { 5, 1 }, { 6, 1 } }).SetName("GraphDAG2_start4");
        }
    }
}
