﻿using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Sorting;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Sorting
{
    [TestFixture]
    public class QuickSortTest
    {
        private const string JsonDataFilename = @"Data\Sorting.json";
        private const string JsonComparisonCountingDataFilename = @"Data\SortingComparisonCounting.json";

        [TestCaseSource(nameof(TestCases))]
        public void SortTest(int[] inputArray)
        {
            // QuickSort sorts the input array in-place. We need to make a copy of the array.
            var copyArray = new int[inputArray.Length];
            inputArray.CopyTo(copyArray, 0);

            QuickSort.Sort(copyArray);

            Assert.That(copyArray.Length, Is.EqualTo(inputArray.Length));
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

                Assert.That(copyArray.Length, Is.EqualTo(inputArray.Length));
                SortingHelper.AssertAscendingOrder(copyArray);
            }

            SortWithStrategy(PivotStrategy.First);
            SortWithStrategy(PivotStrategy.Last);
            SortWithStrategy(PivotStrategy.Random);
            SortWithStrategy(PivotStrategy.Median);
        }

        [TestCaseSource(nameof(TestCasesComparisonCounting))]
        public void ComparisonCountingTest(int[] inputArray, int expectedComparisonCountFirstElement, int expectedComparisonCountLastElement, int expectedComparisonCountMedianElement)
        {
            int SortAndCountComparisons(PivotStrategy strategy)
            {
                var copyArray = new int[inputArray.Length];
                inputArray.CopyTo(copyArray, 0);

                // Return the comparison count for a given strategy.
                return QuickSort.Sort(copyArray, strategy);
            }

            Assert.That(SortAndCountComparisons(PivotStrategy.First), Is.EqualTo(expectedComparisonCountFirstElement));
            Assert.That(SortAndCountComparisons(PivotStrategy.Last), Is.EqualTo(expectedComparisonCountLastElement));
            Assert.That(SortAndCountComparisons(PivotStrategy.Median), Is.EqualTo(expectedComparisonCountMedianElement));
        }

        private static IEnumerable<TestCaseData> TestCases() => TestCaseHelper.SortTestCases(JsonDataFilename, nameof(QuickSort));
        private static IEnumerable<TestCaseData> TestCasesWithStrategy() => TestCaseHelper.SortTestCases(JsonDataFilename, "QuickSortWithStrategy");
        private static IEnumerable<TestCaseData> TestCasesComparisonCounting() => TestCaseHelper.ComparisonCountingTestCases(JsonComparisonCountingDataFilename, "QuickSortComparisonCounting");
    }
}
