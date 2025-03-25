using NUnit.Framework;
using WbsAlgorithms.Arrays;

namespace WbsAlgorithmsTest.Arrays
{
    [TestFixture]
    public class OddOccurrencesInArrayTest
    {
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 1, 2, 1 }, 2)]
        [TestCase(new int[] { 1, 1, 2 }, 2)]
        [TestCase(new int[] { 2, 1, 1 }, 2)]
        [TestCase(new int[] { 1, 1, 2, 2, 1, 2, 1, 2, 2, 1, 1 }, 2)]
        [TestCase(new int[] { 9, 3, 9, 3, 9, 7, 9 }, 7)]
        [TestCase(new int[] { 9, 3, 9, 3, 9, 7, 9, 1, 2, 3, 2, 1, 7, 3, 7, 4, 6, 3, 6, 7, 4, 2, 3 }, 2)]
        public void GetOddNumberTest(int[] inputArray, int expectedValue)
        {
            var result = OddOccurrencesInArray.GetOddNumber(inputArray);
            Assert.That(result, Is.EqualTo(expectedValue));
        }
    }
}
