using System;
using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairPointMinMax
{
    public class ClosestPair1D
    {
        /// <summary>
        /// Finds the closest pair using brute force. Complexity O(n^2)
        /// </summary>
        /// <param name="a">An array of double values. The array should contain at least two elements.</param>
        /// <returns>The closest pair as a 2D point</returns>
        public static Point FindPairBruteForce(double[] a)
        {
            Debug.Assert(a.Length >= 2);

            var minDistance = double.MaxValue;
            var minX = int.MaxValue;
            var minY = int.MaxValue;

            for (var x = 0; x < a.Length - 1; ++x)
            {
                for (var y = x + 1; y < a.Length; ++y)
                {
                    var distance = Math.Abs(a[y] - a[x]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minX = x;
                        minY = y;
                    }
                }
            }

            // Swap indices if the first value is greater than the second one.
            if (a[minX] > a[minY])
            {
                var tmp = minX;
                minX = minY;
                minY = tmp;
            }

            return new Point(a[minX], a[minY]);
        }

        /// <summary>
        /// Finds the closest pair in a sorted array. Complexity O(n*log(n))
        /// 
        /// [Sedgewick] 1.4.16 p.210 - Find a closest pair in an array of double values.
        /// The closest pair includes two values whose difference is no greater than 
        /// the difference of any other pair. The running time of the program should be 
        /// linearithmic in the worst case.
        /// </summary>
        /// <param name="a">An array of double values. The array should contain at least two elements.</param>
        /// <returns>The closest pair as a 2D point. The input array is sorted.</returns>
        public static Point FindPairLinearithmic(double[] a)
        {
            Debug.Assert(a.Length >= 2);

            // Sort the input array.
            Array.Sort(a);

            var minDistance = double.MaxValue;
            var minX = int.MaxValue;
            var minY = int.MaxValue;

            for (var i = 0; i < a.Length - 1; ++i)
            {
                var distance = Math.Abs(a[i + 1] - a[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minX = i;
                    minY = i + 1;
                }
            }

            return new Point(a[minX], a[minY]);
        }
    }
}
