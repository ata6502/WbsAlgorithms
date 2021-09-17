using System;

namespace Shuffling
{
    class Program
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// [Sedgewick] 1.1.36 p.61 - Empirical shuffle check
        /// [Sedgewick] 1.1.37 p.61 - Bad shuffling
        /// 
        /// Run computional experiments to check that the Shuffle method randomly shuffles
        /// the elements in an array.
        /// 
        /// Print an M-by-M table such that row i gives the number of times i wound up
        /// in position j. If the shuffling is truly random, all values in the resulting
        /// table should be close to N/M.        
        /// </summary>
        static void Main(string[] args)
        {
            const int M = 10;    // the size of an array that will be shuffled
            const int N = 10000; // the number of shuffles

            ShuffleAndShowResults(M, N, Shuffle, "Shuffling");
            ShuffleAndShowResults(M, N, BadShuffle, "Bad Shuffling");
        }

        private static void ShuffleAndShowResults(int M, int N, Action<int[]> shuffle, string title)
        {
            var a = new int[M]; // an array to be shuffled
            var pos = new int[M, M]; // the M-by-M table showing the result

            // Shuffle the array N times.
            for (var n = 0; n < N; ++n)
            {
                // Initialize the array before each shuffle.
                for (var i = 0; i < a.Length; ++i)
                    a[i] = i;

                shuffle(a);

                // Count the number of times i wound up in position j.
                for (var j = 0; j < a.Length; ++j)
                    pos[a[j], j]++;  // a[j] is the number i
            }

            // The min and max variables are the min and max values in the resulting table.
            var min = N;
            var max = -1;

            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));

            // Display results in an M-by-M table (indexed [i][j]) where each row i shows
            // the number of times i wound up in position j.
            for (var i = 0; i < M; ++i)
            {
                for (var j = 0; j < M; ++j)
                {
                    Console.Write("{0,6:D} ", pos[i, j]);

                    min = Math.Min(pos[i, j], min);
                    max = Math.Max(pos[i, j], max);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"The values in the table are close to {N / M} \u00B1 {(max - min) / 2}\n");
        }

        /// <summary>
        /// Randomly shuffles elements in an array.
        /// </summary>
        /// <param name="a">The input array</param>
        private static void Shuffle(int[] a)
        {
            var len = a.Length;
            for (var i = 0; i < len; ++i)
            {
                // Exchange a[i] with a random element in a[i..len-1]
                var r = _rnd.Next(i, len);
                var tmp = a[i];
                a[i] = a[r];
                a[r] = tmp;
            }
        }

        /// <summary>
        /// BadShuffle chooses a random integer in a[0..len-1] rather than a[i..len-1] 
        /// The resulting order is not equally likely to be one of the M! possibilities.
        /// </summary>
        /// <param name="a"></param>
        private static void BadShuffle(int[] a)
        {
            var len = a.Length;
            for (var i = 0; i < len; ++i)
            {
                // Exchange a[i] with a random element in a[0..len-1]
                var r = _rnd.Next(0, len); // bad shuffling!
                var tmp = a[i];
                a[i] = a[r];
                a[r] = tmp;
            }
        }
    }
}
