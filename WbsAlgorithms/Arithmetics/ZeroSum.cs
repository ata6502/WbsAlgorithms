using System;

namespace WbsAlgorithms.Arithmetics
{
    public class ZeroSum
    {
        /// <summary>
        /// Counts pairs that sum to zero. Complexity O(n^2)
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs that sum to zero</returns>
        public static int CountPairsQuadratic(int[] a)
        {
            var len = a.Length;
            var cnt = 0;

            for (var i = 0; i < len; ++i)
                for (var j = i + 1; j < len; ++j)
                    if (a[i] + a[j] == 0)
                        ++cnt;
            return cnt;
        }

        /// <summary>
        /// Counts pairs that sum to zero. Complexity O(n)
        /// The input arrays needs to be sorted.
        /// 
        /// [Sedgewick] 1.4.15 p.210 - Develop an implementation of TwoSum that
        /// uses a linear algorithm after the array is sorted.
        /// </summary>
        /// <param name="a">A sorted array of integers</param>
        /// <returns>The number of pairs that sum to zero</returns>
        public static int CountPairsLinear(int[] a)
        {
            var cnt = 0;            // the number of pairs that sum to zero
            var i = 0;              // lower index
            var j = a.Length - 1;   // upper index

            while (i < j)
            {
                // A special case - a sequence of zeroes.
                if (a[i] == 0 || a[j] == 0)
                {
                    // Find the boundary of the sequence of zeroes [i,j]
                    while (a[i] != 0)
                        ++i;
                    while (a[j] != 0)
                        --j;

                    // Check if there are at least two zeroes in the array: j-i+1 > 1 => j-i > 0
                    if (j - i > 0)
                    {
                        // There are j - i + 1 zeros in the array.
                        var n = j - i + 1;

                        // Calculate the number of pairs of zeroes.
                        cnt += n * (n - 1) / 2;
                    }

                    // Stop the while loop.
                    j = 0;
                    i = 1;
                }
                else if (Math.Abs(a[i]) == a[j])
                {
                    // The lower number has to be negative.
                    if (a[i] < 0)
                    {
                        var num = a[j];
                        var neg = 0;
                        var pos = 0;

                        // Count the negative numbers.
                        while (Math.Abs(a[i]) == num && a[i] < 0)
                        {
                            ++i;
                            ++neg;
                        }

                        // Count the corresponding positive numbers.
                        while (a[j] == num)
                        {
                            --j;
                            ++pos;
                        }

                        // Calculate the number of pairs that sum to zero.
                        cnt += neg * pos;
                    }
                    else
                    {
                        // If the lower number is positive it means that there is just
                        // a sequence of equal positive numbers between a[i] and a[j].
                        // We can stop the while loop.
                        j = 0;
                        i = 1;
                    }
                }
                else if (Math.Abs(a[i]) > a[j])
                    ++i;
                else if (Math.Abs(a[i]) < a[j])
                    --j;
            }

            return cnt;
        }
    }
}
