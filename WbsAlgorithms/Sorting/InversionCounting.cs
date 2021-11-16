namespace WbsAlgorithms.Sorting
{
    public class InversionCounting
    {
        /// <summary>
        /// Counts inversions in an array using the divide-and-conquer approach.
        ///
        /// This method utilizes the MergeSort algorithm to compute the number of inversions as
        /// a byproduct of sorting. Because sorting runs in O(n log(n)) the counting inversions
        /// algorithm also runs in O(n log(n)).
        ///
        /// The inversions (i, j) of an array of length n can be classified into one of three categories:
        /// 1. left inversion: an inversion with i, j both in the first half of the array i.e., i,j <= n/2
        /// 2. right inversion: an inversion with i, j both in the second half of the array i.e., i,j > n/2  
        /// 3. split inversion: an inversion with i in the left half and j in the right half i.e., i <= n/2 < j
        /// 
        /// [AlgoIlluminated-1] p.61-70 3.2 Counting inversions in O(n log(n)) 
        /// </summary>
        /// <param name="a">An array of n distinct integers</param>
        /// <param name="i">The first index in the subarray (inclusive)</param>
        /// <param name="j">The last index in the subarray (inclusive)</param>
        /// <returns>A sorted array as well as the number of inversions</returns>
        public static (int[] SortedArray, long InversionCount) SortAndCountInversions(int[] a, int i, int j)
        {
            // Base case: a one-element array is already sorted.
            if (i == j)
                return (new int[] { a[i] }, 0);
            else
            {
                var mid = i + (j - i) / 2;

                // The "divide" step is the same as in the MergeSort algorithm, with one recursive call for 
                // the left half of the array and one for the right half.

                // The first recursive call, on the first half of the input array, recursively counts all 
                // the left inversions (and nothing else). 
                var left = SortAndCountInversions(a, i, mid);

                // The second recursive call counts all the right inversions. 
                var right = SortAndCountInversions(a, mid + 1, j);

                // MergeAndCountSplitInversions counts the inversions not counted by either recursive call -
                // the split inversions. This is the "combine" step of the algorithm. 
                var split = MergeAndCountSplitInversions(left.SortedArray, right.SortedArray);

                return (split.SortedArray, left.InversionCount + right.InversionCount + split.InversionCount);
            }
        }

        /// <summary>
        /// Merges two sorted arrays into one array. Asymptotic running time: O(n)
        /// </summary>
        /// <param name="c">The first sorted array</param>
        /// <param name="d">The second sorted array</param>
        /// <returns>A sorted array containing elements from both input arrays. Also, the number of split inversions performed.</returns>
        private static (int[] SortedArray, long InversionCount) MergeAndCountSplitInversions(int[] c, int[] d)
        {
            // i - an index to traverse the sorted subarray C
            // j - an index to traverse the sorted subarray D
            // k - an index to traverse the output B array
            int i = 0, j = 0, k = 0;

            // The split inversion counter.
            long invCount = 0;

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

                        // This is the only difference between the MergeSort.MergeArrays method and
                        // the MergeAndCountSplitInversions method. Every time an element is copied
                        // from the second subarray D to the output array B, we increment the running
                        // count by the number of elements remaining in the first subarray C which is
                        // Length(C) - i
                        invCount += c.Length - i;
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

            return (b, invCount);
        }

        /// <summary>
        /// Counts inversions in an array using a brute-force approach.
        /// Asymptotic running time: O(n^2)
        /// </summary>
        /// <param name="a">An array of n distinct integers</param>
        /// <returns>The number of inversions of A</returns>
        public static long CountInversionsBruteForce(int[] a)
        {
            var n = a.Length;
            var invCount = 0L;

            for (var i = 0; i < n - 1; ++i)
                for (var j = i + 1; j < n; ++j)
                    if (a[i] > a[j])
                        ++invCount;

            return invCount;
        }
    }
}
