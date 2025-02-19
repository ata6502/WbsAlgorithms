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
        public void FindValueTest(int[] inputArray, int inputOrderStatistic, int expectedOrderStatistic)
        {
            var actualOrderStatistic = RSelect.FindValue(inputArray, inputOrderStatistic);
            Assert.That(actualOrderStatistic, Is.EqualTo(expectedOrderStatistic));
        }

        [TestCase(@"Data\Selection100.txt", 49, 4715, TestName = "RSelect_100numbers")]
        public void FindValueTest(string filename, int inputOrderStatistic, int expectedOrderStatistic)
        {
            var inputArray = DataReader.ReadIntegers(filename);

            var actualOrderStatistic = RSelect.FindValue(inputArray, inputOrderStatistic);
            Assert.That(actualOrderStatistic, Is.EqualTo(expectedOrderStatistic));
        }

        private static IEnumerable<TestCaseData> TestCases() => TestCaseHelper.SelectionTestCases(JsonDataFilename, nameof(RSelect));
    }
}
