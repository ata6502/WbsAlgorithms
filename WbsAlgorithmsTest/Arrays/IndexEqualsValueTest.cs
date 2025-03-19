using NUnit.Framework;
using WbsAlgorithms.Arrays;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    internal class IndexEqualsValueTest
    {
        [TestCase(new int[] { 0 }, 0)]
        [TestCase(new int[] { 1 }, -1)]
        [TestCase(new int[] { -2, 1 }, 1)]
        [TestCase(new int[] { -1, 0, 2, 7, 9 }, 2)]
        [TestCase(new int[] { -7, -1, 0, 3, 7 }, 3)]
        [TestCase(new int[] { -1, 1, 3, 6, 9 }, 1)]
        [TestCase(new int[] { -5, 1, 6 }, 1)]
        [TestCase(new int[] { -10, -8, -4, 0, 2, 5, 16, 17, 18 }, 5)]
        [TestCase(new int[] { -5, 11, 12, 13, 14, 15, 16, 17, 18 }, -1)]
        [TestCase(new int[] { -8, -7, -6, -5, -4 }, -1)]
        [TestCase(new int[] { -5, -3, 1, 6, 8 }, -1)]
        [TestCase(new int[] { -2, -1, 0, 4, 5 }, -1)]
        [TestCase(new int[] { 0, 2, 3, 4, 5 }, 0)]
        [TestCase(new int[] { 0, 3, 4, 5 }, 0)]
        [TestCase(new int[] { -1, 1, 3 }, 1)]
        [TestCase(new int[] { -2, -1, 0, 3 }, 3)]
        [TestCase(new int[] { -3, -2, 2, 5 }, 2)]
        [TestCase(new int[] { -1, 0, 2, 4, 5 }, 2)]
        [TestCase(new int[] { 0, 4, 8, 9, 11 }, 0)]
        [TestCase(new int[] { -2, -1, 2, 5, 8 }, 2)]
        [TestCase(new int[] { -2, -1, 0, 3, 5 }, 3)]
        [TestCase(new int[] { -2, -1, 0, 1, 4 }, 4)]
        [TestCase(new int[] { -5, -2, 0, 1, 2, 3, 5, 7, 10 }, 7)]
        [TestCase(new int[] { -8, -6, -4, -2, 0, 1, 2, 5, 8 }, 8)]
        public void FindIndexTest(int[] a, int expectedValue)
        {
            var valueBruteForce = IndexEqualsValue.FindIndexBruteForce(a);
            var valueRecursive = IndexEqualsValue.FindIndexRecursive(a, 0, a.Length - 1);

            Assert.That(valueBruteForce, Is.EqualTo(expectedValue));
            Assert.That(valueRecursive, Is.EqualTo(expectedValue));
        }
    }
}
