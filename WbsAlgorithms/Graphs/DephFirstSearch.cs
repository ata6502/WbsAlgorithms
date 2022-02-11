using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            // An collection of explored vertices.
            var e = new HashSet<int>(g.VertexCount);

            while (s.Count > 0)
            {
                var v = s.Pop();

                // Check if the vertex v is unexplored.
                if (!e.TryGetValue(v, out _))
                {
                    // Mark the vertex v as explored.
                    e.Add(v);

                    // Traverse each edge in the v's adjacency list.
                    foreach (var w in g[v])
                        s.Push(w);
                }
            }

            return ConvertAndSort(e);
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
            // An collection of explored vertices.
            var e = new HashSet<int>(g.VertexCount);

            ExploreRecursivelyInternal(g, sourceVertex, e);

            return ConvertAndSort(e);

            HashSet<int> ExploreRecursivelyInternal(Graph g, int v, HashSet<int> e)
            {
                e.Add(v);

                foreach (var w in g[v])
                {
                    if (!e.TryGetValue(w, out _))
                        ExploreRecursivelyInternal(g, w, e);
                }

                return e;
            }
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
