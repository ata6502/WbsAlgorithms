using System;

namespace WbsAlgorithms.Arithmetics
{
    /// <summary>
    /// [Sedgewick] 1.1.20 p.58 - Write a recursive method that computes ln(n!)
    /// By convention for an empty product: 0! = 1
    /// </summary>
    public class LogOfFactorial
    {
        public static double Compute(int n)
        {
            if (n < 0)
                throw new ArgumentException("The input value should be greater of equal zero.");

            // By the log rule: log(a·b) = log(a) + log(b)
            return ComputeRecursively(n);
        }

        private static double ComputeRecursively(int n)
        {
            if (n == 0 || n == 1)
                return Math.Log(1);

            return Math.Log(n) + ComputeRecursively(n - 1);
        }
    }
}
