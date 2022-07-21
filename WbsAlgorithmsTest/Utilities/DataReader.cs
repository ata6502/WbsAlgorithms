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

        public static Graph ReadGraph(string filename)
        {
            // An example of a graph:
            // D <-- the type of graph: D - directed, U - undirected
            // 13 <-- the number of vertices V
            // 21 <-- the number of edges E
            // 0 5 <-- vertex indices are 0-based
            // 4 3
            // 0 1
            // 9 12
            // etc.

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
        public static SymbolGraph ReadSymbolGraph(string filename, string delimiter, bool includeReversedEdge)
        {
            var map = new Dictionary<string, int>();

            // Each line represents a set of edges, connecting the first vertex name on the line
            // to each of the other vertices on the line.
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] symbols = GetSymbolVertices(line);
                    foreach (var symbol in symbols)
                        if (!map.TryGetValue(symbol, out var _))
                            map.Add(symbol, map.Count);
                }
            }

            var indexToKeyMap = new string[map.Count];
            foreach (var key in map.Keys)
                indexToKeyMap[map[key]] = key;

            Graph graph = new Graph(map.Count, isDirected: false);

            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] symbols = GetSymbolVertices(line);
                    int v = map[symbols[0]];
                    for (var i = 1; i < symbols.Length; ++i)
                        graph.AddEdge(v, map[symbols[i]], includeReversedEdge);
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

        public static WeightedGraph ReadWeightedGraph(string filename)
        {
            string[] lines = File.ReadAllLines(filename);

            var graph = new WeightedGraph(lines.Length);

            foreach (var line in lines)
            {
                (int u, List<(int,int)> AdjacentVertices) = GetVertices(line);
                graph.AddEdge(u, AdjacentVertices);
            }

            return graph;

            (int v, List<(int,int)> AdjacentVertices) GetVertices(string line)
            {
                char[] VertexSeparator = new char[] { ' ', '\t' };

                string[] nums = line.Trim().Split(VertexSeparator);

                var u = GetInt(nums[0]); // vertex#
                var vertices = new List<(int,int)>(nums.Length - 1); // adjacent vertices and their weights
                for (var i = 1; i < nums.Length; ++i)
                {
                    if (nums[i] == string.Empty)
                        continue;

                    var separatorIndex = nums[i].IndexOf(",");

                    var v = GetInt(nums[i].Substring(0, separatorIndex));
                    var weight = GetInt(nums[i].Substring(separatorIndex + 1));

                    vertices.Add((v, weight));
                }

                return (u, vertices);

                int GetInt(string str)
                {
                    if (!int.TryParse(str, out var n))
                        throw new ArgumentException("not a number");
                    return n;
                }
            }
        }
    }
}
