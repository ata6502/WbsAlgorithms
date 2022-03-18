using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Graphs
{
    /// <summary>
    /// Kosaraju's Algorithm is a two-pass strategy to find SCCs of the given graph.
    /// - The first DFS pass computes a topological ordering in which to process the vertices.
    /// - The scond DFS pass follows the ordering to discover the SCCs one by one.
    /// Graph search algorithms (DFS or BFS) can uncover SCCs, provided they starts from 
    /// the right vertex. We start from the graph's sink and then work backward.
    /// 
    /// [AlgoIlluminated-2] p.55 SCC (Strongly Connected Component) Definition
    /// An SCC of a directed graph is a maximal subset S ⊆ V of vertices such that there is 
    /// a directed path from any vertex in S to any other vertex in S.
    /// 
    /// [AlgoIlluminated-2] p.54-66 Computing Strongly Connected Components
    /// </summary>
    public class StronglyConnectedComponents
    {
        // Finishing time used in the first pass of DFS. It contains the number of
        // vertices explored so far.
        private static int t;

        /// <summary>
        /// Uses the Kosaraju's Algorithm to find SCCs of the given graph.
        /// </summary>
        /// <param name="graph">A directed graph G = (V,E) in adjacency-list representation with V = {1,2,3,...,n}</param>
        /// <param name="graphReversed">A copy of the input graph G with all edges reversed</param>
        /// <returns>An array containing sets of SCC numbers. Each SCC number corresponds to a single vertex.
        /// Each set contains the same SCC numbers and indicates a single strongly connected component.</returns>
        public static int[] GetComponents(Graph graph, Graph graphReversed)
        {
            Debug.Assert(graph.VertexCount == graphReversed.VertexCount);

            // Reset the global state.
            t = 1;

            // Collections' indices are 1-based.
            var size = graph.VertexCount + 1;

            // A collection of explored vertices.
            var explored = new BitArray(size);

            // Finishing times of the vertices.
            var ft = new int[size];

            // Call DFS from every vertex in the reversed graph in decreasing order.
            // The first DFS pass computes finishing time ft(v) for each vertex.
            foreach (var vertex in graphReversed.ReversedVertices)
            {
                if (!explored[vertex])
                    DFS(graphReversed, vertex, explored, ft);
            }

            // Reset the collection of explored vertices.
            explored.SetAll(false);

            // Create a map: finishing time to a vertex number in the original graph.
            var map = new int[size];
            foreach (var vertex in graph.ReversedVertices)
                map[ft[vertex]] = vertex;

            // Leaders of the strongly connected components.
            var leaders = new int[size];

            // Process vertices in the decreasing order of finishing times.
            // We use the ft-to-vertex map to do that in the second DFS pass.
            foreach (var vertex in graph.ReversedVertices)
            {
                // Each invocation of DFS discovers a new strongly connected component.
                if (!explored[vertex])
                    DFS(graph, vertex, explored, ft, map, leaders);
            }

            // Vertices with the same leader constitute a strongly connected component.
            // The leader indices are the sample as in the original graph.
            return leaders;
        }

        // 1st pass - compute finishing times.
        private static void DFS(Graph g, int sourceVertex, BitArray explored, int[] ft)
        {
            // A stack used to discover vertices in the graph and mark them as explored.
            var s = new Stack<int>();

            s.Push(sourceVertex);

            while (s.Count > 0)
            {
                // Just peek a vertex, not pop. We need to keep it on the stack
                // to assign a finishing time later.
                var v = s.Peek();

                // Check if the vertex v is unexplored.
                if (!explored[v])
                {
                    // Mark the vertex v as explored.
                    explored[v] = true;

                    // Traverse each edge in the v's adjacency list. Explore each 
                    // adjacent vertex if it has not been explored yet.
                    foreach (var w in g[v])
                    {
                        if (!explored[w])
                            s.Push(w);
                    }
                }
                else
                {
                    // Check if all the v's adjacent vertices have been explored.
                    var allExplored = g[v].All(i => explored[i]);
                    if (allExplored)
                    {
                        // If so, remove v from the stack.
                        s.Pop();

                        if (ft[v] == 0)
                        {
                            // Assign the finishing time to the vertex v.
                            ft[v] = t;
                            ++t;
                        }
                    }
                }
            }
        }

        // 2nd pass - find leaders.
        private static void DFS(Graph g, int sourceVertex, BitArray explored, int[] ft, int[] map, int[] leaders)
        {
            var s = new Stack<int>();

            s.Push(sourceVertex);

            // The most recent vertex from which DFS was initiated.
            var leader = sourceVertex;

            while (s.Count > 0)
            {
                var v = s.Pop();

                // Check if the vertex v is unexplored.
                if (!explored[v])
                {
                    // Mark the vertex v as explored.
                    explored[v] = true;

                    // Keep the leader of the vertex v. Vertices with the same
                    // leader constitute a stronlgy connected component.
                    // The leader indices are the sample as in the original graph.
                    leaders[map[v]] = leader;

                    // Traverse each edge in the v's adjacency list.
                    foreach (var w in g[map[v]])
                        s.Push(ft[w]);
                }
            }
        }
    }
}
