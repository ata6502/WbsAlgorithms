using System;

namespace WbsAlgorithms.Sorting
{
    /// <summary>
    /// PivotStrategy represents different ways of choosing a pivot. 
    /// </summary>
    public enum PivotStrategy
    {
        First,  // always use the first element as the pivot
        Last,   // always use the last element as the pivot
        Random, // use a random element as the pivot
        Median  // use the median-of-three as the pivot
    }

    public class QuickSort
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());
        private static int _comparisonCount = 0;

        /// <summary>
        /// Sorts an input array in-place using the QuickSort algorithm.
        /// Assumption: The input array has only distict elements, with no duplicates.
        /// 
        /// [AlgoIlluminated-1] p.117-151 QuickSort
        /// </summary>
        /// <param name="a">An array containing distinct integers</param>
        public static void Sort(int[] a)
        {
            SortRecursively(a, 0, a.Length - 1);
        }

        /// <summary>
        /// Sorts an input array in-place using the QuickSort algorithm.
        /// Postcondition: Elements of the subarray a[l],a[l+1],...,a[r] are sorted from smallest to largest.
        /// </summary>
        /// <param name="a">An array of n distinct integers</param>
        /// <param name="l">left endpoints l = {1,2,...,n}, l<=r</param>
        /// <param name="r">right endpoints r = {1,2,...,n}, l<=r</param>
        private static void SortRecursively(int[] a, int l, int r)
        {
            // Base case. A 0- or 1-element subarray.
            if (l >= r)
                return;

            // Choose a pivot randomly.
            int i = _rnd.Next(l, r + 1);

            // Make the pivot first.
            int tmp = a[l];
            a[l] = a[i];
            a[i] = tmp;

            // Get the new pivot position.
            int j = Partition(a, l, r);

            // Recursively sort the first part of the array.
            SortRecursively(a, l, j - 1);

            // Recursively sort the second part of the array.
            SortRecursively(a, j + 1, r);
        }

        /// <summary>
        /// Sorts an input array in-place using the QuickSort algorithm and a specified strategy.
        /// 
        /// [AlgoIlluminated-1] p.153 Problem 5.6 Implement the QuickSort algorithm.
        /// </summary>
        /// <param name="a">An array of n distinct integers</param>
        /// <param name="strategy">A way of choosing a pivot</param>
        /// <returns>The number of comparisons performed during execution of the QuickSort algorithm</returns>
        public static int Sort(int[] a, PivotStrategy strategy)
        {
            _comparisonCount = 0;
            SortRecursively(a, 0, a.Length - 1, strategy);
            return _comparisonCount;
        }

        private static void SortRecursively(int[] a, int l, int r, PivotStrategy strategy)
        {
            // Base case. A 0- or 1-element subarray.
            if (l >= r)
                return;

            // Count the number of comparisons: rightIndex - leftIndex
            _comparisonCount += r - l;

            // Choose a pivot randomly.
            int i = ChoosePivotIndex(a, l, r, strategy);

            // Make the pivot first.
            int tmp = a[l];
            a[l] = a[i];
            a[i] = tmp;

            // Get the new pivot position.
            int j = Partition(a, l, r);

            // Recursively sort the first part of the array.
            SortRecursively(a, l, j - 1, strategy);

            // Recursively sort the second part of the array.
            SortRecursively(a, j + 1, r, strategy);
        }

        /// <summary>
        /// Partitions elements of an array around a pivot.
        /// Postcondition: Elements of the subarray a[l],a[l+1],...,a[r] are partitioned around a[l].
        /// </summary>
        /// <param name="a">An array of n distinct integers. The Partition method operates only on a subarray a[l],a[l+1],...,a[r]</param>
        /// <param name="l">Left endpoints l = {1,2,...,n}, l<=r</param>
        /// <param name="r">Right endpoints r = {1,2,...,n}, l<=r</param>
        /// <returns>The final position of the pivot element.</returns>
        private static int Partition(int[] a, int l, int r)
        {
            // Get the pivot from the first element of the subarray. 
            int p = a[l];

            // j keeps track of processed elements.
            // i keeps track of the boundary between processed elements that are 
            // less than and greater than the pivot

            // All the elements between the pivot and i are less than the pivot.
            // All the elements between i and j are greater than the pivot.

            int i = l + 1;
            int tmp;
            for (int j = l + 1; j <= r; ++j)
            {
                // If a[j] < p, we need to restore the invariant.
                // Otherwise, we don't need to do anything.
                if (a[j] < p)
                {
                    // Swap elements to restore the invariant.
                    tmp = a[j];
                    a[j] = a[i];
                    a[i] = tmp;

                    // Update the boundary between elements less than and greater than the pivot.
                    ++i;
                }
            }

            // Swap the pivot into the correct position.
            tmp = a[l];
            a[l] = a[i - 1];
            a[i - 1] = tmp;

            // Return the final pivot position.
            return i - 1;
        }

        /// <summary>
        /// Chooses a pivot from the input array using a given strategy.
        /// </summary>
        /// <param name="a">an array of n distinct integers</param>
        /// <param name="l">left endpoints l = {1,2,...,n}, l<=r</param>
        /// <param name="r">right endpoints r = {1,2,...,n}, l<=r</param>
        /// <param name="strategy">a way to choose a pivot</param>
        /// <returns>An index of a pivot element chosen from the input array</returns>
        private static int ChoosePivotIndex(int[] a, int l, int r, PivotStrategy strategy)
        {
            int GetMiddle(int[] a, int l, int r)
            {
                int m = (r + l) / 2;

                // Determine the first, middle, and last elements of a given subarray.
                int x = a[l]; // first
                int y = a[m]; // middle
                int z = a[r]; // last

                // Determine the median-of-three element - an element whose value is in 
                // between the other two.
                if ((x > y && x < z) || (x > z && x < y))
                    return l;
                if ((y > x && y < z) || (y > z && y < x))
                    return m;
                if ((z > x && z < y) || (z > y && z < x))
                    return r;

                throw new ArgumentException("invalid arguments");
            }

            switch (strategy)
            {
                // Pick the first element.
                case PivotStrategy.First:
                    return l;
                // Pick the last element.
                case PivotStrategy.Last:
                    return r;
                case PivotStrategy.Random:
                    return _rnd.Next(l, r + 1);
                // Pick the median element. This is the most perfectly balanced split.
                case PivotStrategy.Median:
                    if (r - l + 1 <= 2)
                        return l;
                    else
                        return GetMiddle(a, l, r);
                default:
                    throw new ArgumentException(nameof(strategy));
            }
        }
    }
}
