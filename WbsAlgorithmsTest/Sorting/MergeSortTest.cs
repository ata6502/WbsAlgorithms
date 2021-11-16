using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTests.Utilities;

namespace WbsAlgorithmsTests.Sorting
{
    [TestFixture]
    public class MergeSortTest
    {
        private const string JsonDataFilename = @"Data\Sorting.json";

        [TestCaseSource(nameof(CorrectnessTestCases))]
        public void SortTest(int[] inputArray)
        {
            var sortedArray = MergeSort.Sort(inputArray);

            Assert.AreEqual(inputArray.Length, sortedArray.Length);
            SortingHelpers.AssertAscendingOrder(sortedArray);
        }

        private static IEnumerable<TestCaseData> CorrectnessTestCases() => SortingHelpers.TestCases(JsonDataFilename, nameof(MergeSort));
    }
}