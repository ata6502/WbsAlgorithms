﻿using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// An implementation of a graph using an array of bags.
    /// - Space usage proportional to V + E
    /// - Constant time to add an edge
    /// - Time proportional to the degree of v to iterate through vertices adjacent to v
    /// </summary>
    public class Graph
    {
        // A vertex-indexed array of bags of integers.
        private Bag<int>[] _adjacencyLists;

        // Indicates if the graph is directed.
        private bool _isDirected;

        // The number of edges in the graph. For undirected graphs, this value is doubled.
        private int _edgeCount;

        /// <summary>
        /// The number of vertices V.
        /// </summary>
        public int VertexCount => _adjacencyLists.Length;

        /// <summary>
        /// The number of edges E.
        /// </summary>
        public int EdgeCount
        {
            get
            {
                // The number of edges depends on the type of graph: directed or undirected.
                // For undirected graphs, _edgeCount has to be divided by two.
                return _isDirected ? _edgeCount : _edgeCount / 2;
            }
        }

        /// <summary>
        /// Creates an empty graph with a given number of vertices and no edges.
        /// </summary>
        /// <param name="vertexCount">The number of vertices in the graph</param>
        public Graph(int vertexCount, bool isDirected)
        {
            _isDirected = isDirected;

            // Create an array of adjacency lists.
            _adjacencyLists = new Bag<int>[vertexCount];

            for (var v = 0; v < vertexCount; ++v)
                _adjacencyLists[v] = new Bag<int>();
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
        /// <param name="includeReversedEdge">Includes a reversed edge, usually for undirected graphs</param>
        public void AddEdge(int v, int w, bool includeReversedEdge = false)
        {
            // For undirected graphs, if an edge connects v and w, the w appears in
            // v's list and v appears in w's list.
            _adjacencyLists[v].Add(w);

            // Some undirected graph data files do not include reversed edges. For such
            // data files we can add v to the w's list .
            if (includeReversedEdge)
                _adjacencyLists[w].Add(v);

            ++_edgeCount;
        }
    }
}
