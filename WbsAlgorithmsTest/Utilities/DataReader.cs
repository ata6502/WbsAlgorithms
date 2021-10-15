using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    }
}
