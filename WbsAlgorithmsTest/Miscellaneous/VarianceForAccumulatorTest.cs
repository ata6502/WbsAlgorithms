using NUnit.Framework;
using WbsAlgorithms.Miscellaneous;

namespace WbsAlgorithmsTest.Miscellaneous
{
    [TestFixture]
    public class VarianceForAccumulatorTest
    {
        // Values of the expected mean, variance, and standard deviation calculated using MatLab.
        [TestCase(new double[] { 8 }, 8, 0, 0)]
        [TestCase(new double[] { 5, 3 }, 4, 2, 1.414213562373095)]
        [TestCase(new double[] { 3, 7, 11, 2 }, 5.75, 16.916666666666668, 4.112987559751022)]
        [TestCase(new double[] { 7, 7, 7, 7, 7 }, 7, 0, 0)]
        [TestCase(new double[] { 7, 7, 7, 7, 7, 1 }, 6, 6, 2.449489742783178)]
        [TestCase(new double[] { 3, 8.2, 9, 3.2, 7.1, 0.3, 3.2 }, 4.857142857142857, 10.512857142857142, 3.242353642472878)]
        [TestCase(new double[] { 2.0, 4.0, 5.0 }, 3.6666666666666665, 2.333333333333333, 1.5275252316519465)]
        public void MeanVarianceStdDevTest(double[] values, double expectedMean, double expectedVariance, double expectedStdDev)
        {
            const double Tolerance = 0.000000000000001;

            var acc = new VarianceForAccumulator();
            foreach (var v in values)
                acc.AddValue(v);

            Assert.AreEqual(expectedMean, acc.Mean, Tolerance);
            Assert.AreEqual(expectedVariance, acc.Variance, Tolerance);
            Assert.AreEqual(expectedStdDev, acc.StdDev, Tolerance);
        }
    }
}
