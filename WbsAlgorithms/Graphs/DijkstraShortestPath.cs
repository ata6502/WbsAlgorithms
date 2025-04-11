using System.Collections.Generic;

namespace WbsAlgorithmsTest.Graphs
{
    public class DijkstraShortestPath
    {
        /// <summary>
        /// GetShortestPaths uses the Dijkstra's algorithm to find the shortest path from a source vertex 
        /// to all other vertices in a weighted graph.
        /// </summary>
        /// <param name="graph">A directed graph G = (V,E) in an adjacency-list representation</param>
        /// <param name="start">A starting vertex s in V</param>
        /// <returns>A collection of the shortest paths from the starting vertex to each vertex of the graph</returns>
        public static Dictionary<int, int> GetShortestPaths(Dictionary<int, List<(int, int)>> graph, int start)
        {
            var distances = new Dictionary<int, int>(); // shortest distances
            var priorityQueue = new SortedSet<(int distance, int vertex)>();
            var previousVertices = new Dictionary<int, int?>();

            // Initialize distances with infinity.
            foreach (var vertex in graph.Keys)
            {
                distances[vertex] = int.MaxValue;
                previousVertices[vertex] = null;
            }

            // Distance to the start vertex is 0.
            distances[start] = 0;
            priorityQueue.Add((0, start));

            while (priorityQueue.Count > 0)
            {
                var (currentDistance, currentVertex) = priorityQueue.Min;
                priorityQueue.Remove(priorityQueue.Min);

                if (currentDistance > distances[currentVertex])
                    continue;

                // Process each neighbour.
                foreach (var (neighbor, weight) in graph[currentVertex])
                {
                    int newDistance = distances[currentVertex] + weight;

                    if (newDistance < distances[neighbor])
                    {
                        priorityQueue.Remove((distances[neighbor], neighbor)); // remove the old distance if it exists
                        distances[neighbor] = newDistance;
                        previousVertices[neighbor] = currentVertex;
                        priorityQueue.Add((newDistance, neighbor));
                    }
                }
            }

            return distances;
        }
    }
}
