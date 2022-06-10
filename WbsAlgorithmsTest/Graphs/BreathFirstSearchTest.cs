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

            // The vertex 0 is the source vertex. We can reach the entire graph from this vertex.
            var expected = new List<int> { 0, 1, 2, 3, 4, 5 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 0));

            // The vertex 5 is the sink. We are not able to reach any other vertices.
            expected = new List<int> { 5 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 5));

            // From the vertex 3 we can reach vertices 4 and 5.
            expected = new List<int> { 3, 4, 5 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 3));
        }

        [Test]
        public void MultipleComponentsTest()
        {
            // Graph1 contains three components.
            var g = DataReader.ReadGraph(@"Data\Graph1.txt");

            // From vertex 0 we can reach vertices 3 and 6 (a single component).
            // Vertex 0 (as well as 3 and 6) belongs to the sink component.
            var expected = new List<int> { 0, 3, 6 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 0));

            // From vertex 1 we can reach all vertices (all three components).
            // Vertex 1 (as well as 4 and 7) belongs to the source component.
            expected = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 1));

            // From vertex 2 we can reach vertices 5, 8, 6, 0, 3 (two components).
            expected = new List<int> { 0, 2, 3, 5, 6, 8 };
            CollectionAssert.AreEqual(expected, BreathFirstSearch.ExploreIteratively(g, 2));
        }
    }
}
