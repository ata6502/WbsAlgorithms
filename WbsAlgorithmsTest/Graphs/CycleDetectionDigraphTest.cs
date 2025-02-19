using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class CycleDetectionDigraphTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void HasCycleTest(string graphFile, bool hasCycle, int[] expectedCycle)
        {
            var g = DataReader.ReadGraph(graphFile);
            var alg = new CycleDetectionDigraph(g);

            Assert.That(alg.HasCycle, Is.EqualTo(hasCycle));
            Assert.That(alg.Cycle, Is.EqualTo(expectedCycle).AsCollection);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            // The expectedCycle is one of the cycles in the graph.
            yield return new TestCaseData(@"Data\Graph1.txt", true, new[] { 6, 0, 3, 6 }).SetName("Graph1");
            yield return new TestCaseData(@"Data\Graph2.txt", true, new[] { 4, 3, 4 }).SetName("Graph2");
            yield return new TestCaseData(@"Data\Graph3.txt", true, new[] { 2, 0, 1, 2 }).SetName("Graph3");
            yield return new TestCaseData(@"Data\Graph4.txt", true, new[] { 7, 5, 6, 7 }).SetName("Graph4");
            yield return new TestCaseData(@"Data\Graph5.txt", true, new[] { 11, 9, 10, 11 }).SetName("Graph5");
            yield return new TestCaseData(@"Data\Graph6.txt", true, new[] { 9, 7, 5, 9 }).SetName("Graph6");
            yield return new TestCaseData(@"Data\Graph7.txt", false, null).SetName("Graph7");
            yield return new TestCaseData(@"Data\Graph8.txt", true, new[] { 2, 1, 2 }).SetName("Graph8");
            yield return new TestCaseData(@"Data\GraphDAG1.txt", false, null).SetName("GraphDAG1");
            yield return new TestCaseData(@"Data\GraphDAG2.txt", false, null).SetName("GraphDAG2");
        }
    }
}
