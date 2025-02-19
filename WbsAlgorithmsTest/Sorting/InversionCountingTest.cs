using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTest.Utilities;

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
            Assert.That(inversionCountBruteForce, Is.EqualTo(expectedInversionCount));

            var inversionCountSortAndCount = InversionCounting.SortAndCountInversions(inputArray, 0, inputArray.Length - 1);
            Assert.That(inversionCountSortAndCount.InversionCount, Is.EqualTo(expectedInversionCount));
        }

        [TestCase(@"Data\SortingMediumDataset.txt", 2379u, TestName = "InversionCounting_SortingMediumDataset")]
        [TestCase(@"Data\SortingLargeDataset1.txt", 23948130u, TestName = "InversionCounting_LargeDataset1")]
        [TestCase(@"Data\SortingLargeDataset2.txt", 2407905288u, TestName = "InversionCounting_LargeDataset2")]
        public void CountInversionsUsingLargeDataset(string filename, uint expectedInversionCount)
        {
            var inputArray = DataReader.ReadIntegers(filename);

            var inversionCount = InversionCounting.SortAndCountInversions(inputArray, 0, inputArray.Length - 1);
            Assert.That(inversionCount.InversionCount, Is.EqualTo(expectedInversionCount));
        }

        private static IEnumerable<TestCaseData> CountInversionsTestCases() => TestCaseHelper.InversionCountingTestCases(JsonDataFilename, "InversionCounting");
    }
}
