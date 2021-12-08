using System;

namespace WbsAlgorithms.Searching
{
    public class RSelect
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        // TODO: Add comments.

        // Input: An array A of n distinct number in arbitrary order.
        //        The order statistic we are looking for: an integer 0<=i<=n-1
        // Output: The i-th order statistic i.e., the i-th smallest element in the input array A.
        public static int GetValue(int[] a, int orderStatistic)
        {
            return GetValue(a, 0, a.Length - 1, orderStatistic);
        }

        private static int GetValue(int[] a, int l, int r, int orderStatistic)
        {
            // base case
            if (l >= r)
                return a[r];

            // Choose a pivot uniformly at random.
            var p = _rnd.Next(l, r + 1);

            // Make the pivot first.
            int tmp = a[l];
            a[l] = a[p];
            a[p] = tmp;

            // Partition the input array A around the pivot p. 
            // Return the pivot position in the partitioned array.
            var j = Partition(a, l, r);

            // Examine the cases depending whether the pivot is greater or smaller 
            // than the element we are looking for.

            // [1] The pivot happens to be the order statistic we are looking for.
            if (j == orderStatistic)
                return a[j];
            // [2] The pivot is greater than the element we are looking for.
            else if (j > orderStatistic)
                return GetValue(a, l, j - 1, orderStatistic);
            // [3] The pivot is smaller than the element we are looking for.
            else
                // We do not need to modify the order statistic we are looking for because 
                // the algorithm works in-place and the indices of elements are preserved.
                return GetValue(a, j + 1, r, orderStatistic);
        }

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
