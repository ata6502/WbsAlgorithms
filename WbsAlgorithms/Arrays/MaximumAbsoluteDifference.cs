using System;

namespace WbsAlgorithmsTest.Arrays
{
    /*
        Maximum Absolute Difference

        [geeksforgeeks]: https://www.geeksforgeeks.org/maximum-absolute-difference-value-index-sums/
        [interviewbit]: https://www.interviewbit.com/problems/maximum-absolute-difference/

        You are given an array of N integers, A1, A2 , ... , An. Return maximum value of f(i, j) for all 1 ≤ i, j ≤ N.
        f(i, j) is defined as |A[i] - A[j]| + |i - j|, where |x| denotes absolute value of x.

        Example:
        A = [1, 3, -1]

        f(1, 1) = f(2, 2) = f(3, 3) = 0
        f(1, 2) = f(2, 1) = |1 - 3| + |1 - 2| = 3
        f(1, 3) = f(3, 1) = |1 - (-1)| + |1 - 3| = 4
        f(2, 3) = f(3, 2) = |3 - (-1)| + |2 - 3| = 5  <-- answer

        An efficient solution in O(n) time complexity can be worked out using the properties of absolute values.
        f(i, j) = |A[i] - A[j]| + |i - j| can be written in 4 ways:

        1. A[i] - A[j] > 0 and i - j > 0 --> A[i] > A[j] and i > j :   A[i] - A[j]  +   i - j  =  (A[i] + i) - (A[j] + j)
        2. A[i] - A[j] > 0 and i - j < 0 --> A[i] > A[j] and i < j :   A[i] - A[j]  + -(i - j) =  (A[i] - i) - (A[j] - j)
        3. A[i] - A[j] < 0 and i - j > 0 --> A[i] < A[j] and i > j : -(A[i] - A[j]) +   i - j  = -(A[i] - i) + (A[j] - j)
        4. A[i] - A[j] < 0 and i - j < 0 --> A[i] < A[j] and i < j : -(A[i] - A[j]) + -(i - j) = -(A[i] + i) + (A[j] + j)
    
        Note that cases 1 and 4 are equivalent and so are cases 2 and 3 (why?) and hence we can design our algorithm only for 
        two cases as they will cover all the possibilities. Let's pick the case 1 and 2.
    */
    public class MaximumAbsoluteDifference
    {
        public static int GetDifference(int[] a)
        {
            int max1 = Int32.MinValue, min1 = Int32.MaxValue;
            int max2 = Int32.MinValue, min2 = Int32.MaxValue;

            for (int i = 0; i < a.Length; ++i)
            {
                // Calculate values for case #1
                // j is just another value of index i
                max1 = Math.Max(max1, a[i] + i);    // max of (A[i] + i)
                min1 = Math.Min(min1, a[i] + i);    // min of (A[j] + j)

                // Calculate values for case #2
                max2 = Math.Max(max2, a[i] - i);    // max of (A[i] - i)
                min2 = Math.Min(min2, a[i] - i);    // min of (A[j] - j)
            }

            // Determine which case, #1 or #2, represents the maximum absolute difference.
            return Math.Max(max1 - min1, max2 - min2);
        }

        public static int GetDifferenceBruteForce(int[] a)
        {
            var max = int.MinValue;
            for (var i = 0; i < a.Length; ++i)
            {
                for (var j = i; j < a.Length; ++j)
                {
                    // Calculate the max absolute difference using the formula.
                    var f = Math.Abs(a[i] - a[j]) + Math.Abs(i - j);

                    max = Math.Max(max, f);
                }
            }

            return max;
        }
    }
}
