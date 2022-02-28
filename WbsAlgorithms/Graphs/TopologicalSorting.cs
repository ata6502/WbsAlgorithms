using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    public class TopologicalSorting
    {
        // Keeps track of ordering.
        private static int _ordinalPosition;

        /// <summary>
        /// Finds one of topological orderings of a graph.
        /// </summary>
        /// <param name="g">A directed acycling graph in adjacency list represenation</param>
        /// <returns>A collection of vertices that constitutes a topological ordering of the input graph. 
        /// The dictionary's key is a vertex label and the dictionary's value is an ordering index</returns>
        public static Dictionary<int, int> Sort(Graph g)
        {
            // Initialize the current ordering position to be the index of the last vertex.
            _ordinalPosition = g.VertexCount;

            // A collection of explored vertices.
            var e = new HashSet<int>(g.VertexCount);

            // A collection of vertices that constitutes a topological ordering.
            var f = new Dictionary<int, int>(g.VertexCount);

            // Iterate over all vertex labels.
            foreach (var v in g.Vertices)
            {
                // Check if the vertex v is unexplored.
                if (!e.TryGetValue(v, out _))
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
        private static void DFSTopo(Graph g, int s, HashSet<int> e, Dictionary<int, int> f)
        {
            // Mark the vertex s as explored.
            e.Add(s);

            // For each edge (s,v) in the s's outgoing adjacency list.
            foreach (var v in g[s])
                if (!e.TryGetValue(v, out _)) // if v is unexplored
                    DFSTopo(g, v, e, f);

            // This is additional code in DFS to determine topological ordering.
            f.Add(s, _ordinalPosition); // keep the s's position in ordering
            --_ordinalPosition;         // work right-to-left in recursive DFS
        }
    }
}
