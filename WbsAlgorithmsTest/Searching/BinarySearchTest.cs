using NUnit.Framework;
using System;
using WbsAlgorithms.Searching;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Searching
{
    [TestFixture]
    public class BinarySearchTest
    {
        [TestCase(new int[] { 1 }, 1, 0, TestName = "Search_OneElement")]
        [TestCase(new int[] { 1, 2 }, 1, 0, TestName = "Search_TwoElementsIndex0")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "Search_TwoElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3 }, 1, 0, TestName = "Search_ThreeElementsIndex0")]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, TestName = "Search_ThreeElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 2, TestName = "Search_ThreeElementsIndex2")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 0, TestName = "Search_FourElementsIndex0")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, 1, TestName = "Search_FourElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 2, TestName = "Search_FourElementsIndex2")]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, 3, TestName = "Search_FourElementsIndex3")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 1, 0, TestName = "Search_FiveElementsIndex0")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 2, 1, TestName = "Search_FiveElementsIndex1")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 3, 2, TestName = "Search_FiveElementsIndex2")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 4, 3, TestName = "Search_FiveElementsIndex3")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, 5, 4, TestName = "Search_FiveElementsIndex4")]
        [TestCase(new int[] { 1 }, 2, -1, TestName = "Search_OneElementNotFound")]
        [TestCase(new int[] { 2, 3 }, 1, -1, TestName = "Search_TwoElementsNotFound")]
        [TestCase(new int[] { 1, 2, 3 }, 4, -1, TestName = "Search_ThreeElementsNotFound")]
        [TestCase(new int[] { 2, 3, 4, 5 }, 1, -1, TestName = "Search_FourElementsNotFound")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, -2, 0, TestName = "Search_TenElements1")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, -1, 1, TestName = "Search_TenElements2")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 0, 2, TestName = "Search_TenElements3")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 1, 3, TestName = "Search_TenElements4")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 2, 4, TestName = "Search_TenElements5")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 3, 5, TestName = "Search_TenElements6")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 4, 6, TestName = "Search_TenElements7")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 5, 7, TestName = "Search_TenElements8")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 6, 8, TestName = "Search_TenElements9")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 7, 9, TestName = "Search_TenElements10")]
        [TestCase(new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7 }, 9, -1, TestName = "Search_TenElements11")]
        public void FindElementIndexTest(int[] inputArray, int inputElement, int expectedIndex)
        {
            var actualIndexIteratively = BinarySearch.FindIndexIteratively(inputElement, inputArray);
            var actualIndexRecusively = BinarySearch.FindIndexRecursively(inputElement, inputArray);
            var actualIndexLinearly = BinarySearch.FindIndexLinearly(inputElement, inputArray);
            var actualIndexFibonacci = BinarySearch.FindIndexUsingFibonacci(inputElement, inputArray);

            Assert.That(actualIndexIteratively, Is.EqualTo(expectedIndex));
            Assert.That(actualIndexRecusively, Is.EqualTo(expectedIndex));
            Assert.That(actualIndexLinearly, Is.EqualTo(expectedIndex));
            Assert.That(actualIndexFibonacci, Is.EqualTo(expectedIndex));
        }

        [TestCase(new int[] { 1 }, 1, 0, TestName = "Rank_OneElement")]
        [TestCase(new int[] { 1, 2 }, 1, 0, TestName = "Rank_TwoDifferent1")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "Rank_TwoDifferent2")]
        [TestCase(new int[] { 1, 1 }, 1, 0, TestName = "Rank_TwoTheSame1")]
        [TestCase(new int[] { 2, 2 }, 2, 0, TestName = "Rank_TwoTheSame1")]
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
            var actualCount = BinarySearch.Rank(inputElement, inputArray);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [TestCase(new int[] { 1 }, 1, 1, TestName = "Count_OneElement")]
        [TestCase(new int[] { 1, 2 }, 1, 1, TestName = "Count_TwoDifferent1")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "Count_TwoDifferent2")]
        [TestCase(new int[] { 1, 1 }, 1, 2, TestName = "Count_TwoTheSame1")]
        [TestCase(new int[] { 1, 2, 3 }, 1, 1, TestName = "Count_ThreeDifferent1")]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, TestName = "Count_ThreeDifferent2")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 1, TestName = "Count_ThreeDifferent3")]
        [TestCase(new int[] { 1, 1, 2 }, 1, 2, TestName = "Count_ThreeDifferent4")]
        [TestCase(new int[] { 1, 2, 2 }, 2, 2, TestName = "Count_ThreeDifferent6")]
        [TestCase(new int[] { 1, 1, 1 }, 1, 3, TestName = "Count_ThreeTheSame1")]
        [TestCase(new int[] { 1, 1, 2, 2 }, 1, 2, TestName = "Count_Four1")]
        [TestCase(new int[] { 1, 1, 2, 2 }, 2, 2, TestName = "Count_Four2")]
        [TestCase(new int[] { 1, 1, 2 }, 8, 0, TestName = "Count_Greater1")]
        [TestCase(new int[] { 3, 4, 15 }, 8, 0, TestName = "Count_Greater2")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 1, 1, TestName = "Count_More1")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 3, 1, TestName = "Count_More2")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 5, 2, TestName = "Count_More3")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 6, 1, TestName = "Count_More4")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 8, 2, TestName = "Count_More6")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 9, 0, TestName = "Count_More7")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 10, 3, TestName = "Count_More8")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 15, 1, TestName = "Count_More10")]
        [TestCase(new int[] { 1, 3, 5, 5, 6, 8, 8, 10, 10, 10, 15 }, 20, 0, TestName = "Count_More11")]
        public void CountTest(int[] inputArray, int inputElement, int expectedCount)
        {
            var actualCount = BinarySearch.Count(inputElement, inputArray);

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [TestCase(new int[] { 1 }, 1, 0, TestName = "FirstIndex_OneElement")]
        [TestCase(new int[] { 1, 1 }, 1, 0, TestName = "FirstIndex_TwoElements1")]
        [TestCase(new int[] { 1, 2 }, 1, 0, TestName = "FirstIndex_TwoElements2")]
        [TestCase(new int[] { 1, 2 }, 2, 1, TestName = "FirstIndex_TwoElements3")]
        [TestCase(new int[] { 1, 1, 1 }, 1, 0, TestName = "FirstIndex_ThreeElements1")]
        [TestCase(new int[] { 1, 2, 2 }, 1, 0, TestName = "FirstIndex_ThreeElements2")]
        [TestCase(new int[] { 1, 2, 2 }, 2, 1, TestName = "FirstIndex_ThreeElements3")]
        [TestCase(new int[] { 1, 1, 2 }, 2, 2, TestName = "FirstIndex_ThreeElements4")]
        [TestCase(new int[] { 1, 2, 3 }, 1, 0, TestName = "FirstIndex_ThreeElements5")]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, TestName = "FirstIndex_ThreeElements6")]
        [TestCase(new int[] { 1, 2, 3 }, 3, 2, TestName = "FirstIndex_ThreeElements7")]
        [TestCase(new int[] { 1, 2, 2, 2 }, 2, 1, TestName = "FirstIndex_FourElements1")]
        [TestCase(new int[] { 1, 1, 2, 2 }, 2, 2, TestName = "FirstIndex_FourElements2")]
        [TestCase(new int[] { 1, 1, 1, 2 }, 2, 3, TestName = "FirstIndex_FourElements3")]
        [TestCase(new int[] { 1, 2, 2, 3 }, 2, 1, TestName = "FirstIndex_FourElements4")]
        [TestCase(new int[] { 1, 2, 2, 2, 2 }, 1, 0, TestName = "FirstIndex_FiveElements1")]
        [TestCase(new int[] { 1, 1, 2, 2, 2 }, 1, 0, TestName = "FirstIndex_FiveElements2")]
        [TestCase(new int[] { 1, 1, 1, 2, 2 }, 1, 0, TestName = "FirstIndex_FiveElements3")]
        [TestCase(new int[] { 1, 1, 1, 1, 2 }, 1, 0, TestName = "FirstIndex_FiveElements4")]
        [TestCase(new int[] { 1, 2, 2, 2, 2 }, 2, 1, TestName = "FirstIndex_FiveElements5")]
        [TestCase(new int[] { 1, 1, 2, 2, 2 }, 2, 2, TestName = "FirstIndex_FiveElements6")]
        [TestCase(new int[] { 1, 1, 1, 2, 2 }, 2, 3, TestName = "FirstIndex_FiveElements7")]
        [TestCase(new int[] { 1, 1, 1, 1, 2 }, 2, 4, TestName = "FirstIndex_FiveElements8")]
        [TestCase(new int[] { 1, 2, 3, 3, 3 }, 3, 2, TestName = "FirstIndex_FiveElements9")]
        [TestCase(new int[] { 1, 2, 2, 3, 3 }, 3, 3, TestName = "FirstIndex_FiveElements10")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 2, 0, TestName = "FirstIndex_Mixed1")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 3, 2, TestName = "FirstIndex_Mixed2")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 4, 5, TestName = "FirstIndex_Mixed3")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 5, 7, TestName = "FirstIndex_Mixed4")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 6, 8, TestName = "FirstIndex_Mixed5")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 7, 9, TestName = "FirstIndex_Mixed6")]
        [TestCase(new int[] { 2, 2, 3, 3, 3, 4, 4, 5, 6, 7, 7, 7, 7, 7, 8, 8 }, 8, 14, TestName = "FirstIndex_Mixed7")]
        [TestCase(new int[] { 1 }, 2, -1, TestName = "FirstIndex_NotFound1")]
        [TestCase(new int[] { 1, 2 }, 3, -1, TestName = "FirstIndex_NotFound2")]
        [TestCase(new int[] { 1, 2, 4 }, 3, -1, TestName = "FirstIndex_NotFound3")]
        [TestCase(new int[] { 1, 2, 3, 5 }, 4, -1, TestName = "FirstIndex_NotFound4")]
        [TestCase(new int[] { 2, 3, 5, 7, 8 }, 1, -1, TestName = "FirstIndex_NotFound5")]
        [TestCase(new int[] { 2, 3, 5, 7, 8 }, 9, -1, TestName = "FirstIndex_NotFound6")]
        [TestCase(new int[] { 2, 3, 5, 7, 8 }, 4, -1, TestName = "FirstIndex_NotFound7")]
        public void FindFirstIndexTest(int[] inputArray, int inputElement, int expectedIndex)
        {
            var actualIndexIteratively = BinarySearch.FirstFirstIndexIteratively(inputElement, inputArray);
            var actualIndexRecursively = BinarySearch.FindFirstIndexRecursively(inputElement, inputArray);

            Assert.That(actualIndexIteratively, Is.EqualTo(expectedIndex));
            Assert.That(actualIndexRecursively, Is.EqualTo(expectedIndex));
        }

        [TestCase(@"Data\IntegersWithRepetitions.txt", 752524, 867, TestName = "FirstIndex_BigDataSet")]
        public void FindFirstIndexBigDataSetTest(string filename, int inputElement, int expectedIndex)
        {
            // The number 752524 occupies indexes from 867 to 871 in the sorted array.
            var inputArray = DataReader.ReadIntegers(filename);
            Array.Sort(inputArray);

            var actualIndexIteratively = BinarySearch.FirstFirstIndexIteratively(inputElement, inputArray);
            var actualIndexRecursively = BinarySearch.FindFirstIndexRecursively(inputElement, inputArray);

            Assert.That(actualIndexIteratively, Is.EqualTo(expectedIndex));
            Assert.That(actualIndexRecursively, Is.EqualTo(expectedIndex));
        }
    }
}