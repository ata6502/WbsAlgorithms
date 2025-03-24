using System;
using System.Diagnostics;

namespace WbsAlgorithms.Arrays
{
    /*
        Maximum Sum in Contiguous Subarray

        [interviewbit]: https://www.interviewbit.com/problems/max-sum-contiguous-subarray/
        Solution (lecture): https://www.youtube.com/watch?v=yBCzO0FpsVc&t=294s
        Solution (C++ & Kadane's algo): https://www.youtube.com/watch?v=ohHWQf1HDfU

        Find the contiguous subarray within an array A of length N which has the largest sum.
        Return an integer representing the maximum possible sum of the contiguous subarray.

        Example 1: A = [1, 2, 3, 4, -10]
        Output: 10
        Explanation: The subarray [1, 2, 3, 4] has the maximum possible sum of 10.

        Example 2: A = [-2, 1, -3, 4, -1, 2, 1, -5, 4]
        Output: 6
        Explanation: The subarray [4, -1, 2, 1] has the maximum possible sum of 6.
    */
    public class MaximumSum
    {
        public static int GetMaxSumOfContiguousSubarray(int[] a)
        {
            return GetMaxSumOfContiguousSubarrayRecursive(a, 0, a.Length - 1);
        }

        /*
            Divide-and-conquer approach

            When we split the input array into two subarrays, the contiguous subarray that has 
            the max sum can be:
            - entirely in the left half of the input array
            - entirely in the right half of the input array
            - cross the left and the half part of the input array i.e., it crosses the center of the input array

            Recursive formula (n is the size of the array A):

                     /
                    |  c              if n = 1
            T(n) = <
                    |  2T(n/2) + Θ(n) if n > 1
                     \

            Solution to the recursive formula: T(n) = cn + c'nlog(n)

            Time complexity: O(n log(n))
        */
        private static int GetMaxSumOfContiguousSubarrayRecursive(int[] a, int i, int j)
        {
            Debug.Assert(j >= i);

            if (i == j)
                return a[i];
            else
            {
                var c = (i + j) / 2; // Θ(1)

                // Get the max sum in the left half of the array.
                var leftSum = GetMaxSumOfContiguousSubarrayRecursive(a, i, c); // T(n/2)

                // Get the max sum in the right half of the array.
                var rightSum = GetMaxSumOfContiguousSubarrayRecursive(a, c + 1, j); // T(n/2)

                // Get the max sum of a subarray that crosses the center.
                var centerSum = GetMaxSumOfCrossSubarray(a, i, c, j); // Θ(n)

                return Math.Max(Math.Max(leftSum, rightSum), centerSum); // Θ(1)
            }
        }

        // Time complexity: Θ(n) - we start from the center and go left and right.
        private static int GetMaxSumOfCrossSubarray(int[] a, int i, int c, int j)
        {
            var sum = 0;
            var maxSumLeft = Int32.MinValue;
            var maxSumRight = Int32.MinValue;

            // Find the max sum starting from the center to the left.
            for (var p = c; p >= i; --p)
            {
                sum += a[p];
                maxSumLeft = Math.Max(sum, maxSumLeft);
            }

            sum = 0;

            // Find the max sum starting from the center to the right.
            for (var p = c + 1; p <= j; ++p)
            {
                sum += a[p];
                maxSumRight = Math.Max(sum, maxSumRight);
            }

            // Add both sums. This is our max sum of the across both halves of the input array a[i..j]
            return maxSumLeft + maxSumRight;
        }

        // Iterate over all possible subarrays.
        // Time complexity: O(n^2)
        public static int GetMaxSumOfContiguousSubarrayBruteForce(int[] a)
        {
            var max = Int32.MinValue;

            // i is a start index.
            for (var i = 0; i < a.Length; ++i)
            {
                var sum = 0;

                // j is an end index.
                for (var j = i; j < a.Length; ++j)
                {
                    sum += a[j];
                    max = Math.Max(max, sum);
                }
            }

            return max;
        }
    }
}
