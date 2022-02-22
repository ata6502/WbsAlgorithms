using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// MinCut implements the Karger's Random Contraction algorithm that solves
    /// the minimum cut problem.
    /// </summary>
    public class MinCut
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Finds minimum cuts of a graph using the Karger's Random Contraction algorithm.
        /// 
        /// Reference:
        /// https://www.youtube.com/watch?v=RSokM08eQms "Random Contraction Algorithm"
        /// </summary>
        /// <param name="inputGraph">An undirected graph</param>
        /// <returns>A cut of the input graph as a partition (grouping) of the graph's vertices into
        /// two non-empty sets; the number of crossing edges of the cut. Although the order of elements
        /// in the sets is irrelevant, the algorithm returns the vertices in asceding order.</returns>
        public static (List<int> SetA, List<int> SetB, int CrossingEdgeCount) GetCut(Graph inputGraph)
        {
            // Convert the inputGraph to MinCutGraph. This allows us to contract 
            // vertices in the graph more efficiently.
            var g = new MinCutGraph(inputGraph);

            // Loop as long as the number of remaning vertices in the graph is greater than two.
            while (g.VertexCount > 2)
            {
                // Pick an edge from all remaining edges randomly.
                var e = GetRandomEdge(g);

                // Merge the vertices (u,v) of the random edge e into a single vertex.
                // ContractEdge modifies the input graph g.
                ContractEdge(g, e);
            }

            // Sanity check.
            Debug.Assert(g.VertexCount == 2);

            // Return the cut (not necessarily a min-cut) in the form of two
            // sets of vertices.
            return (g.SetA, g.SetB, g.CrossingEdgeCount);
        }

        private static void ContractEdge(MinCutGraph g, Edge e)
        {
            // Make sure the input edge exists in the graph.
            ValidateEdge(g, e);

            // Remove the input undirectional edge from the graph:
            // - the input edge e
            // - the edge correspoding to e with the head and the tail reversed
            g[e.U].AdjacentVertices.Remove(e.V);
            g[e.V].AdjacentVertices.Remove(e.U);

            // Merge the head vertex with the tail  vertex.
            foreach (var t in g[e.V].AdjacentVertices)
            {
                var index = 0;
                while (index >= 0)
                {
                    index = g[t].AdjacentVertices.FindIndex(index, v => v == e.V);
                    if (index >= 0)
                        g[t].AdjacentVertices[index] = e.U;
                }
            }

            // Keep the contracted vertex and all its previously contracted vertices
            // in the tail vertex. Note that contracted vertices may be duplicated 
            // and not in any particular order. Also, the list of contracted vertices
            // already contains the e.V vertex itself (added during initialization).
            // There is no need to add it again.
            g[e.U].ContractedVertices.AddRange(g[e.V].ContractedVertices);

            // Merge both ends of the input edge into a single vertex. By convention, 
            // make the tail vertex the new vertex.
            g[e.U].AdjacentVertices.AddRange(g[e.V].AdjacentVertices);
            g.RemoveVertex(e.V);

            // Check if the new vertex has any self-loop edges. If so, remove them.
            g[e.U].AdjacentVertices.RemoveAll(v => v == e.U);
        }

        private static void ValidateEdge(MinCutGraph g, Edge e)
        {
            // The input graph is undirected. It means that it contains
            // an edge correspondig to the input edge e with the head and 
            // the tail reversed. Report an error if the corresponding edge 
            // is not found.
            if (!g[e.U].AdjacentVertices.Exists(v => v == e.V))
                throw new ArgumentException($"The edge ({e.U},{e.V}) not found.");
            if (!g[e.V].AdjacentVertices.Exists(u => u == e.U))
                throw new ArgumentException($"The edge ({e.V},{e.U}) not found.");
        }

        private static Edge GetRandomEdge(MinCutGraph g)
        {
            var randomVertex = g.GetRandomVertex();
            var adjacentVertexCount = g[randomVertex].AdjacentVertices.Count;
            var randomAdjacentVertex = g[randomVertex].AdjacentVertices[_rnd.Next(adjacentVertexCount)];

            return new Edge { U = randomVertex, V = randomAdjacentVertex };
        }

        private class MinCutGraph
        {
            private Dictionary<int, Vertices> _graph { get; } = new Dictionary<int, Vertices>();

            public MinCutGraph(Graph g)
            {
                _graph = new Dictionary<int, Vertices>(g.VertexCount);

                foreach(var v in g.Vertices)
                {
                    var adjacentVertices = g[v];
                    var vertices = new Vertices(v);
                    vertices.AdjacentVertices.AddRange(adjacentVertices);
                    vertices.ContractedVertices.Add(v); // add self as a contracted vertex
                    _graph[v] = vertices;
                }
            }

            public int VertexCount => _graph.Count;

            public int GetRandomVertex() => _graph.Keys.ElementAt(_rnd.Next(VertexCount));

            public Vertices this[int vertex]
            {
                get
                {
                    if (!_graph.TryGetValue(vertex, out var _))
                        throw new ArgumentException($"The vertex {vertex} not found.");
                    return _graph[vertex];
                }
            }

            public void RemoveVertex(int vertexKey) => _graph.Remove(vertexKey);

            public List<int> SetA
            {
                get
                {
                    if (_graph.Count != 2)
                        throw new ArgumentException("You can obtain the vertex set A only if two vertices remained in the graph.");

                    // Remove duplicates and sort the contracted vertices.
                    return _graph.First().Value.ContractedVertices.Distinct().OrderBy(v => v).ToList();
                }
            }

            public List<int> SetB
            {
                get
                {
                    if (_graph.Count != 2)
                        throw new ArgumentException("You can obtain the vertex set B only if two vertices remained in the graph.");
                    return _graph.Last().Value.ContractedVertices.Distinct().OrderBy(v => v).ToList();
                }
            }

            public int CrossingEdgeCount
            {
                get
                {
                    if (_graph.Count != 2)
                        throw new ArgumentException("You can obtain the crossing edge count only if two vertices remained in the graph.");

                    var adjacentVertexCountA = _graph.First().Value.AdjacentVertices.Count;
                    var adjacentVertexCountB = _graph.Last().Value.AdjacentVertices.Count;

                    if (adjacentVertexCountA != adjacentVertexCountB)
                        throw new ApplicationException("After contraction of the graph the remaining two vertices have different number of adjacent vertices.");

                    return adjacentVertexCountB;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                // Iterate over each collection of AdjacentVertices and ContractedVertices.
                foreach (var v in _graph)
                {
                    sb.Append($"[{v.Key}]");
                    sb.Append($"({string.Join(',', v.Value.AdjacentVertices)})");
                    sb.Append($"({string.Join(',', v.Value.ContractedVertices)})");
                    sb.Append(" ");
                }

                return sb.ToString();
            }
        }

        private class Vertices
        {
            public int InitialVertex { get; private set; }
            public List<int> AdjacentVertices { get; private set; }
            public List<int> ContractedVertices { get; private set; }

            public Vertices(int initialVertex)
            {
                InitialVertex = initialVertex;
                AdjacentVertices = new List<int>();
                ContractedVertices = new List<int>();
            }
        }
    }
}
