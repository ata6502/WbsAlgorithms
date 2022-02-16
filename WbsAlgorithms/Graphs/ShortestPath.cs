using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class ShortestPath
    {
        /// <summary>
        /// Computes shortest-path distances using BFS (Breath-First Search). The "shortest-path distance"
        /// is the fewest number of edges in a path from a vertex v to w or -1 if w is unreachable from v.
        /// 
        /// [AlgoIlluminated-2] p.32 Shortest Paths
        /// </summary>
        /// <param name="g">A graph in adjacency-list represenation</param>
        /// <param name="sourceVertex">A starting vertex</param>
        /// <returns>The list of the shortest paths from the starting vertex to every vertex of the graph. -1 if a vertex is not reachable from the starting vertex</returns>
        public static Dictionary<int, int> FindShortestPaths(Graph g, int sourceVertex)
        {
            Debug.Assert(g.VertexCount > 0);

            // Add the sourceVertex to the queue.
            var q = new Queue<int>();
            q.Enqueue(sourceVertex);

            // An collection of explored vertices.
            var e = new HashSet<int>(g.VertexCount);
            e.Add(sourceVertex);

            // A collection of distances:
            // key - vertex
            // value - a distance from the sourceVertex to the vertex inticated by the key
            var l = new Dictionary<int, int>(g.VertexCount);

            // Initialize the collections of distances.
            // Initially, assume that all the vertices except the starting vertex are unreachable.
            foreach(var v in g.Vertices)
                l[v] = (v == sourceVertex ? 0 : -1);

            // While the queue is not empty.
            while (q.Count > 0)
            {
                var v = q.Dequeue();

                // Traverse each edge in the v's adjacency list.
                foreach (var w in g[v])
                {
                    // Check if the vertex w is unexplored.
                    if (!e.TryGetValue(w, out _))
                    {
                        // Mark the vertex w as explored.
                        e.Add(w);

                        // The vertex w is discovered for the first time. Set the w's shortest
                        // path as one more than that of the vertex v that triggered w's discovery.
                        l[w] = l[v] + 1;

                        // Add the vertex w to the end of the queue.
                        q.Enqueue(w);
                    }
                }
            }

            // Return distances.
            return l;
        }
    }
}
