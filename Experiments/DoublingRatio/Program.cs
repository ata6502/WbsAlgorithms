using System;
using System.Collections.Generic;
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
            const long InitialArraySize = 250;

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
        private static double RunExperiment(long arraySize, Func<int[], int> runExperiment)
        {
            const int Max = 9999;

            // Create an array of random numbers.
            int[] a = new int[arraySize];
            for (long i = 0; i < arraySize; ++i)
                a[i] = _rnd.Next(-Max, Max + 1);

            _stopwatch.Reset();
            _stopwatch.Start();

            // Run the experiment.
            var cnt = runExperiment(a);

            // Return the time elapsed.
            return (_stopwatch.ElapsedMilliseconds / 1000.0);
        }

        /// <summary>
        /// Counts pairs that sum to 0. Complexity O(n^2)
        /// The order of growth is N^2. To predict running times, multiply the last observed running time by 2^b = 2^2 = 4
        /// 
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
        private static int RunTwoSumQuadratic(int[] a)
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
        /// Counts pairs that sum to 0. Complexity O(n*ln(n)) because of sorting.
        /// 
        /// Size: 128000    Time: 0.01 sec      Ratio: 2.00
        /// Size: 256000    Time: 0.01 sec      Ratio: 2.00
        /// Size: 512000    Time: 0.02 sec      Ratio: 2.00
        /// Size: 1024000   Time: 0.05 sec      Ratio: 2.00
        /// Size: 2048000   Time: 0.10 sec      Ratio: 2.06
        /// Size: 4096000   Time: 0.20 sec      Ratio: 1.97
        /// Size: 8192000   Time: 0.39 sec      Ratio: 2.01
        /// Size: 16384000  Time: 0.79 sec      Ratio: 2.03
        /// Size: 32768000  Time: 1.61 sec      Ratio: 2.03
        /// Size: 65536000  Time: 3.21 sec      Ratio: 1.99
        /// Size: 131072000 Time: 6.51 sec      Ratio: 2.03
        /// Size: 262144000 Time: 13.05 sec     Ratio: 2.00
        /// Size: 524288000 Time: 26.49 sec     Ratio: 2.03
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs that sum to zero. The input array remains sorted.</returns>
        private static int RunTwoSumLinearithmic(int[] a)
        {
            Array.Sort(a);
            return CountPairs(a);
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
        private static int RunThreeSumCubic(int[] a)
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
        /// Counts triplets that sum to 0. Complexity O(n^2).
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of triplets that sum to zero. The input array remains sorted.</returns>
        private static int RunThreeSumQuadratic(int[] a)
        {
            Array.Sort(a);
            return CountTriplets(a);
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
        /// An example of an experiment run:
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
        /// The method uses the following formula to calculate the number of pairs 
        /// in a given set: cnt = (n - 1) * n / 2, where n is the number of elements 
        /// in the set.
        /// 
        /// n  cnt - the number of possible pairs
        /// -  ---
        /// 1  0
        /// 2  1
        /// 3  3
        /// 4  6
        /// 5  10 
        /// etc.
        /// 
        /// [Sedgewick] 1.4.8 p.209 - Develop a linearithmic solution on a sorted array.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs of values that are equal. The input array remains sorted.</returns>
        private static int RunTwoEqualLinearithmic(int[] a)
        {
            Array.Sort(a);

            var len = a.Length;
            var num = 1; // the number of the equal consecutive numbers
            var cnt = 0; // the number of pairs of values that are equal

            for (var i = 1; i < len; ++i)
            {
                if (a[i] == a[i - 1])
                    ++num;
                else if (num > 1)
                {
                    cnt += (num - 1) * num / 2;
                    num = 1;
                }
            }

            // Adjust the last sequence.
            if (num > 1)
                cnt += (num - 1) * num / 2;

            return cnt;
        }

        /// <summary>
        /// Counts the number pairs that are equal. Complexity O(n)
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs of values that are equal. The input array remains sorted.</returns>
        private static int RunTwoEqualLinear(int[] a)
        {
            var vals = new Dictionary<int, int>();

            var len = a.Length;
            var cnt = 0; // the number of pairs of values that are equal

            // Collect distinct values and their frequencies i.e., create a histogram.
            for (var i = 0; i < len; ++i)
            {
                if (!vals.TryGetValue(a[i], out _))
                    vals.Add(a[i], 0);
                vals[a[i]]++;
            }

            // Loop over the histogram and calculate the number of pairs.
            foreach (var v in vals)
            {
                if (vals.TryGetValue(v.Key, out var num))
                    cnt += (num - 1) * num / 2;
            }

            return cnt;
        }

        #region Algorithms
        /// <summary>
        /// Counts pairs that sum to zero. Complexity O(n)
        /// The input array needs to be sorted.
        /// 
        /// This algorithm is identical to WbsAlgorithms.Arithmetics.ZeroSum.CountPairsLinear
        /// Refer to CountPairsLinear for description and more comments.
        /// </summary>
        /// <param name="a">An array of integers</param>
        /// <returns>The number of pairs that sum to zero.</returns>
        private static int CountPairs(int[] a)
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
        /// Counts triples that sum to zero. Complexity O(n^2)
        /// The input array needs to be sorted.
        /// 
        /// This algorithm is identical to WbsAlgorithms.Arithmetics.ZeroSum.CountTripletsQuadratic
        /// Refer to CountTripletsQuadratic for description and more comments.
        /// </summary>
        /// <param name="a">A sorted array of integers</param>
        /// <returns>The number of pairs that sum to zero</returns>
        private static int CountTriplets(int[] a)
        {
            var cnt = 0;

            for (var k = 0; k < a.Length; ++k)
            {
                var i = k + 1;
                var j = a.Length - 1;

                while (i < j)
                {
                    if (a[i] == 0 && a[j] == 0)
                    {
                        // Stop the while loop.
                        j = 0;
                        i = 1;
                    }
                    else if (a[i] + a[j] + a[k] == 0)
                    {
                        if (a[i] == a[j])
                        {
                            var n = j - i + 1;
                            cnt += n * (n - 1) / 2;

                            // Stop the while loop.
                            j = 0;
                            i = 1;
                        }
                        else
                        {
                            var start_i = i;
                            var start_j = j;

                            while (a[i] + a[start_j] + a[k] == 0 && i < start_j)
                                ++i;

                            while (a[start_i] + a[j] + a[k] == 0 && j > start_i)
                                --j;

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
            var n = 0;
            var i = 0;
            while (i < a.Length && a[i] <= 0)
            {
                if (a[i] == 0)
                    ++n;
                ++i;
            }

            return (n - 2) * (n - 1) * n / 6;
        }
        #endregion

        private static Func<int[], int> GetSelectedExperiment()
        {
            Console.WriteLine();
            Console.WriteLine("Select a doubling experiment");
            Console.WriteLine("----------------------------");
            Console.WriteLine("[1] - TwoSum (quadratic)");
            Console.WriteLine("[2] - TwoSum (linearithmic)");
            Console.WriteLine("[3] - ThreeSum (cubic)");
            Console.WriteLine("[4] - ThreeSum (quadratic)");
            Console.WriteLine("[5] - FourSum");
            Console.WriteLine("[6] - TwoEqual (quadratic)");
            Console.WriteLine("[7] - TwoEqual (linearithmic)");
            Console.WriteLine("[8] - TwoEqual (linear)");
            var key = Console.ReadKey(true);

            switch(key.KeyChar)
            {
                case '1':
                    Console.WriteLine("\nRunning TwoSum (quadratic) experiment...");
                    return RunTwoSumQuadratic;
                case '2':
                    Console.WriteLine("\nRunning TwoSum (linearithmic) experiment...");
                    return RunTwoSumLinearithmic;
                case '3':
                    Console.WriteLine("\nRunning ThreeSum (cubic) experiment...");
                    return RunThreeSumCubic;
                case '4':
                    Console.WriteLine("\nRunning ThreeSum (quadratic) experiment...");
                    return RunThreeSumQuadratic;
                case '5':
                    Console.WriteLine("\nRunning FourSum experiment...");
                    return RunFourSum;
                case '6':
                    Console.WriteLine("\nRunning TwoEqual (quadratic) experiment...");
                    return RunTwoEqualQuadratic;
                case '7':
                    Console.WriteLine("\nRunning TwoEqual (linearithmic) experiment...");
                    return RunTwoEqualLinearithmic;
                case '8':
                    Console.WriteLine("\nRunning TwoEqual (linear) experiment...");
                    return RunTwoEqualLinear;
                default:
                    Console.WriteLine("\nIncorrect selection.");
                    return null;
            }
        }
    }
}
