namespace WbsAlgorithmsTests.Utilities
{
    internal class SortingData
    {
        public string TestName { get; set; }
        public int[] InputArray { get; set; }

        public override string ToString() => $"{TestName}: {InputArray.Length} elements";
    }

    internal class InversionCountingData
    {
        public string TestName { get; set; }
        public int[] InputArray { get; set; }
        public uint InversionCount { get; set; }

        public override string ToString() => $"{TestName}: {InputArray.Length} elements";
    }

    internal class QuickSortComparisonCountingData
    {
        public string TestName { get; set; }
        public string InputFile { get; set; }

        // The number of comparisons if the first element is used as the pivot.
        public int ComparisonCountFirstElement { get; set; }

        // The number of comparisons if the last element is used as the pivot.
        public int ComparisonCountLastElement { get; set; }

        // The number of comparisons if the median-of-three is used as the pivot.
        public int ComparisonCountMedianElement { get; set; }

        public override string ToString() => $"{TestName}: {InputFile} file";
    }

    internal class SelectionData
    {
        public string TestName { get; set; }
        public int[] InputArray { get; set; }
        public int InputOrderStatistic { get; set; }
        public int ExpectedOrderStatistic { get; set; }

        public override string ToString() => $"{TestName}: {InputArray.Length} elements";
    }
}
