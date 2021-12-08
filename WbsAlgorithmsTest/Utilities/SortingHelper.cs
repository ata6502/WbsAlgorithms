using NUnit.Framework;

namespace WbsAlgorithmsTest.Utilities
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
    }
}
