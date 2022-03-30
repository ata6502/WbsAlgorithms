namespace WbsAlgorithms.Miscellaneous
{
    /// <summary>
    /// Cyclic Rotation - Rotate an array to the right by a given number of steps.
    ///
    /// Given an array of N integers, rotation of the array means that each element 
    /// is shifted right by one index, and the last element of the array is moved 
    /// to the first place.
    /// 
    /// The goal is to rotate the array, K times; that is, each element should be 
    /// shifted to the right K times.
    /// 
    /// Examples:
    /// 
    /// Input:  [3, 8, 9, 7, 6], K = 1
    /// Output: [6, 3, 8, 9, 7]
    /// 
    /// Input:  [3, 8, 9, 7, 6], K = 3
    /// Output: [9, 7, 6, 3, 8]
    /// 
    /// Input:  [0, 0, 0], K = 1
    /// Output: [0, 0, 0]
    /// 
    /// Input:  [1, 2, 3, 4], K = 4
    /// Output: [1, 2, 3, 4]
    /// 
    /// [Codility] https://app.codility.com/programmers/lessons/2-arrays/cyclic_rotation/
    /// </summary>
    public class CyclicRotation
    {
        /// <summary>
        /// Rotate using an additional array. Time complexity: O(n)
        /// </summary>
        /// <param name="a">An input array</param>
        /// <param name="k">The number of steps</param>
        /// <returns>An array rotated k steps to the right</returns>
        public static int[] Rotate(int[] a, int k)
        {
            if (a.Length == 0)
                return a;

            k = k % a.Length;
            if (k == 0)
                return a;

            var b = new int[a.Length];

            for (var i = 0; i < a.Length; ++i)
            {
                var j = i + k;
                j = j < b.Length ? j : j - b.Length;
                b[j] = a[i];
            }

            return b;
        }

        /// <summary>
        /// Rotate using a brute-force approach. Time complexity: O(n^2)
        /// </summary>
        /// <param name="a">An input array</param>
        /// <param name="k">The number of steps</param>
        /// <returns>An array rotated k steps to the right</returns>
        public static int[] RotateUsingBruteForce(int[] a, int k)
        {
            if (a.Length == 0)
                return a;

            for (var i = 0; i < k; ++i)
            {
                var last = a[a.Length - 1];
                for (var j = a.Length - 1; j > 0; --j)
                    a[j] = a[j - 1];

                a[0] = last;
            }

            return a;
        }
    }
}
