﻿using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Graphs
{
    [TestFixture]
    public class DijkstraShortestPathTest
    {
        [TestCaseSource(nameof(TestCases))]
        public void GetShortestPathsTest(string graphFile, int startVertex, int baseVertex, Dictionary<int, int> expectedShortestPaths)
        {
            var graph = DataReader.ReadWeightedGraph(graphFile, baseVertex);
            var shortestPaths = DijkstraShortestPath.GetShortestPaths(graph, startVertex);

            // Display the shortests paths.
            //foreach (var kvp in shortestPaths)
            //    Console.Write($"{{ {kvp.Key}, {kvp.Value} }}, ");

            foreach (var kvp in shortestPaths)
            {
                Assert.That(expectedShortestPaths[kvp.Key], Is.EqualTo(kvp.Value));
            }
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            // From 0 to 0, the shortest path is 0.
            // From 0 to 1, the shortest path is 3.
            // From 0 to 2, the shortest path is 1.
            // From 0 to 3, the shortest path is 4.
            yield return new TestCaseData(@"Data\WeightedGraph1.txt", 0, 0,
                new Dictionary<int,int> { { 0, 0 }, { 1, 3 }, { 2, 1 }, { 3, 4 } }).SetName("Graph1");
            yield return new TestCaseData(@"Data\WeightedGraph2.txt", 0, 0,
                new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 3 }, { 3, 6 } }).SetName("Graph2");
            yield return new TestCaseData(@"Data\WeightedGraph3.txt", 0, 0,
                new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 2 }, { 3, 3 }, { 4, 4 }, { 5, 4 }, { 6, 3 }, { 7, 2 } }).SetName("Graph3");
            // The path from 0 to 9: 0-1-2-5-9
            yield return new TestCaseData(@"Data\WeightedGraph4.txt", 0, 0,
                new Dictionary<int, int> { { 0, 0 }, { 1, 3 }, { 2, 5 }, { 3, 8 }, { 4, 5 }, { 5, 7 }, { 6, 11 }, { 7, 4 }, { 8, 6 }, { 9, 10 }, { 10, 10 } }).SetName("Graph4");
            yield return new TestCaseData(@"Data\WeightedGraphBig.txt", 1, 1,
                new Dictionary<int, int> { 
                    { 1, 0 }, { 2, 2971 }, { 3, 2644 }, { 4, 3056 }, { 5, 2525 }, { 6, 2818 }, { 7, 2599 }, { 8, 1875 }, { 9, 745 }, { 10, 3205 }, 
                    { 11, 1551 }, { 12, 2906 }, { 13, 2394 }, { 14, 1803 }, { 15, 2942 }, { 16, 1837 }, { 17, 3111 }, { 18, 2284 }, { 19, 1044 }, { 20, 2351 }, 
                    { 21, 3630 }, { 22, 4028 }, { 23, 2650 }, { 24, 3653 }, { 25, 2249 }, { 26, 2150 }, { 27, 1222 }, { 28, 2090 }, { 29, 3540 }, { 30, 2303 }, 
                    { 31, 3455 }, { 32, 3004 }, { 33, 2551 }, { 34, 2656 }, { 35, 998 }, { 36, 2236 }, { 37, 2610 }, { 38, 3548 }, { 39, 1851 }, { 40, 4091 }, 
                    { 41, 2732 }, { 42, 2040 }, { 43, 3312 }, { 44, 2142 }, { 45, 3438 }, { 46, 2937 }, { 47, 2979 }, { 48, 2757 }, { 49, 2437 }, { 50, 3152 }, 
                    { 51, 2503 }, { 52, 2817 }, { 53, 2420 }, { 54, 3369 }, { 55, 2862 }, { 56, 2609 }, { 57, 2857 }, { 58, 3668 }, { 59, 2947 }, { 60, 2592 }, 
                    { 61, 1676 }, { 62, 2573 }, { 63, 2498 }, { 64, 2047 }, { 65, 826 }, { 66, 3393 }, { 67, 2535 }, { 68, 4636 }, { 69, 3650 }, { 70, 743 }, 
                    { 71, 1265 }, { 72, 1539 }, { 73, 3007 }, { 74, 4286 }, { 75, 2720 }, { 76, 3220 }, { 77, 2298 }, { 78, 2795 }, { 79, 2806 }, { 80, 982 }, 
                    { 81, 2976 }, { 82, 2052 }, { 83, 3997 }, { 84, 2656 }, { 85, 1193 }, { 86, 2461 }, { 87, 1608 }, { 88, 3046 }, { 89, 3261 }, { 90, 2018 }, 
                    { 91, 2786 }, { 92, 647 }, { 93, 3542 }, { 94, 3415 }, { 95, 2186 }, { 96, 2398 }, { 97, 4248 }, { 98, 3515 }, { 99, 2367 }, { 100, 2970 }, 
                    { 101, 3536 }, { 102, 2478 }, { 103, 1826 }, { 104, 2551 }, { 105, 3368 }, { 106, 2303 }, { 107, 2540 }, { 108, 1169 }, { 109, 3140 }, { 110, 2317 }, 
                    { 111, 2535 }, { 112, 1759 }, { 113, 1899 }, { 114, 508 }, { 115, 2399 }, { 116, 3513 }, { 117, 2597 }, { 118, 2176 }, { 119, 1090 }, { 120, 2328 }, 
                    { 121, 2818 }, { 122, 1306 }, { 123, 2805 }, { 124, 2057 }, { 125, 2618 }, { 126, 1694 }, { 127, 3285 }, { 128, 1203 }, { 129, 676 }, { 130, 1820 }, 
                    { 131, 1445 }, { 132, 2468 }, { 133, 2029 }, { 134, 1257 }, { 135, 1533 }, { 136, 2417 }, { 137, 3599 }, { 138, 2494 }, { 139, 4101 }, { 140, 546 }, 
                    { 141, 1889 }, { 142, 2616 }, { 143, 2141 }, { 144, 2359 }, { 145, 648 }, { 146, 2682 }, { 147, 3464 }, { 148, 2873 }, { 149, 3109 }, { 150, 2183 }, 
                    { 151, 4159 }, { 152, 1832 }, { 153, 2080 }, { 154, 1831 }, { 155, 2001 }, { 156, 3013 }, { 157, 2143 }, { 158, 1376 }, { 159, 1627 }, { 160, 2403 }, 
                    { 161, 4772 }, { 162, 2556 }, { 163, 2124 }, { 164, 1693 }, { 165, 2442 }, { 166, 3814 }, { 167, 2630 }, { 168, 2038 }, { 169, 2776 }, { 170, 1365 }, 
                    { 171, 3929 }, { 172, 1990 }, { 173, 2069 }, { 174, 3558 }, { 175, 1432 }, { 176, 2279 }, { 177, 3829 }, { 178, 2435 }, { 179, 3691 }, { 180, 3027 }, 
                    { 181, 2345 }, { 182, 3807 }, { 183, 2145 }, { 184, 2703 }, { 185, 2884 }, { 186, 3806 }, { 187, 1151 }, { 188, 2505 }, { 189, 2340 }, { 190, 2596 }, 
                    { 191, 4123 }, { 192, 1737 }, { 193, 3136 }, { 194, 1073 }, { 195, 1707 }, { 196, 2417 }, { 197, 3068 }, { 198, 1724 }, { 199, 815 }, { 200, 2060 }
                }).SetName("GraphBig");
        }
    }
}
