using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class DephFirstSearch
    {
        /// <summary>
        /// Perfroms Deph-First Search on the given graph. Uses an iterative approach.
        /// 
        /// [AlgoIlluminated-2] p.42 Depth-First Search - Iterative Implementation
        /// </summary>
        /// <param name="g">A graph in adjacency-list represenation</param>
        /// <param name="sourceVertex">One of the graph's vertices</param>
        /// <returns>A list of explored vertices</returns>
        public static List<int> ExploreIteratively(Graph g, int sourceVertex)
        {
            Debug.Assert(g.VertexCount > 0);

            var s = new Stack<int>();

            s.Push(sourceVertex);

            // A collection of explored vertices. Vertex indices are 1-based.
            var e = new bool[g.VertexCount + 1];

            while (s.Count > 0)
            {
                var v = s.Pop();

                // Check if the vertex v is unexplored.
                if (!e[v])
                {
                    // Mark the vertex v as explored.
                    e[v] = true;

                    // Traverse each edge in the v's adjacency list.
                    foreach (var w in g[v])
                        s.Push(w);
                }
            }

            return GetVertexList(e);
        }

        /// <summary>
        /// Perfroms Deph-First Search on the given graph. Uses a recursive approach.
        ///
        /// [AlgoIlluminated-2] p.43 Depth-First Search - Recursive Implementation
        /// </summary>
        /// <param name="g">A graph in adjacency-list represenation</param>
        /// <param name="sourceVertex">One of the graph's vertices</param>
        /// <returns>A list of explored vertices</returns>
        public static List<int> ExploreRecursively(Graph g, int sourceVertex)
        {
            // A collection of explored vertices. Vertex indices are 1-based.
            var e = new bool[g.VertexCount + 1];

            ExploreRecursivelyInternal(g, sourceVertex, e);

            return GetVertexList(e);

            bool[] ExploreRecursivelyInternal(Graph g, int v, bool[] e)
            {
                e[v] = true;

                foreach (var w in g[v])
                {
                    if (!e[w])
                        ExploreRecursivelyInternal(g, w, e);
                }

                return e;
            }
        }

        // Convert a collection of explored vertices to a list of 1-based vertex indices.
        private static List<int> GetVertexList(bool[] e)
        {
            var exploredVertices = new List<int>();
            for (var i = 1; i < e.Length; ++i)
                if (e[i])
                    exploredVertices.Add(i);
            return exploredVertices;
        }
    }
}
