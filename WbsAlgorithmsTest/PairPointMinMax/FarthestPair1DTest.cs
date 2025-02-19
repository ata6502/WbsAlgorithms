using NUnit.Framework;
using WbsAlgorithms.Common;
using WbsAlgorithms.PairPointMinMax;

namespace WbsAlgorithmsTest.PairPointMinMax
{
    [TestFixture]
    public class FarthestPair1DTest
    {
        [TestCase(new double[] { 1, 1 }, 1, 1)]
        [TestCase(new double[] { 1, 5, 7 }, 1, 7)]
        [TestCase(new double[] { -20, 4, 1, 5, -67.8, 5.1, 7, 7.2 }, -67.8, 7.2)]
        [TestCase(new double[] { -12, -11, -5, 0.1, 3.4, 5, 8 }, -12, 8)]
        [TestCase(new double[] { 1, 60, 50, -15, -31, 20, 70, 100 }, -31, 100)]
        [TestCase(new double[] { 3, 8, 9, -10, 8, -11, 3 }, -11, 9)]
        [TestCase(new double[] { -30, 50, 2, 3 }, -30, 50)]
        [TestCase(new double[] { 2, 1, 9, 7 }, 1, 9)]
        [TestCase(new double[] { -5.2, 9.4, 20, -10, 21.1, 40, 50, -20 }, -20, 50)]
        [TestCase(new double[] { -4, -3, 0, 10, 20 }, -4, 20)]
        [TestCase(new double[] { -10, -3, 0, 2, 4, 20 }, -10, 20)]
        public void FindPairTest(double[] inputArray, double expectedX, double expectedY)
        {
            var pairExpected = new Point(expectedX, expectedY);
            var pairActual = FarthestPair1D.FindPair(inputArray);
            Assert.That(pairActual, Is.EqualTo(pairExpected));
        }
    }
}
