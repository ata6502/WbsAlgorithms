using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTests.Utilities;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class QuickSortTest
    {
        private const string JsonDataFilename = @"Data\Sorting.json";

        [TestCaseSource(nameof(TestCases))]
        public void SortTest(int[] inputArray)
        {
            // QuickSort sorts the input array in-place. We need to make a copy of the array.
            var copyArray = new int[inputArray.Length];
            inputArray.CopyTo(copyArray, 0);

            QuickSort.Sort(copyArray);

            Assert.AreEqual(inputArray.Length, copyArray.Length);
            SortingHelper.AssertAscendingOrder(copyArray);
        }

        [TestCaseSource(nameof(TestCasesWithStrategy))]
        public void SortWithStrategyTest(int[] inputArray)
        {
            void SortWithStrategy(PivotStrategy strategy)
            {
                var copyArray = new int[inputArray.Length];
                inputArray.CopyTo(copyArray, 0);

                QuickSort.Sort(copyArray, strategy);

                Assert.AreEqual(inputArray.Length, copyArray.Length);
                SortingHelper.AssertAscendingOrder(copyArray);
            }

            SortWithStrategy(PivotStrategy.First);
            SortWithStrategy(PivotStrategy.Last);
            SortWithStrategy(PivotStrategy.Random);
            SortWithStrategy(PivotStrategy.Median);
        }

        private static IEnumerable<TestCaseData> TestCases() => SortingHelper.SortTestCases(JsonDataFilename, nameof(QuickSort));
        private static IEnumerable<TestCaseData> TestCasesWithStrategy() => SortingHelper.SortTestCases(JsonDataFilename, "QuickSortWithStrategy");
    }
}
