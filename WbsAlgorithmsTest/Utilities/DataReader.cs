using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public static int[,] ConvertStringToMatrix(string matrix)
        {
            Debug.Assert(!string.IsNullOrEmpty(matrix));

            var rows = matrix.Replace("\r\n", "").Split(';');
            var cols = rows.First().Split(',');

            // Determine dimensions.
            var rowCount = rows.Length;
            var colCount = cols.Length;

            var a = new int[rowCount, colCount];
            for(var i = 0; i < rowCount; ++i)
            {
                var strNums = rows[i].Split(',');

                // Check if all rows have the same number of columns.
                Debug.Assert(strNums.Length == colCount);

                var j = 0;
                foreach(var str in strNums)
                {
                    var isValidInt = int.TryParse(str, out var num);
                    Debug.Assert(isValidInt);

                    a[i, j] = num;
                    ++j;
                }
            }

            return a;
        }
    }
}
