using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// The DepthFirstPaths algorithm solves the single-source paths problem.
    /// The algorithm works on undirected graphs and it uses DFS.
    /// [Sedgewick] p.536
    /// </summary>
    public class DepthFirstPaths
    {
        private Graph _graph;
        private int _sourceVertex;
        private bool[] _explored;

        // The edgeTo[] is a vertex-index array that gives a way to find a path back
        // to the sourceVertex for *every* vertex connected to the sourceVertex.
        // We store the edge v-w that takes us to each vertex w *for the first time*,
        // by setting edgeTo[w] = v.
        // Note that elements in edgeTo depend on the traversal order of adjacent
        // vertices of each vertex.
        private int[] _edgeTo;

        /// <summary>
        /// Finds paths in the input graph from the source vertex to each vertex of the graph.
        /// <param name="g">The input graph</param>
        /// <param name="sourceVertex">The source vertex</param>
        /// </summary>
        public DepthFirstPaths(Graph g, int sourceVertex)
        {
            Debug.Assert(sourceVertex < g.VertexCount);

            _graph = g;
            _sourceVertex = sourceVertex;
            _explored = new bool[g.VertexCount];
            _edgeTo = new int[g.VertexCount];

            ExplorePaths(_sourceVertex);

            // Populates edgeTo[] which is a tree rooted at the sourceVertex.
            void ExplorePaths(int v)
            {
                // Mark the vertex v as explored.
                _explored[v] = true;

                // Explored recursively all the vertices that are adjacent to 
                // the vertex v and that have not yet been visited.
                foreach (var w in _graph[v])
                {
                    if (!_explored[w])
                    {
                        _edgeTo[w] = v;
                        ExplorePaths(w);
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
        /// Returns a path from the source vertex to the destination vertex.
        /// </summary>
        /// <param name="v">The destination vertex</param>
        /// <returns>A path from the source vertex to the destination vertex; null if no such path exists</returns>
        public IEnumerable<int> GetPathTo(int v)
        {
            if (!HasPathTo(v))
                return null;

            // Recover the path from the sourceVertex to the destination vertex v.
            // We use the variable x to travel up the tree (represented by edgeTo)
            // putting each vertex encountered onto a stack until reaching the sourceVertex.
            var path = new Stack<int>();
            for (var x = v; x != _sourceVertex; x = _edgeTo[x])
                path.Push(x);
            path.Push(_sourceVertex);

            // Return the stack as IEnumerable.
            return path;
        }
    }
}
