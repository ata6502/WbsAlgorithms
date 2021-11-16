namespace WbsAlgorithmsTests.Utilities
{
    class SortingData
    {
        public string TestName { get; set; }
        public int[] InputArray { get; set; }

        public override string ToString() => $"{TestName}: {InputArray.Length} elements";
    }
}
