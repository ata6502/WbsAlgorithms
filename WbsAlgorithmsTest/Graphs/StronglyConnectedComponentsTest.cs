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
            var topFive = (from c in components
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

            yield return new TestCaseData(@"Data\Graph1.txt", "(8,3)(3,3)(5,3)(0,0)(0,0)").SetName("Graph1"); // a 9-vertex 11-edge graph
            yield return new TestCaseData(@"Data\Graph2.txt", "(2,3)(5,3)(7,2)(0,0)(0,0)").SetName("Graph2"); // an 8-vertex 14-edge graph
            yield return new TestCaseData(@"Data\Graph3.txt", "(6,3)(2,3)(7,1)(3,1)(0,0)").SetName("Graph3"); // an 8-vertex 9-edge graph
            yield return new TestCaseData(@"Data\Graph4.txt", "(7,7)(3,1)(0,0)(0,0)(0,0)").SetName("Graph4"); // a 8-vertex 11-edge graph
            yield return new TestCaseData(@"Data\Graph5.txt", "(11,6)(3,3)(5,2)(0,1)(0,0)").SetName("Graph5"); // a 12-vertex 20-edge graph
            yield return new TestCaseData(@"Data\Graph6.txt", "(7,4)(2,3)(10,3)(3,1)(0,0)").SetName("Graph6"); // an 11-vertex 18-edge graph
            yield return new TestCaseData(@"Data\Graph7.txt", "(2,1)(3,1)(0,1)(1,1)(0,0)").SetName("Graph7"); // a 4-vertex 3-edge graph
            yield return new TestCaseData(@"Data\Graph8.txt", "(3,2)(1,1)(0,1)(0,0)(0,0)").SetName("Graph8"); // a 4-vertex 4-edge graph
            yield return new TestCaseData(@"Data\GraphBig.txt", "(615985,434821)(617402,968)(798410,459)(43839,313)(709990,211)").SetName("GraphBig"); // an 875714-vertex graph
        }
    }
}
