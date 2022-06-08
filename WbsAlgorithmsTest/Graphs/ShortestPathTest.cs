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
        public void FindShortestPathsTest(string graphFile, int sourceVertex, int[] expectedDistances)
        {
            var g = DataReader.ReadGraph(graphFile);
            var actualDistances = ShortestPath.FindShortestPaths(g, sourceVertex); // the result distances have 0-based indices
            CollectionAssert.AreEqual(expectedDistances, actualDistances);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\Graph1.txt", 0, new int[] { 0, -1, -1, 1, -1, -1, 2, -1, -1 }).SetName("Graph1_start1");
            yield return new TestCaseData(@"Data\Graph1.txt", 2, new int[] { 4, -1, 0, 5, -1, 1, 3, -1, 2 }).SetName("Graph1_start3");
            yield return new TestCaseData(@"Data\Graph1.txt", 4, new int[] { 6, 1, 5, 7, 0, 3, 5, 2, 4 }).SetName("Graph1_start5");
            yield return new TestCaseData(@"Data\Graph2.txt", 5, new int[] { -1, -1, -1, 2, 1, 0, 1, 2 }).SetName("Graph2_start6");
            yield return new TestCaseData(@"Data\Graph3.txt", 0, new int[] { 0, 1, 2, 3, -1, -1, -1, -1 }).SetName("Graph3_start1");
            yield return new TestCaseData(@"Data\Graph3.txt", 4, new int[] { -1, -1, -1, 1, 0, -1, -1, -1 }).SetName("Graph3_start5");
            yield return new TestCaseData(@"Data\Graph3.txt", 6, new int[] { -1, -1, -1, 3, -1, 2, 0, 1 }).SetName("Graph3_start7");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 0, new int[] { 0, 1, 1, 2, 3, 2 }).SetName("GraphDAG2_start1");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", 3, new int[] { -1, -1, -1, 0, 1, 1 }).SetName("GraphDAG2_start4");
        }
    }
}
