using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// Determines if the vertices of a graph can be assigned one of two colors
    /// in such a way that no edge connects vertices of the same color.
    /// [Sedgewick] p.547
    /// </summary>
    public class TwoColorability
    {
        private bool[] _explored;
        private bool[] _color;

        /// <summary>
        /// Indicates if the graph is bipartite.
        /// </summary>
        public bool IsBipartite { get; private set; } = true;

        /// <summary>
        /// Detects if a graph is bipartite.
        /// </summary>
        /// <param name="g">The input graph</param>
        public TwoColorability(Graph g)
        {
            _explored = new bool[g.VertexCount];
            _color = new bool[g.VertexCount];

            for (var s = 0; s < g.VertexCount; ++s)
                if (!_explored[s])
                    Dfs(g, s);
        }

        private void Dfs(Graph g, int v)
        {
            _explored[v] = true;

            foreach (var w in g[v])
            {
                if (!_explored[w])
                {
                    _color[w] = !_color[v];
                    Dfs(g, w);
                }
                else if (_color[w] == _color[v])
                {
                    IsBipartite = false;
                }
            }
        }
    }
}
