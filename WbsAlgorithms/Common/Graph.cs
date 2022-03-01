using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    public class Graph
    {
        // A list of adjacent vertices. Vertex indices are 1-based.
        private List<List<int>> _vertices { get; } = new List<List<int>>();

        /// <summary>
        /// The number of vertices in the graph.
        /// </summary>
        public int VertexCount => _vertices.Count - 1; // we need to subtract 1 because the list of indices is 1-based

        public Graph()
        {
            _vertices.Add(null);
        }

        /// <summary>
        /// The graph's vertices enumerated from index 1 to VertexCount.
        /// </summary>
        public IEnumerable<int> Vertices
        {
            get
            {
                for (var i = 1; i <= VertexCount; ++i)
                    yield return i;
            }
        }

        /// <summary>
        /// Returns the list of adjacent vertices to a given vertex.
        /// </summary>
        /// <param name="vertex">The vertex index</param>
        /// <returns>The list of adjacent vertices</returns>
        public List<int> this[int vertex]
        {
            get
            {
                if (vertex < 1 || vertex > VertexCount)
                    throw new IndexOutOfRangeException(nameof(vertex));

                return _vertices[vertex];
            }
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="u">A vertex index representing the tail of the edge</param>
        /// <param name="v">A vertex index representing the head of the edge</param>
        public void AddEdge(int u, int v)
        {
            var maxVertexIndex = Math.Max(u, v);

            if (VertexCount < maxVertexIndex)
            {
                // Resize the list if needed.
                _vertices.Capacity = maxVertexIndex + 1; // +1 because indices are 1-based
                var sizeIncrease = maxVertexIndex - VertexCount;
                for (var i = 1; i <= sizeIncrease; ++i)
                    _vertices.Add(new List<int>());
            }

            _vertices[u].Add(v);
        }
    }
}
