using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class DegreesOfSeparationTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void GetShortestPathTest(string graphFile, string delimiter, bool includeReversedEdges, string sourceVertex, string destVertex, string[] expectedPath)
        {
            var g = DataReader.ReadSymbolGraph(graphFile, delimiter, includeReversedEdges);
            var d = new DegreesOfSeparation(g);
            var actualPath = d.GetShortestPath(sourceVertex, destVertex);

            CollectionAssert.AreEqual(expectedPath, actualPath);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "A", "F", "A C E F".Split(" ")).SetName("Graph1_A_F");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "C", "I", "C E F I".Split(" ")).SetName("Graph1_C_I");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "G", "D", "G F D".Split(" ")).SetName("Graph1_G_D");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "J", "E", "J I F E".Split(" ")).SetName("Graph1_J_E");

            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "JFK", "LAS", "JFK ORD PHX LAS".Split(" ")).SetName("Graph2_JFK_LAS");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "JFK", "DFW", "JFK ORD DFW".Split(" ")).SetName("Graph2_JFK_DFW");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "HOU", "DEN", "HOU ORD DEN".Split(" ")).SetName("Graph2_HOU_DEN");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "LAX", "ATL", "LAX PHX ORD ATL".Split(" ")).SetName("Graph2_LAX_ATL");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "MCO", "DFW", "MCO HOU DFW".Split(" ")).SetName("Graph2_MCO_DFW");

            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Animal House (1978)", "Titanic (1997)", "Animal House (1978)/Allen, Karen (I)/Raiders of the Lost Ark (1981)/Taylor, Rocky (I)/Titanic (1997)".Split("/")).SetName("Graph3_AnimalHouse_Titanic");
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Animal House (1978)", "To Catch a Thief (1955)", "Animal House (1978)/Vernon, John (I)/Topaz (1969)/Hitchcock, Alfred (I)/To Catch a Thief (1955)".Split("/")).SetName("Graph3_AnimalHouse_ToCatchThief");
        }
    }
}
