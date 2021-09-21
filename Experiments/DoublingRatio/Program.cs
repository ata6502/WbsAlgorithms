using System;
using System.Diagnostics;

namespace DoublingRatio
{
    class Program
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());
        private static Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// [Sedgewick] p.173, p.192-193 - Doubling ratio experiments.
        /// 
        /// The doubling ratio experiment generates a sequence of random arrays, doubling the array size at each step. 
        /// Each array serves as an input to one of the procedures: RunThreeSum, RunTwoSum, or RunTwoEqual. 
        /// The program measures the running times for each input size. It also calculates the ratio of each running 
        /// time with the previous time.
        ///
        /// A doubling ratio experiment can be used to determine the approximate order of growth of the running time 
        /// of a program. The ratios approach a limit 2^b where b is a positive integer. We can conclude from that that 
        /// the order of growth of the running time is approximately N^b where N is the size of the input array. 
        /// To predict running times, multiply the last observed running time by 2^b.
        ///
        /// This test is not effective if the ratios do not approach a limiting value. If they do, we can imply the following:
        /// - The order of growth of the running time is approximately N^b.
        /// - To predict running times, multiply the last observed running time by 2^b and double N, continuing as long as desired.
        /// - If you want to predict for an input size that is not a power of 2 times N, you can adjust ratios accordingly.
        ///
        /// </summary>
        static void Main(string[] args)
        {
            const int InitialArraySize = 250;

            var arraySize = InitialArraySize;
            var elapsedTime = 0.0;
            var previousTime = 0.0;

            // Double the size of the array with each iteration.
            while (true)
            {
                elapsedTime = RunExperiment(arraySize, RunTwoEqualLinearithmic);

                // The ratio approaches a constant value.
                if (previousTime > 0.0)
                    Console.WriteLine($"Size: {arraySize}\tTime: {elapsedTime:F2} sec\t\tRatio: {elapsedTime / previousTime:F2}");

                arraySize += arraySize; // double the size of the array
                previousTime = elapsedTime;
            }
        }

        /// <summary>
        /// Runs a single experiment.
        /// </summary>
        /// <param name="arraySize">The size of an array used for this experiment</param>
        /// <param name="experiment">The experiment function</param>
        /// <returns>The elapsed time of the experiment in seconds</returns>
        private static double RunExperiment(int arraySize, Func<int[], int> experiment)
        {
            const int Max = 9999;

            // Create an array of random numbers.
            int[] a = new int[arraySize];
            for (var i = 0; i < arraySize; ++i)
                a[i] = _rnd.Next(-Max, Max + 1);

            _stopwatch.Reset();
            _stopwatch.Start();

            // Run the experiment.
            var cnt = experiment(a);

            // Return the time elapsed.
            return (_stopwatch.ElapsedMilliseconds / 1000.0);
        }

        /// <summary>
        /// Counts triples that sum to 0.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of triplets that sum to 0</returns>
        private static int RunThreeSum(int[] a)
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
        /// Counts the number pairs that are equal. Complexity O(n^2)
        /// 
        /// [Sedgewick] 1.4.8 p.209 - Determine the number pairs of values
        /// that are equal.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs of values that are equal</returns>
        private static int RunTwoEqualQuadratic(int[] a)
        {
            var len = a.Length;
            var cnt = 0;

            for (var i = 0; i < len; ++i)
                for (var j = i + 1; j < len; ++j)
                    if (a[i] == a[j])
                        ++cnt;
            return cnt;
        }

        /// <summary>
        /// Counts the number pairs that are equal. Complexity O(n ln(n))
        /// 
        /// [Sedgewick] 1.4.8 p.209 - Sort the input array to develop 
        /// a linearithmic solution.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs of values that are equal</returns>
        private static int RunTwoEqualLinearithmic(int[] a)
        {
            Array.Sort(a);

            var len = a.Length;
            var cnt = 0;

            for (var i = 0; i < len - 1; ++i)
            {
                var start = i + 1;
                while (Array.BinarySearch(a, start, len - start, a[i]) > -1)
                {
                    ++cnt;
                    ++start;
                }
            }

            return cnt;
        }
    }
}
