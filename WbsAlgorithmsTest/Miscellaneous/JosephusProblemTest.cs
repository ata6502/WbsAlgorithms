using NUnit.Framework;
using WbsAlgorithms.Miscellaneous;

namespace WbsAlgorithmsTest.Miscellaneous
{
    [TestFixture]
    public class JosephusProblemTest
    {
        [TestCase(7, 2, new int[] { 1, 3, 5, 0, 4, 2, 6 })]
        [TestCase(8, 2, new int[] { 1, 3, 5, 7, 2, 6, 4, 0 })]
        [TestCase(8, 3, new int[] { 2, 5, 0, 4, 1, 7, 3, 6 })]
        // n - the number of people
        // m - eliminating every m-th person
        public void GetEliminatedPositionsTest(int n, int m, int[] expectedOrder)
        {
            var actualOrder = JosephusProblem.GetEliminatedPositions(n, m);
            CollectionAssert.AreEqual(expectedOrder, actualOrder);
        }
    }
}
