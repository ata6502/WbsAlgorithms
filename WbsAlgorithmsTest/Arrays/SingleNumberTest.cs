using NUnit.Framework;
using WbsAlgorithms.Arrays;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    public class SingleNumberTest
    {
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 1, 1, 2 }, 2)]
        [TestCase(new int[] { 1, 2, 1 }, 2)]
        [TestCase(new int[] { 2, 1, 1 }, 2)]
        [TestCase(new int[] { 2, 2, 1 }, 1)]
        [TestCase(new int[] { 1, 1, 2, 2, 3 }, 3)]
        [TestCase(new int[] { 1, 2, 1, 2, 3 }, 3)]
        [TestCase(new int[] { 1, 2, 1, 3, 2 }, 3)]
        [TestCase(new int[] { 1, 2, 2, 3, 1 }, 3)]
        [TestCase(new int[] { 1, 2, 3, 1, 2 }, 3)]
        [TestCase(new int[] { 2, 1, 3, 2, 1 }, 3)]
        [TestCase(new int[] { 4, 1, 2, 1, 2 }, 4)]
        public void GetSingleNumberTest(int[] inputArray, int expectedValue)
        {
            var singleNumberDictionary = SingleNumber.GetSingleNumberUsingDictionary(inputArray);
            var singleNumberMath = SingleNumber.GetSingleNumberUsingMath(inputArray);
            var singleNumberXor = SingleNumber.GetSingleNumberUsingXor(inputArray);

            Assert.That(singleNumberDictionary, Is.EqualTo(expectedValue));
            Assert.That(singleNumberMath, Is.EqualTo(expectedValue));
            Assert.That(singleNumberXor, Is.EqualTo(expectedValue));
        }
    }
}
