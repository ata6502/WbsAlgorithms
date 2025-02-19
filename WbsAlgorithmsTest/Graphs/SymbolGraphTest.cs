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
        public void AdjacentVerticesTest(string graphFile, string delimiter, bool includeReversedEdges, string key, string[] expectedAdjacentVertices)
        {
            var g = DataReader.ReadSymbolGraph(graphFile, delimiter, includeReversedEdges);
            var actualAdjacentVertices = g.Graph[g.Index(key)].ToList();

            Assert.That(actualAdjacentVertices.Count, Is.EqualTo(expectedAdjacentVertices.Length));
            var cnt = actualAdjacentVertices.Count;

            // Verify the vertices adjacent to the given key.
            for (var i = 0; i < cnt; ++i)
                Assert.That(g.Key(actualAdjacentVertices[i]), Is.EqualTo(expectedAdjacentVertices[i]));
        }

        [TestCaseSource(nameof(ContainsTestCases))]
        public void ContainsTest(string graphFile, string delimiter, bool includeReversedEdges, string key, bool expectedContains)
        {
            var g = DataReader.ReadSymbolGraph(graphFile, delimiter, includeReversedEdges);

            Assert.That(g.Contains(key), Is.EqualTo(expectedContains));
        }

        private static IEnumerable<TestCaseData> AdjacentVerticesTestCases()
        {
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "A", new string[] { "C", "B" }).SetName("Graph1_AdjacentTo_A");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "B", new string[] { "D", "C", "A" }).SetName("Graph1_AdjacentTo_B");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "C", new string[] { "E", "D", "B", "A" }).SetName("Graph1_AdjacentTo_C");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "D", new string[] { "F", "E", "C", "B" }).SetName("Graph1_AdjacentTo_D");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "E", new string[] { "G", "F", "D", "C" }).SetName("Graph1_AdjacentTo_E");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "F", new string[] { "I", "H", "G", "E", "D" }).SetName("Graph1_AdjacentTo_F");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "G", new string[] { "H", "F", "E" }).SetName("Graph1_AdjacentTo_G");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "H", new string[] { "J", "F", "G" }).SetName("Graph1_AdjacentTo_H");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "I", new string[] { "J", "F" }).SetName("Graph1_AdjacentTo_I");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "J", new string[] { "I", "H" }).SetName("Graph1_AdjacentTo_J");

            // The input vertex (e.g., JFK, LAX) is an aiport code. Adjacent vertices are direct flights from that airport.
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "JFK", "ORD ATL MCO".Split(" ")).SetName("Graph2_AdjacentTo_JFK");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "LAX", "LAS PHX".Split(" ")).SetName("Graph2_AdjacentTo_LAX");

            // The SymbolGraph3.txt file consists of lines listing a movie name followed by a list of the performers in the movie.
            // The SymbolGraph3 is bipartite i.e., there are no edges connecting performers to performers or movies to movies.

            // Verifies performers who appear in a given movie.
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Tin Men (1987)", "Hershey, Barbara/Geppi, Cindy/Jones, Kathy (II)/Herr, Marcia/Munchel, Lois Raymond/O'Connell, Deirdre/Weidner, Becky/Kelbaugh, Geri Lynn/Duvall, Susan/Moody, Florence/Barth, Karen/Rappaport, Barbara/Nichols, Penny (I)/Wilson, Shirley Ann/Crofoot, Sharon/Sills, Ellen/Pohlman, Patricia/Ellis, Katherine (II)/Goldpaugh, Kathleen/Morgan, Mary (II)/Ziman, Sharon/Berg, Eva Jean/Posner, Norma/Ford, Lisa/McCauley, Sheila/Stevens, Freddie (I)/Tucker, Michael (I)/Cox, Andy (I)/Portnow, Richard/Levinson, Herb/Sullivan, Brad/Gayle, Jackie/Dreyfuss, Richard/Danoff, Bill/Mahoney, John (I)/Godsey, William C./Kirby, Bruno/Gift, Roland/Jackson, Todd (I)/Tabakin, Ralph/Costantini, Brian/Goldman, Theodore/Brock, Stanley/Craven, Matt (I)/Citronbaum, Myron/Cassel, Seymour/Billings, Joshua/Walsh, J.T./Willis, Michael (I)/DeVito, Danny/Moser, Jeffrey/Steele, David (I)/MacPherson, Walt/Blumenfeld, Alan/DeBoy, David".Split("/")).SetName("Graph3_Performers_AdjacentTo_TinMen");

            // Verifies movies in which a given performer appeared (inverted index).
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Bacon, Kevin", "Woodsman, The (2004)/Wild Things (1998)/Where the Truth Lies (2005)/Tremors (1990)/Trapped (2002)/Stir of Echoes (1999)/Sleepers (1996)/She's Having a Baby (1988)/River Wild, The (1994)/Planes, Trains & Automobiles (1987)/Picture Perfect (1997)/Novocaine (2001)/Mystic River (2003)/My Dog Skip (2000)/Murder in the First (1995)/JFK (1991)/In the Cut (2003)/Hollow Man (2000)/He Said, She Said (1991)/Friday the 13th (1980)/Footloose (1984)/Flatliners (1990)/Few Good Men, A (1992)/Diner (1982)/Beauty Shop (2005)/Apollo 13 (1995)/Animal House (1978)".Split("/")).SetName("Graph3_Movies_AdjacentTo_KevinBacon");
        }

        private static IEnumerable<TestCaseData> ContainsTestCases()
        {
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "A", true).SetName("Graph1_Contains_A");
            yield return new TestCaseData(@"Data\SymbolGraph1.txt", " ", false, "X", false).SetName("Graph1_Contains_X");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", false, "DFW", true).SetName("Graph2_Contains_DFW");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "LAX", true).SetName("Graph2_Contains_LAX");
            yield return new TestCaseData(@"Data\SymbolGraph2.txt", " ", true, "ABC", false).SetName("Graph2_Contains_ABC");
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Jimmy Neutron: Boy Genius (2001)", true).SetName("Graph3_Contains_Movie1");
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Jimmy Neutron: Girl Genius (2001)", false).SetName("Graph3_Contains_Movie2");
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Spade, David", true).SetName("Graph3_Contains_Performer1");
            yield return new TestCaseData(@"Data\SymbolGraph3.txt", "/", true, "Spade, Leszek", false).SetName("Graph3_Contains_Performer2");
        }
    }
}