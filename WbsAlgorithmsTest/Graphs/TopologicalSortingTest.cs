using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class TopologicalSortingTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void SimpleTests(string filename, Dictionary<int, int> expectedSorting)
        {
            var g = DataReader.ReadGraph(filename);
            var f = TopologicalSorting.Sort(g);

            CollectionAssert.AreEqual(expectedSorting, f);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\GraphDAG1.txt", 
                new Dictionary<int, int> { { 1, 1 }, { 2, 3 }, { 3, 2 }, { 4, 4 } }).SetName("GraphDAG1"); // alternatively: { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }
            yield return new TestCaseData(@"Data\GraphDAG2.txt",
                new Dictionary<int, int> { { 1, 1 }, { 2, 3 }, { 3, 2 }, { 4, 4 }, { 5, 5 }, { 6, 6 } }).SetName("GraphDAG2"); // alternatively: { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }
        }
    }
}
