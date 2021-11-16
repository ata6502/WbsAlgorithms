using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTest.Utilities;
using WbsAlgorithmsTests.Utilities;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class InversionCountingTest
    {
        private const string JsonDataFilename = @"Data\SortingInversionCounting.json";

        [TestCaseSource(nameof(CountInversionsTestCases))]
        public void CountInversionsTest(int[] inputArray, uint expectedInversionCount)
        {
            var inversionCountBruteForce = InversionCounting.CountInversionsBruteForce(inputArray);
            Assert.AreEqual(expectedInversionCount, inversionCountBruteForce);

            var inversionCountSortAndCount = InversionCounting.SortAndCountInversions(inputArray, 0, inputArray.Length - 1);
            Assert.AreEqual(expectedInversionCount, inversionCountSortAndCount.InversionCount);
        }

        [TestCase(@"Data\SortingMediumDataset.txt", 2379u, TestName = "InversionCounting_SortingMediumDataset")]
        [TestCase(@"Data\SortingLargeDataset1.txt", 23948130u, TestName = "InversionCounting_LargeDataset1")]
        [TestCase(@"Data\SortingLargeDataset2.txt", 2407905288u, TestName = "InversionCounting_LargeDataset2")]
        public void CountInversionsUsingLargeDataset(string filename, uint expectedInversionCount)
        {
            var inputArray = DataReader.ReadIntegers(filename);

            var inversionCount = InversionCounting.SortAndCountInversions(inputArray, 0, inputArray.Length - 1);
            Assert.AreEqual(expectedInversionCount, inversionCount.InversionCount);
        }

        private static IEnumerable<TestCaseData> CountInversionsTestCases() => SortingHelper.InversionCountingTestCases(JsonDataFilename, "InversionCounting");
    }
}
