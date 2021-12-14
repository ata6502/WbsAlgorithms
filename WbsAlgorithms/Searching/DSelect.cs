using System;
using System.Diagnostics;

namespace WbsAlgorithms.Searching
{
    /// <summary>
    /// A deterministic linear-time algorithm for the selection problem.
    /// DSelect has larger constant factors in the running time than RSelect.
    /// Also, DSelect alocates additional memory.
    /// DSelect uses a median-of-medians as a proxy for the true median.
    /// Refer to [AlgoIlluminated-1] p.168 for details.
    /// </summary>
    public class DSelect
    {
        /// <summary>
        /// Finds the i-th order statistic in the input array.
        /// 
        /// References:
        /// http://web.mit.edu/neboat/www/6.046-fa09/rec3.pdf
        /// 
        /// [AlgoIlluminated-1] p.167-179 Linear-Time Selection - DSelect
        /// </summary>
        /// <param name="a">An array of n distinct number in arbitrary order</param>
        /// <param name="orderStatistic">The order statistic we are looking for: an integer i such that  0 <= i <= n-1</param>
        /// <returns>The i-th order statistic i.e., the i-th smallest element in the input array</returns>
        public static int FindValue(int[] a, int orderStatistic)
        {
            return FindValueRecursively(a, 0, a.Length - 1, orderStatistic);
        }

        private static int FindValueRecursively(int[] a, int l, int r, int orderStatistic)
        {
            // base case
            if (l >= r)
                return a[r];

            var n = r - l + 1;

            // Find the median-of-medians of the input array A.

            var size = n / 5 + (n % 5 == 0 ? 0 : 1);
            var C = new int[size];

            for (var h = 0; h < C.Length; ++h)
                C[h] = GetMedian(a, l, r, h); 

            var p = FindValueRecursively(C, 0, C.Length - 1, n / 10);
            var j = Array.FindIndex(a, num => num == p);

            // Make the pivot first.
            int tmp = a[l];
            a[l] = a[j];
            a[j] = tmp;

            // The rest of the algorithm is identical with RSelect.

            // Partition the input array A around the pivot p. 
            j = Partition(a, l, r);

            if (j == orderStatistic)
                return a[j];
            else if (j > orderStatistic)
                return FindValueRecursively(a, l, j - 1, orderStatistic);
            else
                return FindValueRecursively(a, j + 1, r, orderStatistic);
        }

        /// <summary>
        /// Finds the middle element of a given group of 5.
        /// </summary>
        /// <param name="a">An array of n distinct number in arbitrary order</param>
        /// <param name="begin">The left boundary of a subarray of A</param>
        /// <param name="end">The right boundary of a subarray of A</param>
        /// <param name="h">The group number of 5 elements in the input array A</param>
        /// <returns>The middle element from the h-th group of 5</returns>
        private static int GetMedian(int[] a, int begin, int end, int h)
        {
            var l = begin + 5 * h;
            var r = Math.Min(l + 4 , end);

            return GetMedian(a, l, r);
        }

        private static int GetMedian(int[] a, int l, int r)
        {
            var n = r - l + 1;

            Debug.Assert(n >= 1 && n <= 5);

            if (n == 1)
                return a[l];
            else if (n == 2)
                return Math.Min(a[l], a[r]);
            else if (n == 3)
            {
                int x = a[l], y = a[l+1], z = a[l+2];
                if ((x < z && x > y) || (x > z && x < y))
                    return x;
                else if ((y < z && y > x) || (y > z && y < x))
                    return y;
                else if ((z < y && z > x) || (z > y && z < x))
                    return z;
                else
                    throw new ArgumentException("Elements in the input array A are not distinct.");
            }
            else // n == 4 or n == 5
            {
                // TODO: Figure out a way to find a median without sorting.
                var copy = new int[n];
                Array.Copy(a, l, copy, 0, n);
                Array.Sort(copy);

                return n == 4 ? copy[1] : copy[2];
            }
        }

        // The Partition method is identical to the one in QuickSort.
        // Refer to QuickSort for argument description and comments.
        private static int Partition(int[] a, int l, int r)
        {
            int p = a[l];

            int i = l + 1;
            int tmp;
            for (int j = l + 1; j <= r; ++j)
            {
                if (a[j] < p)
                {
                    tmp = a[j];
                    a[j] = a[i];
                    a[i] = tmp;

                    ++i;
                }
            }

            tmp = a[l];
            a[l] = a[i - 1];
            a[i - 1] = tmp;

            return i - 1;
        }
    }
}
