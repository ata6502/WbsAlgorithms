using System.Diagnostics;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.PairPoint
{
    public class FarthestPair1D
    {
        /// <summary>
        /// Finds the farthest pair in an  unsorted array. Complexity O(n)
        /// 
        /// [Sedgewick] 1.4.17 p.210 - Find a farthest pair in an array of double values.
        /// The farthest pair includes two values whose difference is no smaller than 
        /// the difference of any other pair. The running time of the program should be 
        /// linear in the worst case.
        /// </summary>
        /// <param name="a">An array of double values. The array should contain at least two elements.</param>
        /// <returns>The farthest pair as a 2D point.</returns>
        public static Point FindPair(double[] a)
        {
            Debug.Assert(a.Length >= 2);

            var minIndex = 0;
            var maxIndex = 0;

            // Traverse all values in the array and find minimum and maximum.
            // The difference between the minimum and maximum is the farthest pair.
            for (int i = 0; i < a.Length; ++i)
            {
                if (a[i] < a[minIndex])
                    minIndex = i;
                else if (a[i] > a[maxIndex])
                    maxIndex = i;
            }

            return new Point(a[minIndex], a[maxIndex]);
        }
    }
}
