using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class BreathFirstSearch
    {
        /// <summary>
        /// Perfroms Breath-First Search on the given graph. Uses an iterative approach.
        /// 
        /// [AlgoIlluminated-2] p.27 Breath-First Search
        /// </summary>
        /// <param name="g">A graph in adjacency-list represenation</param>
        /// <param name="sourceVertex">One of the graph's vertices</param>
        /// <returns>A list of explored vertices</returns>
        public static List<int> ExploreIteratively(Graph g, int sourceVertex)
        {
            Debug.Assert(g.VertexCount > 0);

            var q = new Queue<int>();
            q.Enqueue(sourceVertex);

            // An collection of explored vertices.
            var e = new HashSet<int>(g.VertexCount);
            e.Add(sourceVertex);

            while (q.Count > 0)
            {
                var v = q.Dequeue();

                // Traverse each edge in the v's adjacency list.
                foreach (var w in g[v])
                {
                    // Check if the vertex w is unexplored.
                    if (!e.TryGetValue(w, out var explored))
                    {
                        // Mark the vertex w as explored.
                        e.Add(w);

                        // Add the vertex w to the end of the queue.
                        q.Enqueue(w);
                    }
                }
            }

            return ConvertAndSort(e);
        }

        // Convert a HashSet containing explored vertices to a sorted list.
        private static List<int> ConvertAndSort(HashSet<int> e)
        {
            var list = e.ToList();
            list.Sort();
            return list;
        }
    }
}
