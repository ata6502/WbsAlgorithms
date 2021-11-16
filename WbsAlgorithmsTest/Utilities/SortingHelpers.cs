using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTests.Utilities
{
    internal class SortingHelpers
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

        public static IEnumerable<TestCaseData> TestCases(string jsonDataFilename, string testMethodPrefix)
        {
            var testData = DataReader.ReadJsonArray<SortingData>(jsonDataFilename);
            foreach(var d in testData)
                yield return new TestCaseData(d.InputArray).SetName($"{testMethodPrefix}_{d.TestName}");
        }
    }
}
