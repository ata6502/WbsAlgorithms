using System;

namespace WbsAlgorithms.Arithmetic
{
    /// <summary>
    /// A binary gap within a positive integer N is any maximal sequence of consecutive zeros
    /// that is surrounded by ones at both ends in the binary representation of N.
    ///
    /// Example #1: Input: 9 == 1001b           Output: 2
    /// Example #2: Input: 529 == 1000010001b   Output: 4
    /// Example #3: Input: 20 == 10100b         Output: 1
    /// Example #4: Input: 15 == 1111b          no binary gap
    /// Example #5: Input: 32 == 100000b        no binary gap
    /// Example #6: Input: 1041 == 10000010001b Output: 5
    ///
    /// [Codility] https://app.codility.com/programmers/lessons/1-iterations/binary_gap/
    /// </summary>
    public class BinaryGap
    {
        /// <summary>
        /// Finds longest sequence of zeros in binary representation of an integer - a binary gap.
        /// </summary>
        /// <param name="n">A positive integer</param>
        /// <returns>The length of n's longest binary gap; 0 if n doesn't contain a binary gap</returns>
        public static int GetMaxBinaryGap(int n)
        {
            // Skip trailing zeroes.
            while ((n & 1) == 0)
                n >>= 1;

            var gap = 0;
            var max = 0;

            while (n != 0)
            {
                // Check if there is zero.
                if ((n & 1) == 0)
                {
                    ++gap;
                }
                else
                {
                    // Do we have a new max gap?
                    max = Math.Max(max, gap);

                    gap = 0;
                }

                // Proceed to the next digit in N.
                n >>= 1;
            }

            return max;
        }
    }
}
