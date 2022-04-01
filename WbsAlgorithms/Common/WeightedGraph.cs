using System;
using System.Collections.Generic;
using System.Linq;

namespace WbsAlgorithms.Common
{
    public class WeightedGraph
    {
        // A list of adjacent vertices with weights. Vertex indices are 1-based.
        private List<List<(int,int)>> _vertices { get; }

        /// <summary>
        /// The number of vertices in the graph.
        /// </summary>
        public int VertexCount => _vertices.Count - 1; // we need to subtract 1 because the list of indices is 1-based

        public WeightedGraph()
        {
            _vertices = new List<List<(int, int)>>();
            _vertices.Add(null);
        }

        public WeightedGraph(int capacity)
        {
            _vertices = new List<List<(int, int)>>(capacity + 1);
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
        /// Returns the list of adjacent vertices to a given vertex together with their weights.
        /// </summary>
        /// <param name="vertex">The vertex index</param>
        /// <returns>The list of adjacent vertices</returns>
        public List<(int, int)> this[int vertex]
        {
            get
            {
                if (vertex < 1 || vertex > VertexCount)
                    throw new IndexOutOfRangeException(nameof(vertex));

                return _vertices[vertex];
            }
        }

        /// <summary>
        /// Adds a weighted edge to the graph.
        /// </summary>
        /// <param name="u">A vertex index representing the tail of the edge</param>
        /// <param name="adjacentVertices">A list of indices of the adjacent vertices with their weights</param>
        public void AddEdge(int u, List<(int, int)> adjacentVertices)
        {
            var maxVertexIndex = adjacentVertices.Count > 0 ? Math.Max(u, adjacentVertices.Max(v => v.Item1)) : u;

            if (VertexCount < maxVertexIndex)
            {
                // Resize the list if needed.
                _vertices.Capacity = maxVertexIndex + 1; // +1 because indices are 1-based
                var sizeIncrease = maxVertexIndex - VertexCount;
                for (var i = 1; i <= sizeIncrease; ++i)
                    _vertices.Add(new List<(int,int)>());
            }

            foreach(var vw in adjacentVertices)
                _vertices[u].Add(vw);
        }
    }
}
