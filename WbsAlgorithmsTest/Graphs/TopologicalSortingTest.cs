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
        public void SimpleTests(string graphFile, int[] expectedSorting)
        {
            var g = DataReader.ReadGraph(graphFile);
            var f = TopologicalSorting.Sort(g);

            Assert.That(f, Is.EqualTo(expectedSorting).AsCollection);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(@"Data\GraphDAG1.txt", new int[] { 0, 1, 2, 3 }).SetName("GraphDAG1"); // alternatively: 0, 2, 1, 3
            yield return new TestCaseData(@"Data\GraphDAG2.txt", new int[] { 0, 1, 2, 3, 4, 5, }).SetName("GraphDAG2"); // alternatively: 0, 2, 1, 3, 4, 5
        }

        // Test all possible orders of exploration of vertices in a 4-vertex DAG (GraphDAG1.txt)
        [Test]
        public void FourVertexOrderTest()
        {
            var vertices = new int[] { 0, 1, 2, 3 };

            foreach (var vertexOrderString in HeapPermutation(vertices))
            {
                var vertexOrder = Array.ConvertAll(vertexOrderString.Split(' '), s => int.Parse(s));
                var f = TopologicalSorting.Sort(_graphFourVertices, vertexOrder); // f has 0-based indices

                Assert.That(f[0], Is.EqualTo(0));

                if (f[1] == 1) // tests 0, 1, 2, 3
                    Assert.That(f[2], Is.EqualTo(2));
                else if (f[1] == 2) // tests 0, 2, 1, 3
                    Assert.That(f[2], Is.EqualTo(1));
                else
                    Assert.Fail($"f[1]={f[1]}; expected 1 or 2");

                Assert.That(f[3], Is.EqualTo(3));
            }
        }

        // Test all possible orders of exploration of vertices in a 6-vertex DAG (GraphDAG2.txt).
        // There are 6! = 720 possibilities.
        [Test]
        public void SixVertexOrderTest()
        {
            var vertices = new int[] { 0, 1, 2, 3, 4, 5 };

            foreach (var vertexOrderString in HeapPermutation(vertices))
            {
                var vertexOrder = Array.ConvertAll(vertexOrderString.Split(' '), s => int.Parse(s));
                var f = TopologicalSorting.Sort(_graphSixVertices, vertexOrder); // f has 0-based indices

                // There are two possible orderings:
                // 0, 1, 2, 3, 4, 5
                // 0, 2, 1, 3, 4, 5

                Assert.That(f[0], Is.EqualTo(0));

                if (f[1] == 1) // tests 0, 1, 2, 3, 4, 5
                    Assert.That(f[2], Is.EqualTo(2));
                else if (f[1] == 2) // tests 0, 2, 1, 3, 4, 5
                    Assert.That(f[2], Is.EqualTo(1));
                else
                    Assert.Fail($"f[1]={f[1]}; expected 1 or 2");

                Assert.That(f[3], Is.EqualTo(3));
                Assert.That(f[4], Is.EqualTo(4));
                Assert.That(f[5], Is.EqualTo(5));
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
