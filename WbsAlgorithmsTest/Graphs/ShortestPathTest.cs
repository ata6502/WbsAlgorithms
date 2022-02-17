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
            yield return new TestCaseData(@"Data\Graph1.txt", 1,
                new Dictionary<int, int> { { 1, 0 }, { 4, 1 }, { 7, 2 }, { 9, -1 }, { 3, -1 }, { 6, -1 }, { 8, -1 }, { 2, -1 }, { 5, -1 } }).SetName("Graph1_start1");
            yield return new TestCaseData(@"Data\Graph1.txt", 3,
                new Dictionary<int, int> { { 1, 4 }, { 4, 5 }, { 7, 3 }, { 9, 2 }, { 3, 0 }, { 6, 1 }, { 8, -1 }, { 2, -1 }, { 5, -1 } }).SetName("Graph1_start3");
            yield return new TestCaseData(@"Data\Graph1.txt", 5,
                new Dictionary<int, int> { { 1, 6 }, { 4, 7 }, { 7, 5 }, { 9, 4 }, { 3, 5 }, { 6, 3 }, { 8, 2 }, { 2, 1 }, { 5, 0 } }).SetName("Graph1_start5");
            yield return new TestCaseData(@"Data\Graph1.txt", 1,
                new Dictionary<int, int> { { 1, 0 }, { 4, 1 }, { 7, 2 }, { 9, -1 }, { 3, -1 }, { 6, -1 }, { 8, -1 }, { 2, -1 }, { 5, -1 } }).SetName("Graph1_start1");
            yield return new TestCaseData(@"Data\Graph2.txt", 6,
                new Dictionary<int, int> { { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, 2 }, { 5, 1 }, { 6, 0 }, { 7, 1 }, { 8, 2 } }).SetName("Graph2_start6");
            yield return new TestCaseData(@"Data\Graph3.txt", 1,
                new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 2 }, { 4, 3 }, { 5, -1 }, { 6, -1 }, { 7, -1 }, { 8, -1 } }).SetName("Graph3_start1");
            yield return new TestCaseData(@"Data\Graph3.txt", 5,
                new Dictionary<int, int> { { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, 1 }, { 5, 0 }, { 6, -1 }, { 7, -1 }, { 8, -1 } }).SetName("Graph3_start5");
            yield return new TestCaseData(@"Data\Graph3.txt", 7,
                new Dictionary<int, int> { { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, 3 }, { 5, -1 }, { 6, 2 }, { 7, 0 }, { 8, 1 } }).SetName("Graph3_start7");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 1, 
                new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 1 }, { 4, 2 }, { 5, 3 }, { 6, 2 } }).SetName("GraphDAG2_start1");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 4,
                new Dictionary<int, int> { { 1, -1 }, { 2, -1 }, { 3, -1 }, { 4, 0 }, { 5, 1 }, { 6, 1 } }).SetName("GraphDAG2_start4");
        }
    }
}
