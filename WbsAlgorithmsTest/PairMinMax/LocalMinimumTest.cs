using NUnit.Framework;
using System;
using WbsAlgorithms.PairMinMax;

namespace WbsAlgorithmsTest.PairMinMax
{
    [TestFixture]
    public class LocalMinimumTest
    {
        [TestCase(new int[] { 1 }, 0)] // a[0]=1
        [TestCase(new int[] { 1, 2 }, 0)] // a[0]=1
        [TestCase(new int[] { 2, 1 }, 1)] // a[1]=1
        [TestCase(new int[] { 1, 2, 3 }, 0)] // a[0]=1
        [TestCase(new int[] { 2, 1, 3 }, 1)] // a[1]=1
        [TestCase(new int[] { 3, 2, 1 }, 2)] // a[2]=1
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 }, 0)] // a[0]=1
        [TestCase(new int[] { 7, 6, 5, 4, 3, 2, 1 }, 6)] // a[6]=1
        [TestCase(new int[] { 8, 7, 6, 5, 4, 3, 2, 1 }, 7)] // a[7]=1
        [TestCase(new int[] { 1, 0, 2, 3, 4, 5, 6, 7 }, 1)] // a[1]=0
        [TestCase(new int[] { 2, 1, 0, 3, 4, 5, 6, 7 }, 2)] // a[2]=0
        [TestCase(new int[] { 3, 2, 1, 0, 4, 5, 6, 7 }, 3)] // a[3]=0 - This is the only case that requires more comparisons than 2*lg(n): #of compares: 8 ~ 2*lg(n)=6
        [TestCase(new int[] { 4, 3, 2, 1, 0, 5, 6, 7 }, 4)] // a[4]=0
        [TestCase(new int[] { 5, 4, 3, 2, 1, 0, 6, 7 }, 5)] // a[5]=0
        [TestCase(new int[] { 6, 5, 4, 3, 2, 1, 0, 7 }, 6)] // a[6]=0
        [TestCase(new int[] { 1, 0, 4, 8, 5, 2, -1, 7 }, 6)] // a[6]=-1
        [TestCase(new int[] { 8, 9, -1, 3, 4, 1, -3, 2, -2, -4, 6 }, 9)] // a[0]=8 or a[2]=-1 or a[6]=-3 or a[9]=-4
        [TestCase(new int[] { 10, -9, 20, 25, 21, 40, 50, -20 }, 4)] // a[1]=-9 or a[4]=21 or a[7]=-20
        [TestCase(new int[] { -4, -3, 9, 4, 10, 2, 20 }, 3)] // a[0]=-4 or a[3]=4 or a[5]=2
        [TestCase(new int[] { 5, -3, -5, -6, -7, -8 }, 5)] // a[5]=-8
        public void LocalMinimumInArrayTest(int[] inputArray, int expectedIndex)
        {
            // The method finds one local minimum. It's always the same minimum.
            var result = LocalMinimum.FindLocalMinimumIndexInArray(inputArray);

            // In some cases the number of comparisons is greater than 2*lg(n), n = inputArray.Length
            Console.WriteLine($"#of compares: {result.Counter} ~ 2*lg(n)={2*Math.Log(inputArray.Length, 2.0):F0}");

            Assert.AreEqual(expectedIndex, result.Index);
        }
    }
}
