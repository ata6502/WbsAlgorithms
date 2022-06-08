using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WbsAlgorithmsTest.Utilities
{
    internal class DataConverter
    {
        public static T[,] ConvertStringToMatrix<T>(string matrix)
        {
            Debug.Assert(!string.IsNullOrEmpty(matrix));

            var rows = matrix.Replace("\r\n", "").Split(';');
            var cols = rows.First().Split(',');

            // Determine dimensions.
            var rowCount = rows.Length;
            var colCount = cols.Length;

            var a = new T[rowCount, colCount];
            for (var i = 0; i < rowCount; ++i)
            {
                var strNums = rows[i].Split(',');

                // Check if all rows have the same number of columns.
                Debug.Assert(strNums.Length == colCount);

                var j = 0;
                foreach (var str in strNums)
                {
                    var result = Convert<T>(str);
                    Debug.Assert(result.IsSucceeded);

                    a[i, j] = result.Value;
                    ++j;
                }
            }

            return a;
        }

        public static string ConvertMatrixToString<T>(T[,] matrix, int numberOfDecimalPlaces = 0)
        {
            var maxRowIndex = matrix.GetUpperBound(0);
            var maxColIndex = matrix.GetUpperBound(1);
            var sb = new StringBuilder();

            for (var i = 0; i <= maxRowIndex; ++i)
            {
                for (var j = 0; j <= maxColIndex; ++j)
                {
                    sb.Append(ApplyTolerance(matrix[i, j], numberOfDecimalPlaces));
                    if (j < maxColIndex)
                        sb.Append(",");
                }

                if (i < maxRowIndex)
                    sb.Append(";");
            }

            return sb.ToString();
        }

        public static int[] CovertStringToIntArray(string str)
        {
            return str.Split(',').Select(int.Parse).ToArray();
        }

        private static (T Value, bool IsSucceeded) Convert<T>(string str)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                    return ((T)converter.ConvertFromString(str), true);
                return (default(T), false);
            }
            catch (NotSupportedException)
            {
                return (default(T), false);
            }
        }

        private static object ApplyTolerance(object num, int numberOfDecimalPlaces)
            => num is double ? Math.Round((double)num, numberOfDecimalPlaces) : num;

        public static void ConvertOneBasedGraphToZeroBasedGraph(string filename)
        {
            var sb = new StringBuilder();
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.AppendLine(ConvertToZeroBasedVertices(line));
                }
            }
            File.WriteAllText(filename, sb.ToString());

            string ConvertToZeroBasedVertices(string line)
            {
                char[] VertexSeparator = new char[] { ' ', '\t' };
                string[] nums = line.Trim().Split(VertexSeparator);

                var vert = new StringBuilder();
                for (var i = 0; i < nums.Length; ++i)
                {
                    vert.Append(GetInt(nums[i]) - 1);
                    if (i < nums.Length - 1)
                        vert.Append(" ");
                }
                return vert.ToString();

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
