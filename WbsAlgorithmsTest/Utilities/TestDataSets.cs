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
}
