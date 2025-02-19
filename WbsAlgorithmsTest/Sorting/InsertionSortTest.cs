using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class InsertionSortTest
    {
        private const string JsonDataFilename = @"Data\Sorting.json";

        [TestCaseSource(nameof(TestCasesAscending))]
        public void SortAscendingTest(int[] inputArray)
        {
            // We keep the original array because InsertionSort sorts the input array in-place. 
            var sortedArray = new int[inputArray.Length];
            Array.Copy(inputArray, sortedArray, inputArray.Length);

            InsertionSort.SortAscending(sortedArray);

            Assert.That(sortedArray.Length, Is.EqualTo(inputArray.Length));
            SortingHelper.AssertAscendingOrder(sortedArray);
        }

        [TestCaseSource(nameof(TestCasesDescending))]
        public void SortDescendingTest(int[] inputArray)
        {
            // We keep the original array because InsertionSort sorts the input array in-place. 
            var sortedArray = new int[inputArray.Length];
            Array.Copy(inputArray, sortedArray, inputArray.Length);

            InsertionSort.SortDescending(sortedArray);

            Assert.That(sortedArray.Length, Is.EqualTo(inputArray.Length));
            SortingHelper.AssertDescendingOrder(sortedArray);
        }

        private static IEnumerable<TestCaseData> TestCasesAscending() => TestCaseHelper.SortTestCases(JsonDataFilename, nameof(InsertionSort) + "Asc");
        private static IEnumerable<TestCaseData> TestCasesDescending() => TestCaseHelper.SortTestCases(JsonDataFilename, nameof(InsertionSort) + "Desc");
    }
}