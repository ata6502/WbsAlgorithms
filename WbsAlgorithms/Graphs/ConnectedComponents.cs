using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// DFS to find connected components in an undirected graph.
    /// [Sedgewick] p.544
    /// </summary>
    public class ConnectedComponents
    {
        private bool[] _explored;

        // The vertex-to-componentId mapping. It associates the same id
        // to every vertex in each component.
        private int[] _id;

        // The number of components.
        private int _count;

        /// <summary>
        /// Uses DFS to find connected components in an undirected graph.
        /// </summary>
        /// <param name="g">An undirected graph</param>
        public ConnectedComponents(Graph g)
        {
            _explored = new bool[g.VertexCount];
            _id = new int[g.VertexCount];

            for (var s = 0; s < g.VertexCount; ++s)
            {
                // Find a source vertex (an unexplored vertex) to perform
                // for DFS in each component.
                if (!_explored[s])
                {
                    Dfs(g, s);

                    // Proceed to the next component.
                    ++_count;
                }
            }
        }

        // Mark all vertices in a single component as explored.
        // Also, keep the vertex-to-componentId mapping for all
        // these vertices.
        private void Dfs(Graph g, int v)
        {
            _explored[v] = true;
            _id[v] = _count;

            foreach (var w in g[v])
            {
                if (!_explored[w])
                    Dfs(g, w);
            }
        }

        /// <summary>
        /// Determines if the input vertices are connected.
        /// </summary>
        /// <param name="v">The first vertex</param>
        /// <param name="w">The second vertex</param>
        /// <returns>True if the vertices are connected; false otherwise</returns>
        public bool AreConnected(int v, int w) => _id[v] == _id[w];

        /// <summary>
        /// Returns a component id a given vertex belongs to.
        /// </summary>
        /// <param name="v">The input vertex</param>
        /// <returns>Component id between 0 and ComponentCount-1</returns>
        public int GetComponentId(int v) => _id[v];

        /// <summary>
        /// Returns the number of connected components.
        /// </summary>
        public int ComponentCount => _count;
    }
}
