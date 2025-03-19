using NUnit.Framework;
using WbsAlgorithms.Arrays;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    internal class UnimodalArrayTest
    {
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 1, 2 }, 2)]
        [TestCase(new int[] { 2, 1 }, 2)]
        [TestCase(new int[] { 1, 2, 1 }, 2)]
        [TestCase(new int[] { 1, 5, 2 }, 5)]
        [TestCase(new int[] { 1, 3, 5, 2 }, 5)]
        [TestCase(new int[] { 1, 5, 4, 2 }, 5)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 5)]
        [TestCase(new int[] { 1, 5, 4, 2, 0 }, 5)]
        [TestCase(new int[] { 1, 2, 3, 5, 4 }, 5)]
        [TestCase(new int[] { 5, 4, 3, 2, 1 }, 5)]
        [TestCase(new int[] { 1, 3, 7, 8, 12, 10, 5, 2 }, 12)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 6, 3, 2, 1 }, 12)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 15, 17, 18, 22, 26, 34, 56, 76, 65, 55, 53, 51, 49, 44, 40, 3, -4, -9, -10, -23, -90 }, 76)]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }, 32)]
        [TestCase(new int[] { 32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, 32)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 23)]
        [TestCase(new int[] { 6, 8, 12, 9, 4, 1, -5, -6, -10, -23, -56, -78, -90, -91 }, 12)]
        [TestCase(new int[] { 1, 3, 7, 14, 15, 17, 20, 25, 29, -7 }, 29)]
        [TestCase(new int[] { 1, 3, 7, 14, 15, 17, 20, 19, 10, -5 }, 20)]
        public void FindMaxTest(int[] unimodalArray, int expectedMax)
        {
            var max = UnimodalArray.FindMax(unimodalArray);
            Assert.That(max, Is.EqualTo(expectedMax));

            var maxLinear = UnimodalArray.FindMaxLinear(unimodalArray);
            Assert.That(maxLinear, Is.EqualTo(expectedMax));
        }
    }
}
