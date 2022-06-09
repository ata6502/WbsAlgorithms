using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// An implementation of an undirected graph using an array of lists.
    /// - Space usage proportional to V + E
    /// - Constant time to add an edge
    /// - Time proportional to the degree of v to iterate through vertices adjacent to v
    /// - Parallel edges and self-loops are allowed
    /// [Sedgewick] p.526 Graph data type
    /// </summary>
    public class Graph
    {
        // A vertex-indexed array of lists of integers.
        private List<List<int>> _adjacencyLists;

        /// <summary>
        /// The number of vertices V.
        /// </summary>
        public int VertexCount => _adjacencyLists.Count;

        /// <summary>
        /// The number of edges E.
        /// </summary>
        public int EdgeCount { get; private set; }

        public Graph(int vertexCount)
        {
            // Create an array of adjacency lists.
            _adjacencyLists = new List<List<int>>(vertexCount);

            // Initialize all elements in the array to empty lists.
            for (var v = 0; v < vertexCount; ++v)
                _adjacencyLists.Add(new List<int>());
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
        public IEnumerable<int> this[int vertex]
        {
            get
            {
                if (vertex < 0 || vertex > VertexCount - 1)
                    throw new IndexOutOfRangeException(nameof(vertex));

                return _adjacencyLists[vertex];
            }
        }

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="v">A vertex index representing the tail of the edge</param>
        /// <param name="w">A vertex index representing the head of the edge</param>
        public void AddEdge(int v, int w)
        {
            _adjacencyLists[v].Add(w);
            ++EdgeCount;
        }
    }
}
