using NUnit.Framework;
using WbsAlgorithms.Arrays;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    public class MaximumSumTest
    {
        [TestCase(new int[] { 1, -3, -2, 5 }, 5)]
        [TestCase(new int[] { 1, -3, -2, -2, 3, -1, 1, 5 }, 8)]
        [TestCase(new int[] { 1, 2, 3, 4, -10 }, 10)]
        [TestCase(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6)]
        [TestCase(new int[] { 1, -3, 2, -5, 7, 6, -1, -4, 11, -23 }, 19)]
        [TestCase(new int[] { -2, -3, -6, -12, -1, -52 }, -1)]
        [TestCase(new int[] { 1, 3, 2, 5 }, 11)]
        [TestCase(new int[] { 3, -2, 5, -1 }, 6)]
        public void GetMaxSumOfContiguousSubarrayTest(int[] inputArray, int expectedValue)
        {
            var resultBruteForce = MaximumSum.GetMaxSumOfContiguousSubarrayBruteForce(inputArray);
            var resultRecursive = MaximumSum.GetMaxSumOfContiguousSubarray(inputArray);

            Assert.That(resultBruteForce, Is.EqualTo(expectedValue));
            Assert.That(resultRecursive, Is.EqualTo(expectedValue));
        }
    }
}
