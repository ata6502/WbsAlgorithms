using NUnit.Framework;
using WbsAlgorithms.Miscellaneous;

namespace WbsAlgorithmsTest.Miscellaneous
{
    [TestFixture]
    public class AccumulatorVarianceTest
    {
        // Values of the expected mean, variance, and standard deviation calculated using MatLab.
        [TestCase(new double[] { 8 }, 8, 0, 0)]
        [TestCase(new double[] { 5, 3 }, 4, 2, 1.414213562373095)]
        [TestCase(new double[] { 3, 7, 11, 2 }, 5.75, 16.916666666666668, 4.112987559751022)]
        [TestCase(new double[] { 7, 7, 7, 7, 7 }, 7, 0, 0)]
        [TestCase(new double[] { 7, 7, 7, 7, 7, 1 }, 6, 6, 2.449489742783178)]
        [TestCase(new double[] { 3, 8.2, 9, 3.2, 7.1, 0.3, 3.2 }, 4.857142857142857, 10.512857142857142, 3.242353642472878)]
        [TestCase(new double[] { 2.0, 4.0, 5.0 }, 3.6666666666666665, 2.333333333333333, 1.5275252316519465)]
        [TestCase(new double[] { 2, 4, 5 }, 3.6666666666666665, 2.333333333333333, 1.5275252316519465)]
        [TestCase(new double[] { 5, 2, 6, 2, 2, 1, 2, 3, 6, 1 }, 3.0, 3.7777777777777777, 1.9436506316151001)]
        public void MeanVarianceStdDevTest(double[] values, double expectedMean, double expectedVariance, double expectedStdDev)
        {
            const double Tolerance = 0.000000000000001;

            var acc = new AccumulatorVariance();
            foreach (var v in values)
                acc.AddValue(v);

            Assert.That(acc.Mean, Is.EqualTo(expectedMean).Within(Tolerance));
            Assert.That(acc.Variance, Is.EqualTo(expectedVariance).Within(Tolerance));
            Assert.That(acc.StdDev, Is.EqualTo(expectedStdDev).Within(Tolerance));
        }
    }
}
