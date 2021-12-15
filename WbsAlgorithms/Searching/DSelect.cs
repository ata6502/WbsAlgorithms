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
        /// <param name="a">An array of n distinct numbers in arbitrary order</param>
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

            // Calculate the size of the subarray a[l..r]
            var n = r - l + 1;

            //
            // Find the median-of-medians of the subarray a[l..r]
            //

            // Calculate the number of 5-element groups. The last group may have
            // fewer elements.
            var size = n / 5 + (n % 5 == 0 ? 0 : 1);

            // The m array contains medians for each 5-element group.
            var m = new int[size];

            // Caclulate the medians.
            for (var h = 0; h < m.Length; ++h)
                m[h] = GetMedian(a, l, r, h);

            // Find the median-of-medians. Since the length of the array M is roughly n/5,
            // the median-of-medians is the M's n/10-th order statistic.
            // Refer to [AlgoIlluminated-1] p.169-170 
            var p = FindValueRecursively(m, 0, m.Length - 1, n / 10);

            // Determine the index of the median-of-medians in the input array A.
            // TODO: This operation is O(n). Try to avoid it.
            var j = Array.FindIndex(a, num => num == p);

            // Treat the median-of-medians as a pivot. Move the pivot to
            // the first position in the subarray a[l..r]
            int tmp = a[l];
            a[l] = a[j];
            a[j] = tmp;

            // The rest of the algorithm is identical with RSelect.

            // Partition the subarray a[l..r] around the pivot p. 
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
        /// <param name="a">An array of n distinct numbers in arbitrary order</param>
        /// <param name="begin">The left boundary of a subarray a[begin..end]</param>
        /// <param name="end">The right boundary of a subarray a[begin..end]</param>
        /// <param name="h">The group number of 5 elements in the input array A</param>
        /// <returns>The middle element from the h-th group of 5</returns>
        private static int GetMedian(int[] a, int begin, int end, int h)
        {
            // Calculate a boundary a[l..r] of a subarray for the group h within a[begin..end]
            // The subarray a[l..r] may have at most 5 elements.
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
            else if (n == 3 || n == 4)
            {
                // In the 3 or 4-element array, the median is the second smallest element.
                var min1 = int.MaxValue; // the smallest element
                var min2 = int.MaxValue; // the second smallest element

                for(var i = l; i < l + n; ++i)
                {
                    if (a[i] < min1)
                    {
                        min2 = min1;
                        min1 = a[i];
                    }
                    else if (a[i] < min2)
                    {
                        min2 = a[i];
                    }
                }

                return min2;
            }
            else // n == 5
            {
                // In the 5-element array, the median is the third smallest element.
                var min1 = int.MaxValue; // the smallest element
                var min2 = int.MaxValue; // the second smallest element
                var min3 = int.MaxValue; // the third smallest element

                for (var i = l; i < l + n; ++i)
                {
                    if (a[i] < min1)
                    {
                        min3 = min2;
                        min2 = min1;
                        min1 = a[i];
                    }
                    else if (a[i] < min2)
                    {
                        min3 = min2;
                        min2 = a[i];
                    }
                    else if (a[i] < min3)
                    {
                        min3 = a[i];
                    }
                }

                return min3;
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
