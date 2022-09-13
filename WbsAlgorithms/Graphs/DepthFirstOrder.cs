using System.Collections.Generic;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// [Sedgewick] p.580
    /// </summary>
    public class DepthFirstOrder
    {
        private bool[] _explored;

        private Queue<int> _preorderVertices = new Queue<int>();
        private Queue<int> _postorderVertices = new Queue<int>();
        private Stack<int> _reversePostorderVertices = new Stack<int>();

        public DepthFirstOrder(Graph g)
        {
            _explored = new bool[g.VertexCount];

            for (var v = 0; v < g.VertexCount; ++v)
                if (!_explored[v])
                    Dfs(g, v);
        }

        private void Dfs(Graph g, int v)
        {
            Debug.Assert(v < g.VertexCount);

            _preorderVertices.Enqueue(v);
            _explored[v] = true;

            foreach (var w in g[v])
            {
                if (!_explored[w])
                    Dfs(g, w);
            }

            _postorderVertices.Enqueue(v);
            _reversePostorderVertices.Push(v);
        }
    }
}
