using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithmsTests.Utilities;

namespace WbsAlgorithmsTest.Utilities
{
    internal class TestDataHelper
    {
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
