using NUnit.Framework;
using WbsAlgorithms.DivideAndConquer;

namespace WbsAlgorithmsTest.DivideAndConquer
{
    [TestFixture]
    public class BinarySearchTest
    {
        [TestCase(new int[] { 1 }, 1, 0, TestName = "OneElement")]
        [TestCase(new int[] { 1, 2 }, 1, 0, TestName = "TwoElementsIndex0")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "TwoElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3 }, 1, 0, TestName = "ThreeElementsIndex0")]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, TestName = "ThreeElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 2, TestName = "ThreeElementsIndex2")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 0, TestName = "FourElementsIndex0")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, 1, TestName = "FourElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 2, TestName = "FourElementsIndex2")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, 3, TestName = "FourElementsIndex3")]
        [TestCase(new int[] { 1 }, 2, -1, TestName = "OneElementNotFound")]
        [TestCase(new int[] { 2, 3 }, 1, -1, TestName = "TwoElementsNotFound")]
        [TestCase(new int[] { 1, 2, 3 }, 4, -1, TestName = "ThreeElementsNotFound")]
        [TestCase(new int[] { 2, 3, 4, 5 }, 1, -1, TestName = "FourElementsNotFound")]
        public void FindElementIndexTest(int[] inputArray, int inputElement, int expectedIndex)
        {
            var actualIndexIteratively = BinarySearch.FindElementIteratively(inputElement, inputArray);

            Assert.AreEqual(expectedIndex, actualIndexIteratively);
        }
    }
}