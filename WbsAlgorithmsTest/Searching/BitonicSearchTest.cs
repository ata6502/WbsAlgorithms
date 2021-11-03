using NUnit.Framework;
using System;
using WbsAlgorithms.Searching;

namespace WbsAlgorithmsTest.Searching
{
    [TestFixture]
    public class BitonicSearchTest
    {
        [TestCase(new int[] { 1 }, 1, 0)]
        [TestCase(new int[] { 1 }, 8, -1)]
        [TestCase(new int[] { 1, 2 }, 1, 0)]
        [TestCase(new int[] { 2, 1 }, 1, 1)]
        [TestCase(new int[] { 1, 2, 3 }, 1, 0)]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1)]
        [TestCase(new int[] { 1, 2, 3 }, 3, 2)]
        [TestCase(new int[] { 3, 2, 1 }, 3, 0)]
        [TestCase(new int[] { 3, 2, 1 }, 2, 1)]
        [TestCase(new int[] { 3, 2, 1 }, 1, 2)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 1, 0)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, 1)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 3, 2)]
        [TestCase(new int[] { 1, 2, 3, 4 }, 4, 3)]
        [TestCase(new int[] { 1, 3, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 3, 2 }, 3, 1)]
        [TestCase(new int[] { 1, 3, 2 }, 2, 2)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 5, 1)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 8, 2)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 7, 3)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 4, 4)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 2, 5)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, 99, -1)]
        [TestCase(new int[] { 1, 5, 8, 7, 4, 2 }, -9, -1)]
        [TestCase(new int[] { 1, 6, 8, 4 }, 1, 0)]
        [TestCase(new int[] { 1, 6, 8, 4 }, 6, 1)]
        [TestCase(new int[] { 1, 6, 8, 4 }, 8, 2)]
        [TestCase(new int[] { 1, 6, 8, 4 }, 4, 3)]
        [TestCase(new int[] { 4, 8, 6, 1 }, 4, 0)]
        [TestCase(new int[] { 4, 8, 6, 1 }, 8, 1)]
        [TestCase(new int[] { 4, 8, 6, 1 }, 6, 2)]
        [TestCase(new int[] { 4, 8, 6, 1 }, 1, 3)]
        [TestCase(new int[] { 1, 2, 3, 4, -1, -2, -3 }, -1, 4)]
        [TestCase(new int[] { 1, 5, 4, 3, 2, 0 }, 5, 1)]
        [TestCase(new int[] { 2, 4, 8, 16, 32, 1 }, 2, 0)]
        [TestCase(new int[] { 2, 4, 8, 16, 32, 1 }, 88, -1)]
        [TestCase(new int[] { 2, 4, 8, 16, 32 }, 32, 4)]
        [TestCase(new int[] { -3, 9, 18, 20, 17, 5, 1 }, 20, 3)]
        [TestCase(new int[] { 5, 6, 7, 8, 9, 10, 3, 2, 1 }, 30, -1)]
        [TestCase(new int[] { 6, 8, 12, 9, 4, 1, -5, -6, -10, -23, -56, -78, -90, -91 }, -5, 6)]
        [TestCase(new int[] { 6, 8, 12, 9, 4, 1, -5, -6, -10, -23, -56, -78, -90, -91 }, 6, 0)]
        [TestCase(new int[] { 6, 8, 12, 9, 4, 1, -5, -6, -10, -23, -56, -78, -90, -91 }, -91, 13)]
        [TestCase(new int[] { -8, 1, 2, 3, 4, 5, -2, -3 }, -8, 0)]
        [TestCase(new int[] { -8, 1, 2, 3, 4, 5, -2, -3 }, 4, 4)]
        [TestCase(new int[] { -8, 1, 2, 3, 4, 5, -2, -3 }, 5, 5)]
        [TestCase(new int[] { -8, 1, 2, 3, 4, 5, -2, -3 }, -2, 6)]
        [TestCase(new int[] { -8, 1, 2, 3, 4, 5, -2, -3 }, -3, 7)]
        [TestCase(new int[] { 1, 5, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 5, 2 }, 5, 1)]
        [TestCase(new int[] { 1, 5, 2 }, 2, 2)]
        [TestCase(new int[] { 1, 3, 5, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 3, 5, 2 }, 3, 1)]
        [TestCase(new int[] { 1, 3, 5, 2 }, 5, 2)]
        [TestCase(new int[] { 1, 3, 5, 2 }, 2, 3)]
        [TestCase(new int[] { 1, 5, 4, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 5, 4, 2 }, 5, 1)]
        [TestCase(new int[] { 1, 5, 4, 2 }, 4, 2)]
        [TestCase(new int[] { 1, 5, 4, 2 }, 2, 3)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 1, 0)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 3, 1)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 5, 2)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 4, 3)]
        [TestCase(new int[] { 1, 3, 5, 4, 2 }, 2, 4)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 18, 0)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 23, 1)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 20, 2)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 8, 3)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 3, 4)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 2, 5)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, 1, 6)]
        [TestCase(new int[] { 18, 23, 20, 8, 3, 2, 1, -9 }, -9, 7)]
        public void FindIndexTest(int[] inputArray, int elementToFind, int expectedIndex)
        {
            var result = BitonicSearch.FindIndex(inputArray, elementToFind);

            // In a few test cases, the number of comparisons (the Counter) is greater than 3*lg(N) by 1 or 2 comparisons.
            // For example:
            // - FindIndexTest([1, 2, 3, 4],1,0): #of compares = 7; 3*lg(n) = 6
            // - FindIndexTest([1, 2, 3, 4],4,3): #of compares = 8; 3*lg(n) = 6
            // - FindIndexTest([1, 3, 5, 2],2,3): #of compares = 7; 3*lg(n) = 6
            // - FindIndexTest([2, 4, 8, 16, 32],32,4): #of compares = 8; 3*lg(n) = 6
            // - FindIndexTest([5, 6, 7, 8, 9, 10, 3, 2, 1],30,-1): #of compares = 10; 3*lg(n) = 9
            Console.WriteLine($"#of compares: {result.Counter} <= 3*lg(n)={3 * Math.Round(Math.Log(inputArray.Length, 2.0)):F0}");

            Assert.AreEqual(expectedIndex, result.Index);
        }
    }
}
