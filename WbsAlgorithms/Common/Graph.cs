using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    public class Graph
    {
        // The list of graph's vertices: key - a vertex's index, value - the list of incident vertices
        private Dictionary<int, List<int>> _vertices { get; } = new Dictionary<int, List<int>>();

        // TODO: Do we need to keep the list of graph's edges?
        private List<Edge> _edges { get; } = new List<Edge>();

        /// <summary>
        /// The number of vertices in the graph.
        /// </summary>
        public int VertexCount => _vertices.Count;

        /// <summary>
        /// The graph's vertices.
        /// </summary>
        public IEnumerable<int> Vertices
        {
            get
            {
                foreach (var v in _vertices)
                    yield return v.Key;
            }
        }

        /// <summary>
        /// Returns the list of adjacent vertices to a given vertex.
        /// </summary>
        /// <param name="vertex">The vertex</param>
        /// <returns>The list of adjacent vertices</returns>
        public List<int> this[int vertex]
        {
            get
            {
                if (_vertices.TryGetValue(vertex, out var incidentVertices))
                    return incidentVertices;
                else
                    return new List<int>();
            }
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="u">A vertex representing the tail of the edge</param>
        /// <param name="v">A vertex representing the head of the edge</param>
        public void AddEdge(int u, int v)
        {
            if (_vertices.TryGetValue(u, out var incidentVertices))
                incidentVertices.Add(v);
            else
                _vertices[u] = new List<int> { v };
        }
    }
}
