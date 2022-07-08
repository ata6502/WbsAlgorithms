using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// The BreathFirstPaths algorithm solves the shortest path problem. It uses BFS to find
    /// paths in an undirected graph with the fewest number of edges from a source vertex.
    /// [Sedgewick] p.540
    /// </summary>
    public class BreathFirstPaths
    {
        private int _sourceVertex;
        private bool[] _explored;

        // Contains the result of search - a parent-link representation of a tree
        // rooted at the sourceVertex, which defines the shortest paths from
        // the sourceVertex to every vertex that is connected to s.
        private int[] _edgeTo;

        /// <summary>
        /// Finds the shortest paths in the input graph from the source vertex to each vertex of the graph.
        /// <param name="g">The input graph</param>
        /// <param name="sourceVertex">The source vertex</param>
        /// </summary>
        public BreathFirstPaths(Graph g, int sourceVertex)
        {
            Debug.Assert(sourceVertex < g.VertexCount);

            _sourceVertex = sourceVertex;
            _explored = new bool[g.VertexCount];
            _edgeTo = new int[g.VertexCount];

            ExplorePaths(g, _sourceVertex);
        }

        // Populates edgeTo[] which represents a tree rooted at the sourceVertex.
        private void ExplorePaths(Graph g, int s)
        {
            var q = new Queue<int>();
            q.Enqueue(s);

            // Mark the vertex s as explored.
            _explored[s] = true;

            while (q.Count > 0)
            {
                var v = q.Dequeue();

                // Traverse each vertex in the v's adjacency list.
                foreach (var w in g[v])
                {
                    // Check if the vertex w is unexplored.
                    if (!_explored[w])
                    {
                        // Keep the last edge on a shortest path.
                        _edgeTo[w] = v;

                        // Mark the vertex w as explored i.e., the shortest
                        // path to w is already known.
                        _explored[w] = true;

                        // Add the vertex w to the queue.
                        q.Enqueue(w);
                    }
                }
            }
        }

        /// <summary>
        /// Determines if there is a path from the source vertex to the destination vertex.
        /// </summary>
        /// <param name="v">The destination vertex</param>
        /// <returns>True if there is a path between the source vertex and the destination vertex; otherwise, returns false</returns>
        public bool HasPathTo(int v) => _explored[v];

        /// <summary>
        /// Returns a path from the source vertex s to the destination vertex v with 
        /// the property that no other such path from s to v has fewer edges.
        /// </summary>
        /// <param name="v">The destination vertex</param>
        /// <returns>A path from the source vertex to the destination vertex; null if no such path exists</returns>
        public IEnumerable<int> GetPathTo(int v)
        {
            if (!HasPathTo(v))
                return null;

            // Recover the path from the sourceVertex to the destination vertex v.
            var path = new Stack<int>();
            for (var x = v; x != _sourceVertex; x = _edgeTo[x])
                path.Push(x);
            path.Push(_sourceVertex);

            // Return the stack as IEnumerable.
            return path;
        }
    }
}
