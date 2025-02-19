using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class ConnectedComponentsTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void GetConnectedComponentsTest(string graphFile, int expectedComponentCount, int[][] expectedComponents)
        {
            var g = DataReader.ReadGraph(graphFile);
            var alg = new ConnectedComponents(g);
            var cnt = alg.ComponentCount;

            // Verify the number of connected components.
            Assert.That(cnt, Is.EqualTo(expectedComponentCount));

            // Create queues that will contain vertices of each component.
            var components = new Queue<int>[cnt];
            for (var i = 0; i < cnt; ++i)
                components[i] = new Queue<int>();

            // Populate queues with component vertices: each queue contains
            // vertices for a single component.
            for (var v = 0; v < g.VertexCount; ++v)
                components[alg.GetComponentId(v)].Enqueue(v);

            // Verify vertices in each component.
            for (var i = 0; i < cnt; ++i)
            {
                var j = 0;
                foreach (var v in components[i])
                {
                    Assert.That(v, Is.EqualTo(expectedComponents[i][j]));
                    ++j;
                }
            }
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            // Component ids for each vertex: [0,0,0,0,0,0,1,1,2,2,2,2]
            yield return new TestCaseData(
                @"Data\UndirectedGraph8.txt", 3,
                new int[][]
                {
                    new[]{ 0, 1, 2, 3, 4, 5, 6 },
                    new[]{ 7, 8 },
                    new[]{ 9, 10, 11, 12 }
                }).SetName("Graph8");
        }
    }
}
