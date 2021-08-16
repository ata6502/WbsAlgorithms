using System;

namespace WbsAlgorithms.Arithmetics
{
    public class FibonacciSequence
    {
        /// <summary>
        /// [Sedgewick] 1.1.19 p.57 - Compute the Fibonacci sequence by saving computed values 
        /// in an array.
        /// 
        /// Examples:
        /// ComputeSequenceUsingArray(5) returns { 0 1 1 2 3 5 }
        /// ComputeSequenceUsingArray(13) returns { 0 1 1 2 3 5 8 13 21 34 55 89 144 233 }
        /// </summary>
        /// <param name="n">An input number from 0 to 100 for which to compute the Fibonacci sequence</param>
        /// <returns>The Fibonacci sequence for numbers from 0 to n</returns>
        public static long[] ComputeSequenceUsingArray(int n)
        {
            const int MaxNumber = 100;

            if (n > MaxNumber)
                throw new ArgumentOutOfRangeException($"The input number {n} is too large for this implementation of Fibonacci sequence.");

            if (n == 0)
                return new long[] { 0 };
            if (n == 1)
                return new long[] { 0, 1 };

            var a = new long[n+1];
            a[0] = 0;
            a[1] = 1;

            for (var i = 2; i < a.Length; ++i)
                a[i] = a[i - 1] + a[i - 2];

            return a;
        }
    }
}
