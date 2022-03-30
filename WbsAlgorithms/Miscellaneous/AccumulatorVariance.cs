using System;

namespace WbsAlgorithms.Miscellaneous
{
    /// <summary>
    /// [Sedgewick] 1.2.18 p.118 - Validate that the properties Variance and StdDev
    /// compute the mean and variance of the numbers provided to the method AddValue.
    /// </summary>
    public class AccumulatorVariance
    {
        private double _mean;
        private double _sum; // the numerator of variance
        private int _count;  // the number of values i.e., the accumulator's size

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
        /// The mean (expected value) of a discrete random variable is 
        /// the probability-weighted average of all possible values.
        /// </summary>
        public double Mean => _mean;

        /// <summary>
        /// Returns the variance of the numbers provided in the AddValue method.
        /// Variance is always non-negative: a small variance indicates that the data points 
        /// tend to be very close to the mean (expected value) and hence to each other, while 
        /// a high variance indicates that the data points are very spread out around the mean 
        /// and from each other.
        /// </summary>
        public double Variance => _count > 1 ? _sum / (_count - 1) : 0;

        /// <summary>
        /// Returns the standard deviation of the numbers provided in the AddValue method.
        /// The standard deviation is used to quantify the amount of variation of a set of data values.
        /// A standard deviation close to 0 indicates that the data points tend to be very close 
        /// to the mean (expected value) of the set, while a high standard deviation indicates 
        /// that the data points are spread out over a wider range of values.
        /// </summary>
        public double StdDev => Math.Sqrt(Variance);
    }
}
