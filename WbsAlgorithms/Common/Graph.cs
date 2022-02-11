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
        /// TODO: Add comment
        /// </summary>
        public int VertexCount => _vertices.Count;

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
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
        /// TODO: Add comment
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public void AddEdge(int u, int v)
        {
            if (_vertices.TryGetValue(u, out var incidentVertices))
                incidentVertices.Add(v);
            else
                _vertices[u] = new List<int> { v };
        }
    }
}
