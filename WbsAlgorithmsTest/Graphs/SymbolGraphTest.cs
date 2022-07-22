using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class SymbolGraphTest
    {
        [TestCaseSource(nameof(AdjacentVerticesTestCases))]
        public void AdjacentVerticesTest(string graphFile, string vertex, string[] expectedAdjacentVertices)
        {
            var g = DataReader.ReadSymbolGraph(graphFile, " ", false);
            var actualAdjacentVertices = g.Graph[g.Index(vertex)].ToList();

            Assert.AreEqual(expectedAdjacentVertices.Length, actualAdjacentVertices.Count);
            var cnt = actualAdjacentVertices.Count;

            for (var i = 0; i < cnt; ++i)
                Assert.AreEqual(expectedAdjacentVertices[i], g.Key(actualAdjacentVertices[i]));
        }

        private static IEnumerable<TestCaseData> AdjacentVerticesTestCases()
        {
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "A", new string[] { "C", "B" }).SetName("Graph1_AdjacentTo_A");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "B", new string[] { "D", "C", "A" }).SetName("Graph1_AdjacentTo_B");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "C", new string[] { "E", "D", "B", "A" }).SetName("Graph1_AdjacentTo_C");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "D", new string[] { "F", "E", "C", "B" }).SetName("Graph1_AdjacentTo_D");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "E", new string[] { "G", "F", "D", "C" }).SetName("Graph1_AdjacentTo_E");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "F", new string[] { "I", "H", "G", "E", "D" }).SetName("Graph1_AdjacentTo_F");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "G", new string[] { "H", "F", "E" }).SetName("Graph1_AdjacentTo_G");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "H", new string[] { "J", "F", "G" }).SetName("Graph1_AdjacentTo_H");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "I", new string[] { "J", "F" }).SetName("Graph1_AdjacentTo_I");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "J", new string[] { "I", "H" }).SetName("Graph1_AdjacentTo_J");
        }
    }
}
