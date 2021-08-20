using NUnit.Framework;
using WbsAlgorithms.Miscellaneous;

namespace WbsAlgorithmsTest.Miscellaneous
{
    [TestFixture]
    public class BinomialDistributionTest
    {
        private const double Delta = 0.0000000001;

        [TestCase(5, 3, 0.5, 0.3125, 93, 26, false)]
        [TestCase(6, 2, 0.7, 0.059535, 97, 26, false)]
        [TestCase(5, 2, 0.25, 0.263671875, 63, 22, false)]
        [TestCase(10, 5, 0.25, 0.0583992004, 2467, 66, false)]
        [TestCase(15, 7, 0.25, 0.0393204717, 65535, 122, false)]
        [TestCase(20, 10, 0.25, 0.0099222753, 2433071, 206, false)]
        [TestCase(30, 15, 0.25, 0.0019305450, 0, 421, true)]
        [TestCase(50, 25, 0.25, 0.0000844919, 0, 1076, true)]
        [TestCase(100, 50, 0.25, 0.0000000451, 0, 4026, true)]
        public void CalculateProbabilityTest(int n, int k, double p, double expectedProbability, int extectedCounter1, int extectedCounter2, bool skipComputeMethod)
        {
            var binomial = new BinomialDistribution();

            // Some calculations would take too long using the Compute method. We skip this test cases.
            if (!skipComputeMethod)
            {
                var result1 = binomial.Compute(n, k, p);

                Assert.AreEqual(expectedProbability, result1.Probability, Delta);
                Assert.AreEqual(extectedCounter1, result1.Counter);
            }

            var result2 = binomial.ComputeWithMemory(n, k, p);

            Assert.AreEqual(expectedProbability, result2.Probability, Delta);
            Assert.AreEqual(extectedCounter2, result2.Counter);
        }
    }
}
