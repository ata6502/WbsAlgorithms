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
        /// The input array needs to be sorted.
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

        /// <summary>
        /// Counts triples that sum to zero. Complexity O(n^3)
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of triplets that sum to 0</returns>
        public static int CountTripletsCubic(int[] a)
        {
            var len = a.Length;
            var cnt = 0;

            for (var i = 0; i < len; ++i)
                for (var j = i + 1; j < len; ++j)
                    for (var k = j + 1; k < len; ++k)
                        if (a[i] + a[j] + a[k] == 0)
                            ++cnt;
            return cnt;
        }

        /// <summary>
        /// Counts triples that sum to zero. Complexity O(n^2)
        /// The input array needs to be sorted.
        /// 
        /// [Sedgewick] 1.4.15 p.210 - Develop a quadratic algorithm
        /// for the 3-sum problem.
        /// </summary>
        /// <param name="a">A sorted array of integers</param>
        /// <returns>The number of pairs that sum to zero</returns>
        public static int CountTripletsQuadratic(int[] a)
        {
            var cnt = 0; // the number of triplets that sum to zero

            for (var k = 0; k < a.Length; ++k)
            {
                var i = k + 1;         // lower index
                var j = a.Length - 1;  // upper index

                while (i < j)
                {
                    // Check if i and j indices point to a sequence of zeroes.
                    if (a[i] == 0 && a[j] == 0)
                    {
                        // If so, stop the while loop.
                        j = 0;
                        i = 1;
                    }
                    else if (a[i] + a[j] + a[k] == 0)
                    {
                        // Because a[k] has a fixed value, we can count the number
                        // of pairs {a[i],a[j]} that sum to -a[k]. Together,
                        // the values a[k], a[i], and a[j] form a triplet.

                        // A special case - the lower index i and the upper index j
                        // point to the array elements that have the same value.
                        if (a[i] == a[j])
                        {
                            // Calculate the number of elements.
                            var n = j - i + 1;

                            // Calculate the number of pairs that can be created
                            // from n elements: cnt = n!/(2!*(n-2)!) = n*(n-1)/2
                            cnt += n * (n - 1) / 2;

                            // There are no more numbers to process. We can stop
                            // the while loop.
                            j = 0;
                            i = 1;
                        }
                        else
                        {
                            // Keep both indices.
                            var start_i = i;
                            var start_j = j;

                            // Wind up the index i as long as the triplet is zero.
                            while (a[i] + a[start_j] + a[k] == 0 && i < start_j)
                                ++i;

                            // Wind down the index j as long as the triplet is zero.
                            while (a[start_i] + a[j] + a[k] == 0 && j > start_i)
                                --j;

                            // Calculate the number of pairs that could be formed from
                            // two intervals: [start_i, i] and [start_j, j]
                            cnt += (i - start_i) * (start_j - j);
                        }
                    }
                    else if (a[i] + a[j] + a[k] < 0)
                        ++i;
                    else
                        --j;
                }
            }

            cnt += CountZeroTriplets(a);

            return cnt;
        }

        private static int CountZeroTriplets(int[] a)
        {
            // Scan the input array to count zeroes.
            var n = 0; // the number of zeroes
            var i = 0;
            while (i < a.Length && a[i] <= 0)
            {
                if (a[i] == 0)
                    ++n;
                ++i;
            }

            // Calculate the number of triplets that can be formed from
            // n zeroes: cnt = n! / (3!*(n-3)!)
            var cnt = (n - 2) * (n - 1) * n / 6;

            return cnt;
        }
    }
}
