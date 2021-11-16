using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
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

        private static IEnumerable<TestCaseData> CountInversionsTestCases() => SortingHelper.InversionCountingTestCases(JsonDataFilename);
    }
}
