using NUnit.Framework;
using System.Collections.Generic;

namespace WbsAlgorithmsTest.Utilities
{
    internal class TestCaseHelper
    {
        public static IEnumerable<TestCaseData> SortTestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<SortingData>(jsonDataFilename);
            foreach (var d in testData)
                yield return new TestCaseData(d.InputArray).SetName($"{testMethodPrefix}_{d.TestName}");
        }

        public static IEnumerable<TestCaseData> InversionCountingTestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<InversionCountingData>(jsonDataFilename);
            foreach (var d in testData)
                yield return new TestCaseData(d.InputArray, d.InversionCount).SetName($"{testMethodPrefix}_{d.TestName}");
        }

        public static IEnumerable<TestCaseData> ComparisonCountingTestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<QuickSortComparisonCountingData>(jsonDataFilename);
            foreach (var d in testData)
            {
                var inputArray = DataReader.ReadIntegers(d.InputFile);
                yield return new TestCaseData(
                    inputArray,
                    d.ComparisonCountFirstElement,
                    d.ComparisonCountLastElement,
                    d.ComparisonCountMedianElement).SetName($"{testMethodPrefix}_{d.TestName}");
            }
        }

        public static IEnumerable<TestCaseData> SelectionTestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<SelectionData>(jsonDataFilename);
            foreach (var d in testData)
            {
                yield return new TestCaseData(
                    d.InputArray,
                    d.InputOrderStatistic,
                    d.ExpectedOrderStatistic).SetName($"{testMethodPrefix}_{d.TestName}");
            }
        }
    }
}
