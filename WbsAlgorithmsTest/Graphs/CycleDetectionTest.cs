using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class CycleDetectionTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void HasCycleTest(string graphFile, bool hasCycle)
        {
            var g = DataReader.ReadGraph(graphFile);
            var alg = new CycleDetection(g);

            Assert.That(alg.HasCycle, Is.EqualTo(hasCycle));
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\UndirectedGraph6.txt", true).SetName("Graph6");
            yield return new TestCaseData(@"Data\UndirectedGraph7.txt", true).SetName("Graph7");
            yield return new TestCaseData(@"Data\UndirectedGraph8.txt", true).SetName("Graph8");
            yield return new TestCaseData(@"Data\UndirectedGraph9.txt", false).SetName("Graph9");
            yield return new TestCaseData(@"Data\UndirectedGraph10.txt", false).SetName("Graph10");
        }
    }
}
