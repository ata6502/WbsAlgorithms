using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class MergeSortTest
    {
        private const string JsonDataFilename = @"Data\Sorting.json";

        [TestCaseSource(nameof(TestCases))]
        public void SortTest(int[] inputArray)
        {
            var sortedArray = MergeSort.Sort(inputArray);

            Assert.AreEqual(inputArray.Length, sortedArray.Length);
            SortingHelper.AssertAscendingOrder(sortedArray);
        }

        private static IEnumerable<TestCaseData> TestCases() => TestCaseHelper.SortTestCases(JsonDataFilename, nameof(MergeSort));
    }
}