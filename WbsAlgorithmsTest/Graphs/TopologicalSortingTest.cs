using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.Common;
using WbsAlgorithms.Graphs;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class TopologicalSortingTest
    {
        private Graph _graphFourVertices;
        private Graph _graphSixVertices;

        [OneTimeSetUp]
        public void ReadGraphs()
        {
            _graphFourVertices = DataReader.ReadGraph(@"Data\GraphDAG1.txt");
            _graphSixVertices = DataReader.ReadGraph(@"Data\GraphDAG2.txt");
        }

        [TestCaseSource(nameof(TestCases))]
        public void SimpleTests(string filename, Dictionary<int, int> expectedSorting)
        {
            var g = DataReader.ReadGraph(filename);
            var f = TopologicalSorting.Sort(g);

            CollectionAssert.AreEqual(expectedSorting, f);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\GraphDAG1.txt", 
                new Dictionary<int, int> { { 1, 1 }, { 2, 3 }, { 3, 2 }, { 4, 4 } }).SetName("GraphDAG1"); // alternatively: { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }
            yield return new TestCaseData(@"Data\GraphDAG2.txt",
                new Dictionary<int, int> { { 1, 1 }, { 2, 3 }, { 3, 2 }, { 4, 4 }, { 5, 5 }, { 6, 6 } }).SetName("GraphDAG2"); // alternatively: { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 5 }, { 6, 6 }
        }

        // Test all possible orders of exploration of vertices in a 4-vertex DAG (GraphDAG1.txt).
        [Test]
        public void FourVertexOrderTest()
        {
            var vertices = new int[] { 1, 2, 3, 4 };

            foreach (var vertexOrderString in HeapPermutation(vertices))
            {
                var vertexOrder = Array.ConvertAll(vertexOrderString.Split(' '), s => int.Parse(s));
                var f = TopologicalSorting.Sort(_graphFourVertices, vertexOrder);

                Assert.AreEqual(1, f[1]);

                if (f[2] == 2) // tests { 1,1 }, { 2,2 }, { 3,3 }, { 4,4 }
                    Assert.AreEqual(3, f[3]);
                else if (f[2] == 3) // tests { 1,1 }, { 2,3 }, { 3,2 }, { 4,4 }
                    Assert.AreEqual(2, f[3]);
                else
                    Assert.Fail($"f[2]={f[2]}; expected 2 or 3");

                Assert.AreEqual(4, f[4]);
            }
        }

        /// <summary>
        /// HeapPermutation generates all permutations of the input array using the Heap's Algorithm.
        /// Source: https://en.wikipedia.org/wiki/Heap%27s_algorithm (non-recursive implementation)
        /// Also: https://www.geeksforgeeks.org/heaps-algorithm-for-generating-permutations/
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private IEnumerable<string> HeapPermutation(int[] a)
        {
            var n = a.Length;

            // c is an encoding of the stack state. c[k] encodes the for-loop counter for when HeapPermutation(k - 1, A) is called.
            var c = new int[n];

            var permutation = string.Join(' ', a);
            yield return permutation;

            // i acts similarly to the stack pointer.
            var i = 0;
            while (i < n)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        var tmp = a[0];
                        a[0] = a[i];
                        a[i] = tmp;
                    }
                    else
                    {
                        var tmp = a[c[i]];
                        a[c[i]] = a[i];
                        a[i] = tmp;
                    }

                    permutation = string.Join(' ', a);
                    yield return permutation;

                    // Swap has occurred ending the for-loop. Simulate the increment of the for-loop counter.
                    c[i] += 1;

                    // Simulate recursive call reaching the base case by bringing the pointer to the base case analog in the array.
                    i = 0;
                }
                else
                {
                    // Calling HeapPermutation(i+1, A) has ended as the for-loop terminated. 
                    // Reset the state and simulate popping the stack by incrementing the pointer.
                    c[i] = 0;
                    i++;
                }
            }
        }
    }
}
