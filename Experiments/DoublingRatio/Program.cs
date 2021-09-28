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
        /// Each array serves as an input to one of the procedures: RunThreeSum, RunTwoSum, RunTwoEqual, etc.
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
        /// </summary>
        static void Main(string[] args)
        {
            const int InitialArraySize = 250;

            var arraySize = InitialArraySize;
            var elapsedTime = 0.0;
            var previousTime = 0.0;
            var selectedExperiment = GetSelectedExperiment();

            if (selectedExperiment == null)
                return;

            // Double the size of the array with each iteration.
            while (true)
            {
                elapsedTime = RunExperiment(arraySize, selectedExperiment);

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
        /// Counts pairs that sum to 0.
        /// The order of growth is N^2. To predict running times, multiply the last observed running time by 2^b = 2^2 = 4
        /// 
        /// Size: 500       Time: 0.00 sec      Ratio: 0.00
        /// Size: 4000      Time: 0.00 sec      Ratio: 4.00
        /// Size: 8000      Time: 0.01 sec      Ratio: 3.75
        /// Size: 16000     Time: 0.06 sec      Ratio: 3.80
        /// Size: 32000     Time: 0.23 sec      Ratio: 4.00
        /// Size: 64000     Time: 0.92 sec      Ratio: 4.04
        /// Size: 128000    Time: 3.68 sec      Ratio: 3.99
        /// Size: 256000    Time: 14.48 sec     Ratio: 3.93
        /// Size: 512000    Time: 57.87 sec     Ratio: 4.00
        /// Size: 1024000   Time: 231.55 sec    Ratio: 4.00
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs that sum to 0</returns>
        private static int RunTwoSum(int[] a)
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
        /// Counts triples that sum to 0.
        /// The order of growth is N^3. To predict running times, multiply the last observed running time by 2^b = 2^3 = 8
        ///
        /// Size: 250   Elapsed time: 0.01 sec Ratio: 11.00
        /// Size: 500   Elapsed time: 0.08 sec Ratio: 7.18
        /// Size: 1000   Elapsed time: 0.58 sec Ratio: 7.35
        /// Size: 2000   Elapsed time: 4.43 sec Ratio: 7.62
        /// Size: 4000   Elapsed time: 33.62 sec Ratio: 7.59
        /// Size: 8000   Elapsed time: 269.00 sec Ratio: 8.00
        /// Size: 16000   Elapsed time: 2144.69 sec Ratio: 7.97
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
        /// Counts quadruplets that sum to 0. Complexity O(n^4); ratio 2^4 = 16
        /// The order of growth is N^4. To predict running times, multiply the last observed running time by 2^b = 2^4 = 16
        /// 
        /// Size: 250   Elapsed time: 0.73 sec Ratio: 13.70
        /// Size: 500   Elapsed time: 10.27 sec Ratio: 14.14
        /// Size: 1000   Elapsed time: 157.90 sec Ratio: 15.38
        /// Size: 2000   Elapsed time: 2461.19 sec Ratio: 15.59
        /// 
        /// [Sedgewick] 1.4.14 p.210 - Develop an algorithm for the 4-sum problem.
        /// For now, just a brute-force approach.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of quadruplets that sum to 0</returns>
        private static int RunFourSum(int[] a)
        {
            var len = a.Length;
            var cnt = 0;

            for (var i = 0; i < len; ++i)
                for (var j = i + 1; j < len; ++j)
                    for (var k = j + 1; k < len; ++k)
                        for (var l = k + 1; l < len; ++l)
                            if (a[i] + a[j] + a[k] + a[l] == 0)
                                ++cnt;
            return cnt;
        }

        /// <summary>
        /// Counts the number pairs that are equal. Complexity O(n^2)
        /// The order of growth is N^2. To predict running times, multiply the last observed running time by 2^b = 2^2 = 4
        /// 
        /// Size: 250   Elapsed time: 0.00 sec Ratio: NaN
        /// Size: 500   Elapsed time: 0.00 sec Ratio: NaN
        /// Size: 1000   Elapsed time: 0.00 sec Ratio: 8
        /// Size: 2000   Elapsed time: 0.01 sec Ratio: 4.00
        /// Size: 4000   Elapsed time: 0.03 sec Ratio: 4.25
        /// Size: 8000   Elapsed time: 0.10 sec Ratio: 3.06
        /// Size: 16000   Elapsed time: 0.40 sec Ratio: 3.87
        /// Size: 32000   Elapsed time: 1.42 sec Ratio: 3.53
        /// Size: 64000   Elapsed time: 5.54 sec Ratio: 3.90
        /// Size: 128000   Elapsed time: 22.49 sec Ratio: 4.06
        /// Size: 256000   Elapsed time: 90.50 sec Ratio: 4.02
        /// Size: 512000   Elapsed time: 362.00 sec Ratio: 4.00 
        /// 
        /// [Sedgewick] 1.4.8 p.209 - Determine the number pairs of values that are equal.
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
        /// [Sedgewick] 1.4.8 p.209 - Sort the input array to develop a linearithmic solution.
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

        private static Func<int[], int> GetSelectedExperiment()
        {
            Console.WriteLine();
            Console.WriteLine("Select a doubling experiment");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] - TwoSum");
            Console.WriteLine("[2] - ThreeSum");
            Console.WriteLine("[3] - FourSum");
            Console.WriteLine("[4] - TwoEqual (quadratic)");
            Console.WriteLine("[5] - TwoEqual (linearithmic)");
            var key = Console.ReadKey(true);

            switch(key.KeyChar)
            {
                case '1':
                    Console.WriteLine("\nRunning TwoSum experiment...");
                    return RunTwoSum;
                case '2':
                    Console.WriteLine("\nRunning ThreeSum experiment...");
                    return RunThreeSum;
                case '3':
                    Console.WriteLine("\nRunning FourSum experiment...");
                    return RunFourSum;
                case '4':
                    Console.WriteLine("\nRunning TwoEqual (quadratic) experiment...");
                    return RunTwoEqualQuadratic;
                case '5':
                    Console.WriteLine("\nRunning TwoEqual (linearithmic) experiment...");
                    return RunTwoEqualLinearithmic;
                default:
                    Console.WriteLine("\nIncorrect selection.");
                    return null;
            }
        }
    }
}
