using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class DephFirstSearch
    {
        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sourceVertex"></param>
        /// <returns></returns>
        public static List<int> ExploreIteratively(Graph g, int sourceVertex)
        {
            Debug.Assert(g.VertexCount > 0);

            var s = new Stack<int>();

            s.Push(sourceVertex);

            // An collection of explored/unexplored vertices.
            var e = new Dictionary<int, bool>(g.VertexCount);

            while (s.Count > 0)
            {
                var v = s.Pop();

                // Check if the vertex v is unexplored.
                e.TryGetValue(v, out var explored);
                if (!explored)
                {
                    // Mark the vertex v as explored.
                    e[v] = true;

                    // Traverse each edge in the v's adjacency list.
                    foreach (var w in g[v])
                        s.Push(w);
                }
            }

            return ConvertAndSort(e);
        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sourceVertex"></param>
        /// <returns></returns>
        public static List<int> ExploreRecursively(Graph g, int sourceVertex)
        {
            // An collection of explored/unexplored vertices.
            var e = new Dictionary<int, bool>(g.VertexCount);

            ExploreRecursivelyInternal(g, sourceVertex, e);

            return ConvertAndSort(e);
        }

        private static Dictionary<int, bool> ExploreRecursivelyInternal(Graph g, int v, Dictionary<int, bool> e)
        {
            e[v] = true;

            foreach (var w in g[v])
            {
                e.TryGetValue(w, out var explored);
                if (!explored)
                    ExploreRecursivelyInternal(g, w, e);
            }

            return e;
        }

        // Convert the dictionary of explored vertices to a list and sort it.
        private static List<int> ConvertAndSort(Dictionary<int, bool> e)
        {
            var exploredVertices = new List<int>(e.Count);

            foreach (var v in e)
                exploredVertices.Add(v.Key);
            exploredVertices.Sort();

            return exploredVertices;
        }
    }
}
