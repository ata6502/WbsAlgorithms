using System;
using System.Collections.Generic;
using System.Text;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// An implementation of an undirected graph using an array of bags.
    /// [Sedgewick] p.526 Graph data type
    /// </summary>
    public class UndirectedGraph
    {
        // An array of adjacent vertices for each vertex represented as an index in the array.
        private Bag<int>[] _adjacencyLists; 

        /// <summary>
        /// The number of vertices in the graph.
        /// </summary>
        public int V { get; }

        /// <summary>
        /// The number of edges in the graph.
        /// </summary>
        public int E { get; private set; }

        /// <summary>
        /// Returns the list of adjacent vertices to a given vertex.
        /// </summary>
        /// <param name="v">The vertex index</param>
        /// <returns>The list of adjacent vertices</returns>
        public IEnumerable<int> this[int v]
        {
            get
            {
                if (v < 1 || v > V)
                    throw new IndexOutOfRangeException(nameof(v));

                return _adjacencyLists[v];
            }
        }

        /// <summary>
        /// Creates an empty undirected graph with a given number of vertices
        /// and no edges.
        /// </summary>
        /// <param name="vertexCount">The number of vertices in the graph</param>
        public UndirectedGraph(int vertexCount)
        {
            V = vertexCount;
            E = 0;

            // Create an array of adjacency lists.
            _adjacencyLists = new Bag<int>[V];

            // Initialize all elements in the array to empty lists.
            for (var v = 0; v < V; ++v)
                _adjacencyLists[v] = new Bag<int>();
        }

        /// <summary>
        /// Adds an undirected edge to the graph.
        /// </summary>
        /// <param name="u">A vertex in the graph</param>
        /// <param name="v">Another vertex in the graph</param>
        public void AddEdge(int v, int w)
        {
            _adjacencyLists[v].Add(w);
            _adjacencyLists[w].Add(v);
            ++E;
        }
    }
}
