using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// Detects if a graph has at least one cycle.
    /// [Sedgewick] p.547
    /// </summary>
    public class CycleDetection
    {
        private bool[] _explored;

        /// <summary>
        /// Returns true if a graph has a cycle; otherwise, false.
        /// </summary>
        public bool HasCycle { get; private set; }

        /// <summary>
        /// Detects if a graph has at least one cycle.
        /// </summary>
        /// <param name="g">The input graph</param>
        public CycleDetection(Graph g)
        {
            _explored = new bool[g.VertexCount];

            for (var s = 0; s < g.VertexCount; ++s)
                if (!_explored[s])
                    Dfs(g, s, s);
        }

        private void Dfs(Graph g, int v, int u)
        {
            _explored[v] = true;

            foreach (var w in g[v])
            {
                if (!_explored[w])
                    Dfs(g, w, v);
                else if (w != u)
                    HasCycle = true;
            }
        }
    }
}
