using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTests.Utilities
{
    internal class SortingHelper
    {
        public static void AssertAscendingOrder(int[] values)
        {
            for (int i = 0; i < values.Length - 1; ++i)
            {
                Assert.GreaterOrEqual(values[i + 1], values[i]);
            }
        }

        public static void AssertDescendingOrder(int[] values)
        {
            for (int i = 0; i < values.Length - 1; ++i)
            {
                Assert.LessOrEqual(values[i + 1], values[i]);
            }
        }

        public static IEnumerable<TestCaseData> SortTestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<SortingData>(jsonDataFilename);
            foreach(var d in testData)
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
    }
}
