using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class TopologicalSorting
    {
        // Keeps track of where we are in topological ordering.
        private static int _ordinalPosition;

        /// <summary>
        /// Finds one of topological orderings of a graph. Computes an ordering in reverse order
        /// (from right to left).
        /// </summary>
        /// <param name="g">A directed acycling graph in adjacency list represenation</param>
        /// <param name="vertexOrder">An optional array that determines the order of exploration of the vertices.
        /// All orders of exploration should return the same topological ordering. If not specified, the vertices
        /// are explored from left to right.</param>
        /// <returns>A collection of vertices that constitutes a topological ordering of the input graph.</returns>
        public static int[] Sort(Graph g, int[] vertexOrder = null)
        {
            // Initialize the current ordering position to be the index of the last vertex.
            _ordinalPosition = g.VertexCount - 1;

            // A collection of explored vertices.
            var e = new bool[g.VertexCount];

            // A collection of vertices that constitutes a topological ordering.
            var f = new int[g.VertexCount];

            // Iterate over all vertex labels.
            if (vertexOrder == null)
            {
                foreach (var v in g.Vertices)
                {
                    // Check if the vertex v is unexplored.
                    if (!e[v])
                        DFSTopo(g, v, e, f);
                }
            }
            else
            {
                foreach (var v in vertexOrder)
                    if (!e[v])
                        DFSTopo(g, v, e, f);
            }

            return f;
        }

        /// <summary>
        /// A recursive helper that implemenets the DFS algorithm augmented for topological ordering.
        /// Postcondition: Every vertex reachable from s is marked as "explored" and has an assigned f-value.
        /// </summary>
        /// <param name="g">A directed acycling graph G = (V,E) in adjacency list represenation</param>
        /// <param name="s">A vertex that belogs to V</param>
        /// <param name="e">A collection of explored vertices</param>
        /// <param name="f">A collection of vertices that constitutes a topological ordering</param>
        private static void DFSTopo(Graph g, int s, bool[] e, int[] f)
        {
            // Mark the vertex s as explored.
            e[s] = true;

            // For each edge (s,v) in the s's outgoing adjacency list.
            foreach (var v in g[s])
                if (!e[v]) // if v is unexplored
                    DFSTopo(g, v, e, f);

            // This is additional code in DFS to determine topological ordering.
            f[s] = _ordinalPosition; // keep the s's position in ordering
            --_ordinalPosition;      // work right-to-left in recursive DFS
        }
    }
}
