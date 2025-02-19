using NUnit.Framework;

namespace WbsAlgorithmsTest.Utilities
{
    internal class SortingHelper
    {
        public static void AssertAscendingOrder(int[] values)
        {
            for (int i = 0; i < values.Length - 1; ++i)
            {
                Assert.That(values[i + 1], Is.GreaterThanOrEqualTo(values[i]));
            }
        }

        public static void AssertDescendingOrder(int[] values)
        {
            for (int i = 0; i < values.Length - 1; ++i)
            {
                Assert.That(values[i + 1], Is.LessThanOrEqualTo(values[i]));
            }
        }
    }
}
