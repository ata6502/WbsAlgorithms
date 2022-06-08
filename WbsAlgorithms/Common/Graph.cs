using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    public class Graph
    {
        // A list of adjacent vertices. Vertex indices are 0-based.
        private List<List<int>> _vertices { get; }

        /// <summary>
        /// The number of vertices in the graph.
        /// </summary>
        public int VertexCount => _vertices.Count;

        public Graph()
        {
            _vertices = new List<List<int>>();
        }

        public Graph(int capacity)
        {
            _vertices = new List<List<int>>(capacity);
        }

        /// <summary>
        /// The graph's vertices enumerated from index 0 to VertexCount-1.
        /// </summary>
        public IEnumerable<int> Vertices
        {
            get
            {
                for (var i = 0; i <= VertexCount-1; ++i)
                    yield return i;
            }
        }

        /// <summary>
        /// The graph's vertices enumerated from index VertexCount-1 down to 0.
        /// </summary>
        public IEnumerable<int> ReversedVertices
        {
            get
            {
                for (var i = VertexCount-1; i >= 0; --i)
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
                if (vertex < 0 || vertex > VertexCount-1)
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

            if (VertexCount < maxVertexIndex + 1)
            {
                // Resize the list if needed.
                _vertices.Capacity = maxVertexIndex + 1;
                var sizeIncrease = maxVertexIndex + 1 - VertexCount;
                for (var i = 0; i < sizeIncrease; ++i)
                    _vertices.Add(new List<int>());
            }

            _vertices[u].Add(v);
        }
    }
}
