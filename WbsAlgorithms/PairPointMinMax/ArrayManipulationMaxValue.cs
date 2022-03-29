using System;

namespace WbsAlgorithms.PairPointMinMax
{
    /// <summary>
    /// Array Manipulation (hard) - the "slope" technique
    /// 
    /// Starting with a 1-based index array of zeros and a list of operations, for each operation add a value 
    /// to each of the array element between two given indices, inclusive. Once all operations have been
    /// performed, return the maximum value in the array.
    /// 
    /// [HackerRank] https://www.hackerrank.com/challenges/crush/problem
    /// </summary>
    public class ArrayManipulationMaxValue
    {
        /// <summary>
        /// Returns the maximum value in an input array after applying given operations using a "slope" technique.
        /// 
        /// An explanation from HackerRank:
        /// For every input line of a-b-k (a - startIndex, b - stopIndex, k - value to add), you are given the range [a..b]
        /// where the values increase by k. Instead of keeping track of actual values increasing, we just keep track of
        /// the rate of change i.e. a slope where the rate started increasing and where it stopped increasing.
        /// This is done by adding k to the position 'a' of the array and adding -k to the position 'b+1' for every
        /// input line a-b-k. b+1 is used because the increase still applies at b.
        /// The maximum final value is an equivalent to the maximum accumulated "slope" starting from the first position.
        /// 
        /// Time complexity: O(n)
        /// Space complexity: O(1)
        /// 
        /// Example ("slope technique"):
        /// 
        /// Input: size = 10, operations = [[1,5,3],[4,8,7],[6,9,1]]
        /// 
        /// Operations are interpreted as adding the values of k between the 1-based 
        /// indices a and b inclusive:
        /// 
        /// a b k
        /// -----
        /// 1 5 3
        /// 4 8 7
        /// 6 9 1
        ///
        /// index-> 1  2  3  4  5  6  7  8  9 10
        ///        [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        ///        [3, 3, 3, 3, 3, 0, 0, 0, 0, 0]
        ///        [3, 3, 3,10,10, 7, 7, 7, 0, 0]
        ///        [3, 3, 3,10,10, 8, 8, 8, 1, 0]
        ///        
        /// Output: The largest value after performing all operations is 10.
        /// </summary>
        /// <param name="size">The number of elements in the input array</param>
        /// <param name="operations">The operations we want to apply to the input array</param>
        /// <returns>The maximum value in the resultant array after applying all operations</returns>
        public static long GetMaxValue(int size, int[,] operations)
        {
            // Determine the number of operations.
            var opCount = operations.GetUpperBound(0) + 1;

            // The resultant array initialized with 0s.
            var a = new long[size];

            // Iterate over all operations.
            for (var i = 0; i < opCount; ++i)
            {
                var startIndex = operations[i, 0] - 1;
                var stopIndex = operations[i, 1] - 1;
                var val = operations[i, 2];

                // Add/subtract val on the boundaries of the slope.
                a[startIndex] += val;
                if (stopIndex + 1 < size)
                    a[stopIndex + 1] -= val;
            }

            // Calculate the maximum value as the maximum accumulated "slope".
            var max = 0L;
            var sum = 0L;
            for (var i = 0; i < size; ++i)
            {
                sum += a[i];
                max = Math.Max(max, sum);
            }

            return max;
        }

        /// <summary>
        /// Returns the maximum value in an input array after applying given operations using brute-force.
        /// 
        /// Example (brute force):
        /// 
        /// Input: size = 10, operations = [[1,5,3],[4,8,7],[6,9,1]]
        /// 
        /// Operations are interpreted as adding the values of k between the 1-based 
        /// indices a and b inclusive:
        /// 
        /// a b k
        /// -----
        /// 1 5 3
        /// 4 8 7
        /// 6 9 1
        ///
        /// index-> 1  2  3  4  5  6  7  8  9 10
        ///        [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        ///        [3, 0, 0, 0, 0,-3, 0, 0, 0, 0]
        ///        [3, 0, 0, 7, 0,-3, 0, 0,-7, 0]
        ///        [3, 0, 0, 7, 0,-2, 0, 0,-7, 1] 3 -> 3+7=10 (max) -> 10-2=8 -> 8-7=1 -> 1+1=2
        ///        
        /// Output: The largest value after performing all operations is 10.
        /// </summary>
        /// <param name="size">The number of elements in the input array</param>
        /// <param name="operations">The operations we want to apply to the input array</param>
        /// <returns>The maximum value in the resultant array after applying all operations</returns>
        public static long GetMaxValueBruteForce(int size, int[,] operations)
        {
            // Determine the number of operations.
            var opCount = operations.GetUpperBound(0) + 1;

            // The resultant array initialized with 0s.
            var a = new long[size];

            // Add all values and put them into an array.
            var max = long.MinValue;
            for (var i = 0; i < opCount; ++i)
            {
                var startIndex = operations[i, 0] - 1;
                var stopIndex = operations[i, 1] - 1;
                var val = operations[i, 2];

                for (var j = startIndex; j <= stopIndex; ++j)
                {
                    a[j] += val;

                    // Keep the running max sum.
                    max = Math.Max(max, a[j]);
                }
            }

            return max;
        }
    }
}
