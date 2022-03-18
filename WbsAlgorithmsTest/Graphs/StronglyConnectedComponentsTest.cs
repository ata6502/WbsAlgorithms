using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class StronglyConnectedComponentsTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void GetComponentsTest(string filename, string expectedTopSccSizesWithLeaders)
        {
            var graph = DataReader.ReadGraph(filename);
            var reversed = DataReader.ReverseGraph(graph);
            var components = StronglyConnectedComponents.GetComponents(graph, reversed);

            // Grab the sizes of the biggest five strongly connected components.
            // Skip the first "dummy" element. The elements in the components collection
            // have 1-based indices.
            var topFive = (from c in components.Skip(1)
                           group c by c into g
                           orderby g.Count() descending
                           select (g.First(), g.Count())).Take(5).ToList();

            // If the graph has fewer than five components, set the remaining sizes to 0.
            for (var i = topFive.Count; i < 5; ++i)
                topFive.Add((0, 0));
            var actualTopSccSizesWithLeaders = string.Join("", topFive).Replace(" ", "");

            // Assert sizes and leaders of the top five strongly connected components. 
            Assert.AreEqual(expectedTopSccSizesWithLeaders, actualTopSccSizesWithLeaders);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            // (leader, the size of SCC)

            yield return new TestCaseData(@"Data\Graph1.txt", "(9,3)(3,3)(6,3)(0,0)(0,0)").SetName("Graph1"); // a 9-vertex 11-edge graph
            yield return new TestCaseData(@"Data\Graph2.txt", "(3,3)(6,3)(8,2)(0,0)(0,0)").SetName("Graph2"); // an 8-vertex 14-edge graph
            yield return new TestCaseData(@"Data\Graph3.txt", "(7,3)(3,3)(8,1)(4,1)(0,0)").SetName("Graph3"); // an 8-vertex 9-edge graph
            yield return new TestCaseData(@"Data\Graph4.txt", "(8,7)(1,1)(0,0)(0,0)(0,0)").SetName("Graph4"); // a 8-vertex 11-edge graph
            yield return new TestCaseData(@"Data\Graph5.txt", "(12,6)(5,3)(7,2)(2,1)(0,0)").SetName("Graph5"); // a 12-vertex 20-edge graph
            yield return new TestCaseData(@"Data\Graph6.txt", "(8,4)(3,3)(11,3)(4,1)(0,0)").SetName("Graph6"); // an 11-vertex 18-edge graph
            yield return new TestCaseData(@"Data\Graph7.txt", "(3,1)(4,1)(1,1)(2,1)(0,0)").SetName("Graph7"); // a 4-vertex 3-edge graph
            yield return new TestCaseData(@"Data\Graph8.txt", "(4,2)(2,1)(1,1)(0,0)(0,0)").SetName("Graph8"); // a 4-vertex 4-edge graph
            yield return new TestCaseData(@"Data\GraphBig.txt", "(615986,434821)(617403,968)(798411,459)(367067,313)(709991,211)").SetName("LargeGraph"); // an 875714-vertex graph
        }
    }
}
