using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WbsAlgorithms.Common;

namespace WbsAlgorithmsTest.Utilities
{
    internal class DataReader
    {
        public static int[] ReadIntegers(string filename)
        {
            var nums = new List<int>();

            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (int.TryParse(line, out var num))
                        nums.Add(num);
                }
            }

            return nums.ToArray();
        }

        public static int[,] ReadIntegerMatrix(string filename, int rowCount, int columnCount)
        {
            var matrix = new int[rowCount, columnCount];

            string line;
            using (var reader = new StreamReader(filename))
            {
                for(var n = 0; n < rowCount; ++n)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        throw new ArgumentException($"The expected number of rows is {rowCount}.");

                    for (var m = 0; m < columnCount; ++m)
                    {
                        var nums = line.Split(' ');

                        if (nums.Length != columnCount)
                            throw new ArgumentException($"The expected number of columns is {columnCount}.");

                        if (!int.TryParse(nums[m], out var val))
                            throw new ArgumentException($"The value '{nums[m]}' cannot be converted to an integer.");

                        matrix[n, m] = val;

                    }
                }
            }

            return matrix;
        }

        public static Point[] CreatePoints(string filename)
        {
            var str = File.ReadAllText(filename).Split(',');
            var coordinates = new double[str.Length];
            var i = 0;

            foreach (var s in str)
            {
                if (double.TryParse(s, out var n))
                    coordinates[i] = n;
                else
                    throw new FormatException($"The string '{s}' in the position {i + 1} is not correctly formatted number.");
                ++i;
            }

            return CreatePoints(coordinates);
        }

        public static Point[] CreatePoints(params double[] coordinates)
        {
            Debug.Assert(coordinates.Length % 2 == 0);

            var pointCount = coordinates.Length / 2;
            var points = new Point[pointCount];

            for (var i = 0; i < pointCount; ++i)
            {
                var p = new Point(coordinates[2 * i], coordinates[2 * i + 1]);
                points[i] = p;
            }

            return points;
        }

        public static T[] ReadJsonArray<T>(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<T[]>(json);
        }

        /*
            An example of an undirected graph:
            
            Index Adjacent vertices           
            ----- -----------------
            0:    1,2                        [1]  [4]
            1:    0,3                        /\   /| 
            2:    0,3                       /  \ / |
            3:    1,2,4,5                 [0]  [3] |
            4:    3,5                       \  / \ |
            5:    2,3,4                      \/   \|            
                                             [2]--[5]

            Data in a file
            --------------
            U <-- the type of graph: D - directed, U - undirected
            6 <-- the number of vertices V
            7 <-- the number of edges E
            0 1 2 <-- indices are 0-based
            1 0 3
            2 0 3
            3 1 2 4 5
            4 3 5
            5 2 3 4
            etc.
        */
        public static Graph ReadGraph(string filename)
        {


            Graph graph;

            using (var reader = new StreamReader(filename))
            {
                // The first line is the graph type: 'D' - directed graph, 'U' - undirected graph
                var isDirected = ReadGraphType(reader.ReadLine()) == 'D';

                // The second line is the number of vertices V.
                var vertexCount = ReadPositiveInteger(reader.ReadLine());

                // The third line is the number of edges E.
                var edgeCount = ReadPositiveInteger(reader.ReadLine());

                graph = new Graph(vertexCount, isDirected);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    (int v, List<int> AdjacentVertices) = GetVertices(line);

                    foreach (var w in AdjacentVertices)
                        graph.AddEdge(v, w);
                }

                // Verify the number of edges
                if (edgeCount != graph.EdgeCount)
                    throw new ArgumentException($"The number of edges mismatch in {filename}. Expected: {edgeCount}, Actual: {graph.EdgeCount}");
            }

            return graph;

            (int v, List<int> AdjacentVertices) GetVertices(string line)
            {
                char[] VertexSeparator = new char[] { ' ', '\t' };

                string[] nums = line.Trim().Split(VertexSeparator);
                Debug.Assert(nums.Length >= 2);

                // Read a vertex v.
                var v = ReadPositiveInteger(nums[0]);

                // Read vertices adjacent to the vertex v.
                var vertices = new List<int>(nums.Length - 1);
                for (var i = 1; i < nums.Length; ++i)
                    vertices.Add(ReadPositiveInteger(nums[i]));

                return (v, vertices);
            }

            int ReadPositiveInteger(string str)
            {
                if (string.IsNullOrEmpty(str))
                    throw new ArgumentException("The number is missing.");

                if (!int.TryParse(str, out var n))
                    throw new FormatException($"The number '{str}' is not a valid integer.");

                if (n < 0)
                    throw new ArgumentException("The number must be non-negative.");

                return n;
            }

            char ReadGraphType(string str)
            {
                switch(str.ToUpper().Trim())
                {
                    case "D":
                        return 'D';
                    case "U":
                        return 'U';
                    default:
                        throw new ArgumentException($"An unrecognized graph type {str}.");
                }
            }
        }

        public static Graph ReverseDirectedGraph(Graph graph)
        {
            var reversed = new Graph(graph.VertexCount, isDirected: true);

            foreach (var v in graph.Vertices)
            {
                var adjacentVertices = graph[v];
                foreach (var w in adjacentVertices)
                    reversed.AddEdge(w, v);
            }

            return reversed;
        }

        // A delimiter separates vertex names.
        public static SymbolGraph ReadSymbolGraph(string filename, string delimiter, bool includeReversedEdges)
        {
            var map = new Dictionary<string, int>();

            // Each line represents a set of edges, connecting the first vertex name on the line
            // to each of the other vertices on the line.

            // First pass determines the number of vertices.
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Build the key-to-index map.
                    string[] symbols = GetSymbolVertices(line);
                    foreach (var symbol in symbols)
                        if (!map.TryGetValue(symbol, out var _))
                            map.Add(symbol, map.Count);
                }
            }

            // Build the index-to-key map (the inverted index map).
            var indexToKeyMap = new string[map.Count];
            foreach (var key in map.Keys)
                indexToKeyMap[map[key]] = key;

            Graph graph = new Graph(map.Count, isDirected: false);

            // Second pass: knowing the number of vertices we can build the graph
            // by connecting the first vertex on each line to all the others.
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] symbols = GetSymbolVertices(line);
                    int v = map[symbols[0]];
                    for (var i = 1; i < symbols.Length; ++i)
                        graph.AddEdge(v, map[symbols[i]], includeReversedEdges);
                }
            }

            return new SymbolGraph(map, indexToKeyMap, graph);

            string[] GetSymbolVertices(string line)
            {
                string[] symbolVertices = line.Trim().Split(delimiter);
                Debug.Assert(symbolVertices.Length >= 2);

                return symbolVertices;
            }
        }

        /// <summary>
        /// Reads a weighted graph from a file.
        /// </summary>
        /// <param name="filename">The filename containing vertices and weights of the graph in the adjacency list represenation</param>
        /// <param name="baseKey">The key of the first vertex. Commonly, 0 or 1 are used as the first vertex.</param>
        /// <returns>A weighted graph represented as a dictionary where each vertex maps to a list of (neighbor, weight) pairs.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Dictionary<int, List<(int, int)>> ReadWeightedGraph(string filename, int baseKey = 0)
        {
            var graph = new Dictionary<int, List<(int, int)>>();
            string[] lines = File.ReadAllLines(filename);

            char[] VertexSeparator = { ' ', '\t' };
            int vertexKey = baseKey;
            int lineNumber = 0;
            foreach (var line in lines)
            {
                string[] f = line.Trim().Split(VertexSeparator);
                ++lineNumber;

                // Skip empty lines.
                if (f.Length == 0)
                    continue;

                // Verify that the first field represents the correct vertex.
                if (int.TryParse(f[0], out int key) && key == vertexKey)
                {
                    // Add an empty adjacency list for the vertex.
                    graph.Add(vertexKey, new List<(int, int)>());

                    // Add adjacent vertices the weights. If the vertex does not have any outgoing
                    // edges, it means it is a sink.
                    if (f.Length > 1)
                    {
                        for (var i = 1; i < f.Length; ++i)
                        {
                            if (f[i] == string.Empty)
                                continue;

                            int separatorIndex = f[i].IndexOf(",");
                            if (separatorIndex == -1)
                            {
                                throw new ArgumentException($"Invalid vertex/weight pair {f[i]} in line {lineNumber}.");
                            }

                            // Parse the vertex and weight.
                            var vertexStr = f[i].Substring(0, separatorIndex);
                            var weightStr = f[i].Substring(separatorIndex + 1);

                            if (!int.TryParse(vertexStr, out int vertex))
                            {
                                throw new ArgumentException($"Invalid vertex key {vertexStr} in line {lineNumber}.");
                            }

                            if (!int.TryParse(weightStr, out int weight))
                            {
                                throw new ArgumentException($"Invalid weight {weightStr} in line {lineNumber}.");
                            }

                            graph[vertexKey].Add((vertex, weight));
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"Invalid vertex key {f[0]} in line {lineNumber}.");
                }

                ++vertexKey;
            }

            return graph;
        }
    }
}
