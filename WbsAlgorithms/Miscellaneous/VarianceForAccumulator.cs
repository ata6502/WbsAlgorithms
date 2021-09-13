using System;

namespace WbsAlgorithms.Miscellaneous
{
    /// <summary>
    /// [Sedgewick] 1.2.18 p.118 - Validate that the properties Variance and StdDev
    /// compute the mean and variance of the numbers provided to the method AddValue.
    /// </summary>
    public class VarianceForAccumulator
    {
        private double _mean;
        private double _sum;
        private int _count;

        /// <summary>
        /// Adds a value to the accumulator. This implementation is less susceptible
        /// to roundoff error than the implementation based on saving the sum of
        /// the squares of the numbers.
        /// </summary>
        /// <param name="val">A value to add to the accumulator</param>
        public void AddValue(double val)
        {
            ++_count;
            _sum += 1.0 * (_count - 1) / _count * (val - _mean) * (val - _mean);
            _mean += (val - _mean) / _count;
        }

        /// <summary>
        /// Returns the mean of the numbers provided in the AddValue method.
        /// </summary>
        public double Mean => _mean;

        /// <summary>
        /// Returns the variance of the numbers provided in the AddValue method.
        /// </summary>
        public double Variance => _count > 1 ? _sum / (_count - 1) : 0;

        /// <summary>
        /// Returns the standard deviation of the numbers provided in the AddValue method.
        /// </summary>
        public double StdDev => Math.Sqrt(Variance);
    }
}
