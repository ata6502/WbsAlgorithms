using NUnit.Framework;
using WbsAlgorithms.Miscellaneous;

namespace WbsAlgorithmsTest.Miscellaneous
{
    [TestFixture]
    public class CyclicRotationTest
    {
        [TestCase(new int[] { 3, 8, 9, 7, 6 }, 1, new int[] { 6, 3, 8, 9, 7 })]
        [TestCase(new int[] { 3, 8, 9, 7, 6 }, 3, new int[] { 9, 7, 6, 3, 8 })]
        [TestCase(new int[] { 0, 0, 0 }, 1, new int[] { 0, 0, 0 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, new int[] { 1, 2, 3, 4 })]
        public void RotateTest(int[] inputArray, int shiftValue, int[] expectedArray)
        {
            var actualArray = CyclicRotation.Rotate(inputArray, shiftValue);
            Assert.That(actualArray, Is.EqualTo(expectedArray).AsCollection);

            var actualArrayBruteForce = CyclicRotation.RotateUsingBruteForce(inputArray, shiftValue);
            Assert.That(actualArrayBruteForce, Is.EqualTo(expectedArray).AsCollection);
        }
    }
}
