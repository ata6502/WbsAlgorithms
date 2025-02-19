using NUnit.Framework;
using WbsAlgorithms.Sorting;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class SortThreeNumbersTest
    {
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 }, TestName = "Test123")]
        [TestCase(new int[] { 1, 3, 2 }, new int[] { 1, 2, 3 }, TestName = "Test132")]
        [TestCase(new int[] { 2, 1, 3 }, new int[] { 1, 2, 3 }, TestName = "Test213")]
        [TestCase(new int[] { 2, 3, 1 }, new int[] { 1, 2, 3 }, TestName = "Test231")]
        [TestCase(new int[] { 3, 1, 2 }, new int[] { 1, 2, 3 }, TestName = "Test312")]
        [TestCase(new int[] { 3, 2, 1 }, new int[] { 1, 2, 3 }, TestName = "Test321")]
        public void SortAscendingTest(int[] inputArray, int[] expectedResult)
        {
            // Sorts the input array in-place.
            SortThreeNumbers.SortAscending(inputArray);

            Assert.That(inputArray, Is.EqualTo(expectedResult).AsCollection);
        }
    }
}
