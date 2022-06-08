using System.Collections.Generic;
using System.Diagnostics;
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

            // A collection of explored vertices.
            var e = new bool[g.VertexCount];

            // Mark the source vertex as explored.
            e[sourceVertex] = true;

            while (q.Count > 0)
            {
                var v = q.Dequeue();

                // Traverse each edge in the v's adjacency list.
                foreach (var w in g[v])
                {
                    // Check if the vertex w is unexplored.
                    if (!e[w])
                    {
                        // Mark the vertex w as explored.
                        e[w] = true;

                        // Add the vertex w to the end of the queue.
                        q.Enqueue(w);
                    }
                }
            }

            return GetVertexList(e);
        }

        // Convert a collection of explored vertices to a list vertex indices.
        private static List<int> GetVertexList(bool[] e)
        {
            var exploredVertices = new List<int>();
            for (var i = 0; i < e.Length; ++i)
                if (e[i])
                    exploredVertices.Add(i);
            return exploredVertices;
        }
    }
}
