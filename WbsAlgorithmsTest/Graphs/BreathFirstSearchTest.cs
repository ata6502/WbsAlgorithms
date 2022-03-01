using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class BreathFirstSearchTest
    {
        [Test]
        public void SingleComponentTest()
        {
            // GraphDAG2 contains one component.
            var g = DataReader.ReadGraph(@"Data\GraphDAG2.txt");

            // The vertex 1 is the source vertex. We can reach the entire graph from this vertex.
            var expected = new List<int> { 1, 2, 3, 4, 5, 6 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 1));

            // The vertex 6 is the sink. We are not able to reach any other vertices.
            expected = new List<int> { 6 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 6));

            // From the vertex 4 we can reach vertices 5 and 6.
            expected = new List<int> { 4, 5, 6 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 4));
        }

        [Test]
        public void MultipleComponentsTest()
        {
            // Graph1 contains three components.
            var g = DataReader.ReadGraph(@"Data\Graph1.txt");

            // From vertex 1 we can reach vertices 4 and 7 (a single component).
            // Vertex 1 (as well as 4 and 7) belongs to the sink component.
            var expected = new List<int> { 1, 4, 7 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 1));

            // From vertex 2 we can reach all vertices (all three components).
            // Vertex 2 (as well as 5 and 8) belongs to the source component.
            expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 2));

            // From vertex 3 we can reach vertices 6, 9, 7, 1, 4 (two components).
            expected = new List<int> { 1, 3, 4, 6, 7, 9 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 3));
        }
    }
}
