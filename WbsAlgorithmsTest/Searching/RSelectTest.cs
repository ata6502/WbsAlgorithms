using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Searching;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Searching
{
    [TestFixture]
    public class RSelectTest
    {
        private const string JsonDataFilename = @"Data\Selection.json";

        [TestCaseSource(nameof(TestCases))]
        public void GetValueTest(int[] inputArray, int inputOrderStatistic, int expectedOrderStatistic)
        {
            var actualOrderStatistic = RSelect.GetValue(inputArray, inputOrderStatistic);
            Assert.AreEqual(expectedOrderStatistic, actualOrderStatistic);
        }

        private static IEnumerable<TestCaseData> TestCases() => TestDataHelper.SelectionTestCases(JsonDataFilename, nameof(RSelect));
    }
}
