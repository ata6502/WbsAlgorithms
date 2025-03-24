using NUnit.Framework;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    public class MaximumAbsoluteDifferenceTest
    {
        [TestCase(new int[] { 1, 3, -1 }, 5)]
        [TestCase(new int[] { 3, -2, 5, -4 }, 10)]
        [TestCase(new int[] { -70, -64, -6, -56, 64, 61, -57, 16, 48, -98 }, 167)]
        [TestCase(new int[] { -4, 5, 71, -45, 1, 0, -78, -77, 45, 120, -3, 67, 2, -34 }, 201)]
        public void GetDifferenceTest(int[] inputArray, int expectedValue)
        {
            var differenceBruteForce = MaximumAbsoluteDifference.GetDifferenceBruteForce(inputArray);
            var difference = MaximumAbsoluteDifference.GetDifference(inputArray);

            Assert.That(differenceBruteForce, Is.EqualTo(expectedValue));
            Assert.That(difference, Is.EqualTo(expectedValue));
        }
    }
}
