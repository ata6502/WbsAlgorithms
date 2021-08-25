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
            var actualIndexIteratively = BinarySearch.FindIndexIteratively(inputElement, inputArray);
            var actualIndexRecusively = BinarySearch.FindIndexRecursively(inputElement, inputArray);

            Assert.AreEqual(expectedIndex, actualIndexIteratively);
            Assert.AreEqual(expectedIndex, actualIndexRecusively);
        }

        [TestCase(new int[] { 1 }, 1, 0, TestName = "Rank_OneElement")]
        [TestCase(new int[] { 1, 2 }, 1, 0, TestName = "Rank_TwoDifferent1")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "Rank_TwoDifferent2")]
        [TestCase(new int[] { 1, 1 }, 1, 0, TestName = "Rank_TheSame1")]
        [TestCase(new int[] { 2, 2 }, 2, 0, TestName = "Rank_TheSame1")]
        [TestCase(new int[] { 1, 2, 3 }, 1, 0, TestName = "Rank_ThreeDifferent1")]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, TestName = "Rank_ThreeDifferent2")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 2, TestName = "Rank_ThreeDifferent3")]
        [TestCase(new int[] { 1, 1, 2 }, 1, 0, TestName = "Rank_ThreeDifferent4")]
        [TestCase(new int[] { 1, 1, 2 }, 2, 2, TestName = "Rank_ThreeDifferent5")]
        [TestCase(new int[] { 1, 2, 2 }, 1, 0, TestName = "Rank_ThreeDifferent6")]
        [TestCase(new int[] { 1, 2, 2 }, 2, 1, TestName = "Rank_ThreeDifferent7")]
        [TestCase(new int[] { 1, 1, 1 }, 1, 0, TestName = "Rank_ThreeTheSame1")]
        [TestCase(new int[] { 2, 2, 2 }, 2, 0, TestName = "Rank_ThreeTheSame2")]
        [TestCase(new int[] { 3, 3, 3 }, 3, 0, TestName = "Rank_ThreeTheSame2")]
        [TestCase(new int[] { 1, 1, 2, 3 }, 2, 2, TestName = "Rank_Four1")]
        [TestCase(new int[] { 1, 1, 2, 3 }, 3, 3, TestName = "Rank_Four2")]
        [TestCase(new int[] { 1, 1, 2 }, 8, 3, TestName = "Rank_Greater1")]
        [TestCase(new int[] { 3, 4, 5 }, 8, 3, TestName = "Rank_Greater2")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 1, 0, TestName = "Rank_More1")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 3, 1, TestName = "Rank_More2")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 5, 2, TestName = "Rank_More3")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 6, 4, TestName = "Rank_More4")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 7, 5, TestName = "Rank_More5")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 8, 5, TestName = "Rank_More6")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 9, 7, TestName = "Rank_More7")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 10, 7, TestName = "Rank_More8")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 11, 10, TestName = "Rank_More9")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 15, 10, TestName = "Rank_More10")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 20, 11, TestName = "Rank_More11")]
        public void RankTest(int[] inputArray, int inputElement, int expectedCount)
        {
            var actualCount = BinarySearch.RankRecursively(inputElement, inputArray, 0, inputArray.Length-1);

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}