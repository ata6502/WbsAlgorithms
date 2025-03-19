using NUnit.Framework;
using System;
using WbsAlgorithms.Arrays;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Arrays
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
            var result = LocalMinimum.FindLocalMinimumInArray(inputArray);

            // In some cases the number of comparisons is greater than 2*lg(n), n = inputArray.Length
            TestContext.Out.WriteLine($"#of compares: {result.Counter} ~ 2*lg(n)={2*Math.Log(inputArray.Length, 2.0):F0}");

            Assert.That(result.Index, Is.EqualTo(expectedIndex));
        }

        [TestCase("1", 1, 1)]                // a[0,0] = 1
        [TestCase("1,2,3 ; 4,5,6", 1, 1)]    // a[0,0] = 1
        [TestCase("7,2,3 ; 4,5,6", 2, 2)]    // a[0,1] = 2 or a[1,0] = 4
        [TestCase("6,5,1 ; 7,8,4", 1, 1)]    // a[0,2] = 1
        [TestCase("7,8,9 ; 4,5,6", 4, 4)]    // a[1,0] = 4
        [TestCase("4,5,6 ; 2,1,3", 1, 1)]    // a[1,1] = 1
        [TestCase("6,4,3 ; 5,2,1", 1, 1)]    // a[1,2] = 1
        [TestCase("7,1 ; 3,-1", -1, -1)]     // a[1,1] = -1

        // a[0,1] = 2 or a[1,0] = 4 or a[1,2] = 1
        [TestCase(@"5, 2, 3 ; 
                    4, 6, 1 ; 
                    7, 8, 9", 2, 1)] 

        // a[1,1] = -9
        [TestCase(@"5,  90, 3, 10 ; 
                    4, -9,  1, 15 ; 
                    7, -1,  9, 19 ; 
                    12, 8, 13, 99", -9, -9)]

        // a[2,2] = -8
        [TestCase(@"5,  90, 3, 10 ; 
                    4,  1, -7, 15 ; 
                    7, -1, -8, 19 ; 
                    12, 8, 13, 99", -8, -8)]

        // a[0,6] = 2
        [TestCase(@"20, 100, 12, 11,  10, 101, 2 ; 
                    19, 102, 13, 103, 9,  104, 3 ; 
                    18, 105, 14, 106, 8,  107, 4 ; 
                    17, 16,  15, 108, 7,  6,   5", 2, 2)]

        // a[6,2] = 2 or a[3,4] = 1
        [TestCase(@"180, 181, 39,  102, 103, 50, 104, 105, 106, 107, 108 ; 
                    179, 110, 38,  111, 112, 49, 113, 114, 115, 116, 117 ; 
                    37,  36,  33,  34,  35,  48, 118, 119, 120, 121, 122 ; 
                    123, 124, 32,  125, 1,   10, 126, 127, 128, 129, 130 ; 
                    131, 132, 31,  133, 134, 47, 135, 136, 137, 138, 139 ; 
                    46,  45,  30,  44,  43,  60, 51,  52,  53,  54,  55  ; 
                    140, 141, 2,   142, 143, 56, 144, 145, 146, 147, 148 ; 
                    149, 150, 151, 152, 153, 57, 154, 155, 156, 157, 158 ; 
                    159, 160, 161, 162, 163, 58, 164, 165, 166, 167, 168 ; 
                    169, 170, 171, 172, 173, 59, 174, 175, 176, 177, 178", 2, 1)]
        public void LocalMinimumInMatrixTest(string inputMatrixAsString, int expectedValueSimple, int expectedValueLinear)
        {
            var a = DataConverter.ConvertStringToMatrix<int>(inputMatrixAsString);

            var (i, j) = LocalMinimum.FindLocalMinimumInMatrixSimple(a);
            Assert.That(a[i,j], Is.EqualTo(expectedValueSimple));

            (i, j) = LocalMinimum.FindLocalMinimumInMatrixLinear(a);
            Assert.That(a[i, j], Is.EqualTo(expectedValueLinear));
        }
    }
}
