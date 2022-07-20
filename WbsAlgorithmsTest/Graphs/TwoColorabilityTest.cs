using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class TwoColorabilityTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void IsBipartiteTest(string graphFile, bool isBipartite)
        {
            var g = DataReader.ReadGraph(graphFile);
            var alg = new TwoColorability(g);

            Assert.AreEqual(isBipartite, alg.IsBipartite);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\UndirectedGraph6.txt", true).SetName("Graph6");
            yield return new TestCaseData(@"Data\UndirectedGraph7.txt", false).SetName("Graph7");
            yield return new TestCaseData(@"Data\UndirectedGraph8.txt", false).SetName("Graph8");
            yield return new TestCaseData(@"Data\UndirectedGraph9.txt", true).SetName("Graph9");
            yield return new TestCaseData(@"Data\UndirectedGraph10.txt", true).SetName("Graph10");
        }
    }
}
