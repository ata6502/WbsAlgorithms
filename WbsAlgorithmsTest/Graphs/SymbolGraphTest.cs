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
        public void AdjacentVerticesTest(string graphFile, string key, string delimiter, bool includeReversedEdges, string[] expectedAdjacentVertices)
        {
            var g = DataReader.ReadSymbolGraph(graphFile, delimiter, includeReversedEdges);
            var actualAdjacentVertices = g.Graph[g.Index(key)].ToList();

            Assert.AreEqual(expectedAdjacentVertices.Length, actualAdjacentVertices.Count);
            var cnt = actualAdjacentVertices.Count;

            // Verify the vertices adjacent to the given key.
            for (var i = 0; i < cnt; ++i)
                Assert.AreEqual(expectedAdjacentVertices[i], g.Key(actualAdjacentVertices[i]));
        }

        private static IEnumerable<TestCaseData> AdjacentVerticesTestCases()
        {
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "A", " ", false, new string[] { "C", "B" }).SetName("Graph1_AdjacentTo_A");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "B", " ", false, new string[] { "D", "C", "A" }).SetName("Graph1_AdjacentTo_B");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "C", " ", false, new string[] { "E", "D", "B", "A" }).SetName("Graph1_AdjacentTo_C");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "D", " ", false, new string[] { "F", "E", "C", "B" }).SetName("Graph1_AdjacentTo_D");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "E", " ", false, new string[] { "G", "F", "D", "C" }).SetName("Graph1_AdjacentTo_E");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "F", " ", false, new string[] { "I", "H", "G", "E", "D" }).SetName("Graph1_AdjacentTo_F");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "G", " ", false, new string[] { "H", "F", "E" }).SetName("Graph1_AdjacentTo_G");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "H", " ", false, new string[] { "J", "F", "G" }).SetName("Graph1_AdjacentTo_H");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "I", " ", false, new string[] { "J", "F" }).SetName("Graph1_AdjacentTo_I");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", "J", " ", false, new string[] { "I", "H" }).SetName("Graph1_AdjacentTo_J");

            // The input vertex (e.g., JFK, LAX) is an aiport code. Adjacent vertices are direct flights from that airport.
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", "JFK", " ", true, "ORD ATL MCO".Split(" ")).SetName("Graph2_AdjacentTo_JFK");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", "LAX", " ", true, "LAS PHX".Split(" ")).SetName("Graph2_AdjacentTo_LAX");

            // The SymbolGraph3.txt file consists of lines listing a movie name followed by a list of the performers in the movie.
            // The SymbolGraph3 is bipartite i.e., there are no edges connecting performers to performers or movies to movies.

            // Verifies performers who appear in a given movie.
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "Tin Men (1987)", "/", true, "Hershey, Barbara/Geppi, Cindy/Jones, Kathy (II)/Herr, Marcia/Munchel, Lois Raymond/O'Connell, Deirdre/Weidner, Becky/Kelbaugh, Geri Lynn/Duvall, Susan/Moody, Florence/Barth, Karen/Rappaport, Barbara/Nichols, Penny (I)/Wilson, Shirley Ann/Crofoot, Sharon/Sills, Ellen/Pohlman, Patricia/Ellis, Katherine (II)/Goldpaugh, Kathleen/Morgan, Mary (II)/Ziman, Sharon/Berg, Eva Jean/Posner, Norma/Ford, Lisa/McCauley, Sheila/Stevens, Freddie (I)/Tucker, Michael (I)/Cox, Andy (I)/Portnow, Richard/Levinson, Herb/Sullivan, Brad/Gayle, Jackie/Dreyfuss, Richard/Danoff, Bill/Mahoney, John (I)/Godsey, William C./Kirby, Bruno/Gift, Roland/Jackson, Todd (I)/Tabakin, Ralph/Costantini, Brian/Goldman, Theodore/Brock, Stanley/Craven, Matt (I)/Citronbaum, Myron/Cassel, Seymour/Billings, Joshua/Walsh, J.T./Willis, Michael (I)/DeVito, Danny/Moser, Jeffrey/Steele, David (I)/MacPherson, Walt/Blumenfeld, Alan/DeBoy, David".Split("/")).SetName("Graph3_Performers");
        }
    }
}
