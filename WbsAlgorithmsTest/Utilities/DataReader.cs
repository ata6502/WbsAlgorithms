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
            var graph = new Graph();

            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    (int v, List<int> AdjacentVertices) = GetVertices(line);

                    foreach (var w in AdjacentVertices)
                        graph.AddEdge(v, w);
                }
            }

            return graph;

            (int v, List<int> AdjacentVertices) GetVertices(string line)
            {
                char[] VertexSeparator = new char[] { ' ', '\t' };

                string[] nums = line.Trim().Split(VertexSeparator);
                Debug.Assert(nums.Length >= 2);

                var v = GetInt(nums[0]);
                var vertices = new List<int>(nums.Length - 1); // adjacent vertices
                for (var i = 1; i < nums.Length; ++i)
                    vertices.Add(GetInt(nums[i]));

                return (v, vertices);

                int GetInt(string str)
                {
                    if (!int.TryParse(str, out var n))
                        throw new ArgumentException("not a number");
                    return n;
                }
            }
        }

        public static Graph ReverseGraph(Graph graph)
        {
            var reversed = new Graph(graph.VertexCount);

            foreach (var v in graph.Vertices)
            {
                var adjacentVertices = graph[v];
                foreach (var w in adjacentVertices)
                    reversed.AddEdge(w, v);
            }

            return reversed;
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

        // Based on https://algs4.cs.princeton.edu/41graph/Graph.java.html
        public static UndirectedGraphSedgewick ReadUndirectedGraphSedgewick(string filename)
        {
            // An example of a graph:
            // 13 <-- the number of vertices V
            // 13 <-- the number of edges E
            // 0 5 <-- vertex indices are 0-based
            // 4 3
            // 0 1
            // 9 12
            // etc.

            using (var reader = new StreamReader(filename))
            {
                // The first line is the number of vertices V.
                var vertexCount = ReadPositiveInteger(reader.ReadLine());

                // The second line is the number of edges.
                var edgeCount = ReadPositiveInteger(reader.ReadLine());

                // Create the undirected graph Sedgewick-style.
                var graph = new UndirectedGraphSedgewick(vertexCount);

                for (var i = 0; i < edgeCount; ++i)
                {
                    var (v, w) = ReadIntegerPair(reader.ReadLine());

                    if (v >= vertexCount || w >= vertexCount)
                        throw new ArgumentOutOfRangeException("Vertex out of range");

                    graph.AddEdge(v, w);
                }

                return graph;
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

            (int, int) ReadIntegerPair(string str)
            {
                char[] VertexSeparator = new char[] { ' ' };

                if (string.IsNullOrEmpty(str))
                    throw new ArgumentException("The numbers are missing.");

                string[] nums = str.Trim().Split(VertexSeparator);
                if (nums.Length != 2)
                    throw new ArgumentException("Two numbers expected.");

                var v = ReadPositiveInteger(nums[0]);
                var w = ReadPositiveInteger(nums[1]);

                return (v, w);
            }
        }
    }
}
