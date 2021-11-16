namespace WbsAlgorithms.Sorting
{
    public class MergeSort
    {
        /// <summary>
        /// Sorts an array of integers using the MergeSort algorithm.
        /// Assumption: The numbers in the input array are distinct.
        ///
        /// MergeSort is a recursive divide-and-conquer algorithm. With every recursive 
        /// call the algorithm breaks the input array in half and sorts the first and 
        /// second halves recursively. The merge step combines these two sorted arrays 
        /// into a single sorted array.
        ///
        /// [AlgoIlluminated-1] p.12-26 1.4 MergeSort algorithm. An example of a divide-and-conquer technique.
        /// </summary>
        /// <param name="a">An array of n distinct numbers, in arbitrary order.</param>
        /// <returns>An array of the same numbers and input, sorted from smallest to largest.</returns>
        public static int[] Sort(int[] a)
        {
            return SortRecursively(a, 0, a.Length - 1);
        }

        // i = the first index in the subarray (inclusive)
        // j = the last index in the subarray (inclusive).
        private static int[] SortRecursively(int[] a, int i, int j)
        {
            // Base case: a one-element array is already sorted.
            if (i == j)
                return new int[] { a[i] };
            else
            {
                // Two recursive calls split the input array into two subarrays and sorts 
                // each subarray with the help of the merge step.
                var mid = i + (j - i) / 2;
                var c = SortRecursively(a, i, mid);        // the first half of the array a
                var d = SortRecursively(a, mid+1, j);      // the second half of the array a

                // The merge step performs the comparisons between the subarray's
                // elements and returns a single sorted array.
                return MergeArrays(c, d);
            }
        }

        // MergeArrays traverses indices of the sorted subarrays populating 
        // the output array from left to right in sorted order.
        private static int[] MergeArrays(int[] c, int[] d)
        {
            // i - an index to traverse the sorted subarray C
            // j - an index to traverse the sorted subarray D
            // k - an index to traverse the output B array
            int i = 0, j = 0, k = 0;

            // The length of the output array.
            int n = c.Length + d.Length;

            // The output array.
            var b = new int[n];

            // Traverse all three arrays. Keep track of when the traversal of C or D falls off the end.
            while (k < n)
            {
                // Identify the minimum element in either C or D. Copy the element to the output array B. 
                if (i < c.Length && j < d.Length)
                {
                    if (c[i] < d[j])
                    {
                        b[k] = c[i];
                        ++i;
                    }
                    else // d[j] >= c[i]
                    {
                        b[k] = d[j];
                        ++j;
                    }
                }
                // Check if C still has any elements to copy.
                else if (i < c.Length)
                {
                    b[k] = c[i];
                    ++i;
                }
                // Check if D still has any elements to copy.
                else if (j < d.Length)
                {
                    b[k] = d[j];
                    ++j;
                }

                ++k;
            }

            return b;
        }
    }
}
