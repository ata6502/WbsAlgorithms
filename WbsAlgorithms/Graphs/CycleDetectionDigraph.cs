using System.Collections.Generic;
using System.Linq;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// Detects if a directed graph (a digraph) has at least one cycle.
    /// Also, finds a path represencting a cycle (if any) in the graph.
    /// 
    /// [Sedgewick] p.577 - The algorithm from the book does not detect if a graph is acyclic.
    /// The implementation below fixes that problem.
    ///
    /// [Baeldung] describes another algorithm to detect a cycle in a digraph. The website 
    /// includes detailed description and flow charts.
    /// https://www.baeldung.com/cs/detecting-cycles-in-directed-graph
    /// </summary>
    public class CycleDetectionDigraph
    {
        private bool[] _explored;

        // The edgeTo[] is a vertex-index array that gives a way to find a path back
        // to the sourceVertex for *every* vertex connected to the sourceVertex.
        // We store the edge v-w that takes us to each vertex w *for the first time*,
        // by setting edgeTo[w] = v.
        // Note that elements in edgeTo depend on the traversal order of adjacent
        // vertices of each vertex.
        private int[] _edgeTo;

        // Vertices on a cycle if one exists.
        private Stack<int> _cycle;

        // Vertices on the recursive call stack.
        private bool[] _onStack;

        /// <summary>
        /// Returns true if a graph has a cycle; otherwise, false.
        /// </summary>
        public bool HasCycle => _cycle != null;

        /// <summary>
        /// Returns vertices of a cycle if one exists.
        /// </summary>
        public IEnumerable<int> Cycle => _cycle;

        /// <summary>
        /// Detects and finds a cycle in a directed graph.
        /// </summary>
        /// <param name="g">The input graph</param>
        public CycleDetectionDigraph(Graph g)
        {
            _explored = new bool[g.VertexCount];
            _edgeTo = new int[g.VertexCount];
            _onStack = new bool[g.VertexCount];

            for (var v = 0; v < g.VertexCount; ++v)
                if (!_explored[v])
                    Dfs(g, v);
        }

        /// <summary>
        /// Discovers a directed cycle in a digraph.
        /// </summary>
        /// <param name="g">The input graph</param>
        /// <param name="v">The source vertex</param>
        private void Dfs(Graph g, int v)
        {
            _onStack[v] = true;
            _explored[v] = true;

            var adjacentVertices = g[v].ToList();

            // This condition fixes the cases when a cycle is not found.
            // Otherwise, we go into an infinite loop when we try to
            // recover a non-existing cycle.
            if (adjacentVertices.Count == 0)
                _onStack[v] = false;
            else
            {
                foreach (var w in adjacentVertices)
                {
                    if (HasCycle)
                        return;
                    else if (!_explored[w])
                    {
                        _edgeTo[w] = v;
                        Dfs(g, w);
                    }
                    // Check if there is an edge v->w that is on the recursive call stack.
                    // If so, we have discovered a directed cycle.
                    else if (_onStack[w])
                    {
                        // Recover the cycle by following the _edgeTo links.
                        _cycle = new Stack<int>();
                        for (var i = v; i != w; i = _edgeTo[i])
                            _cycle.Push(i);
                        _cycle.Push(w);
                        _cycle.Push(v);
                    }

                    _onStack[v] = false;
                }
            }
        }
    }
}

