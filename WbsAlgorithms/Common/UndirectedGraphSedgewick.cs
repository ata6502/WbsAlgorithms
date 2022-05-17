using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// An implementation of an undirected graph using an array of bags.
    /// - Space usage proportional to V + E
    /// - Constant time to add an edge
    /// - Time proportional to the degree of v to iterate through vertices adjacent to v
    /// - Parallel edges and self-loops are allowed
    /// [Sedgewick] p.526 Graph data type
    /// </summary>
    public class UndirectedGraphSedgewick
    {
        // A vertex-indexed array pf lists of integers.
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
        public UndirectedGraphSedgewick(int vertexCount)
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
            // Add an edge twice because the graph is undirected.
            // If an edge connects v and w, the w appears in v's list
            // and v appears in w's list.
            _adjacencyLists[v].Add(w);
            _adjacencyLists[w].Add(v);
            ++E;
        }
    }
}
