using NUnit.Framework;
using WbsAlgorithms.Arithmetics;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class LargestNumberTest
    {
        [TestCase(new int[] { 3, 30, 34, 5, 9 }, "9534330")]
        [TestCase(new int[] { 0 }, "0")]
        [TestCase(new int[] { 0, 0, 0 }, "0")]
        [TestCase(new int[] { 0, 1, 0 }, "100")]
        [TestCase(new int[] { 1, 0, 2 }, "210")]
        [TestCase(new int[] { 21, 0, 23 }, "23210")]
        [TestCase(new int[] { 231, 10, 5, 60, 6, 1 }, "6605231110")]
        public void GetLargestNumberTest(int[] inputArray, string expectedLargestNumber)
        {
            var actualLargestNumber1 = LargestNumber.GetLargestNumber1(inputArray);
            Assert.AreEqual(expectedLargestNumber, actualLargestNumber1);

            var actualLargestNumber2 = LargestNumber.GetLargestNumber2(inputArray);
            Assert.AreEqual(expectedLargestNumber, actualLargestNumber2);
        }
    }
}
