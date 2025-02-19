using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class ArrayQuestionsTest
    {
        [TestCaseSource(nameof(MatrixTestCases))]
        public void SetZerosInMatrixTest(int[,] inputMatrix, int[,] expectedZeroMatrix)
        {
            var m = inputMatrix.GetUpperBound(0) - inputMatrix.GetLowerBound(0) + 1;
            var n = inputMatrix.GetUpperBound(1) - inputMatrix.GetLowerBound(1) + 1;
            var matrix = new int[m, n];

            Array.Copy(inputMatrix, matrix, m * n);
            ArrayQuestions.SetZerosInMatrix(matrix); // works in-place
            Assert.That(matrix, Is.EqualTo(expectedZeroMatrix).AsCollection);

            Array.Copy(inputMatrix, matrix, m * n);
            ArrayQuestions.SetZerosInMatrixWithoutAdditionalStorage(matrix); // works in-place
            Assert.That(matrix, Is.EqualTo(expectedZeroMatrix).AsCollection);
        }

        private static IEnumerable<TestCaseData> MatrixTestCases()
        {
            yield return new TestCaseData(new[,] { { 1, 0 }, { 2, 3 } }, new[,] { { 0, 0 }, { 2, 0 } }).SetName("ZeroMatrix_2x2_1");
            yield return new TestCaseData(new[,] { { 1, 2 }, { 0, 3 } }, new[,] { { 0, 2 }, { 0, 0 } }).SetName("ZeroMatrix_2x2_2");
            yield return new TestCaseData(new[,] { { 1, 2, 0 }, { 3, 4, 5 } }, new[,] { { 0, 0, 0 }, { 3, 4, 0 } }).SetName("ZeroMatrix_2x3_1");
            yield return new TestCaseData(new[,] { { 1, 2, 3 }, { 4, 0, 5 } }, new[,] { { 1, 0, 3 }, { 0, 0, 0 } }).SetName("ZeroMatrix_2x3_2");
            yield return new TestCaseData(new[,] { { 0, 2, 3, 4 }, { 5, 6, 0, 8 }, { 9, 10, 11, 12 } }, new[,] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 10, 0, 12 } }).SetName("ZeroMatrix_3x4_1");
            yield return new TestCaseData(new[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 0, 12 }, { 13, 14, 15, 16 } }, new[,] { { 1, 2, 0, 4 }, { 5, 6, 0, 8 }, { 0, 0, 0, 0 }, { 13, 14, 0, 16 } }).SetName("ZeroMatrix_4x4_1");
            yield return new TestCaseData(new[,] { { 1, 2, 3, 4 }, { 5, 0, 7, 0 }, { 9, 10, 11, 12 }, { 13, 14, 0, 16 } }, new[,] { { 1, 0, 0, 0 }, { 0, 0, 0, 0 }, { 9, 0, 0, 0 }, { 0, 0, 0, 0 } }).SetName("ZeroMatrix_4x4_2");
        }

        [Test]
        public void RotateMatrixTest()
        {
            var matrix1 = new[,] { { 1, 2 }, { 3, 4 } };
            var expectedRotatedMatrix1 = new[,] { { 3, 1 }, { 4, 2 } };
            ArrayQuestions.RotateMatrix(matrix1);
            Assert.That(matrix1, Is.EqualTo(expectedRotatedMatrix1).AsCollection);

            var matrix2 = new [,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            var expectedRotatedMatrix2 = new [,] { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };
            ArrayQuestions.RotateMatrix(matrix2);
            Assert.That(matrix2, Is.EqualTo(expectedRotatedMatrix2).AsCollection);

            var matrix3 = new[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } };
            var expectedRotatedMatrix3 = new[,] { { 13, 9, 5, 1 }, { 14, 10, 6, 2 }, { 15, 11, 7, 3 }, { 16, 12, 8, 4 } };
            ArrayQuestions.RotateMatrix(matrix3);
            Assert.That(matrix3, Is.EqualTo(expectedRotatedMatrix3).AsCollection);
        }

        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 6, 5, 4, 3, 2, 1 })]
        public void ReverseArrayTest(int[] inputArray, int[] expectedReversedArray)
        {
            // ReverseArray modifies the input array. It reverses the input array in-place.
            Assert.That(ArrayQuestions.ReverseArray(inputArray), Is.EqualTo(expectedReversedArray).AsCollection);
        }

        [TestCase(new int[] { 1, 2 }, 1)]
        [TestCase(new int[] { 2, 1 }, 1)]
        [TestCase(new int[] { 3, 2, 1 }, 2)]
        [TestCase(new int[] { 2, 3, 1 }, 2)]
        [TestCase(new int[] { 3, 1, 2 }, 2)]
        [TestCase(new int[] { 4, 3, 3, 2, 5, 5, 3, 4, 4, 6 }, 5)]
        [TestCase(new int[] { 5, 3, 3, 2, 4 }, 4)]
        [TestCase(new int[] { 3, 4, 6, 7, 8 }, 7)]
        [TestCase(new int[] { }, -1)]
        [TestCase(new int[] { 1 }, -1)]
        [TestCase(new int[] { 1, 1, 1 }, -1)]
        public void FindSecondLargestValueTest(int[] inputArray, int expectedSecondLargestValue)
        {
            Assert.That(ArrayQuestions.FindSecondLargestValue(inputArray), Is.EqualTo(expectedSecondLargestValue));
        }

        [TestCase(new int[] { 1, 3 }, 2)]
        [TestCase(new int[] { 2, 4 }, 3)]
        [TestCase(new int[] { 1, 2, 3, 5 }, 4)]
        [TestCase(new int[] { 3, 4, 6, 7, 8 }, 5)]
        public void FindMissingValueTest(int[] inputArray, int expectedMissingValue)
        {
            Assert.That(ArrayQuestions.FindMissingValue(inputArray), Is.EqualTo(expectedMissingValue));
            Assert.That(ArrayQuestions.FindMissingValueUsingFormula(inputArray), Is.EqualTo(expectedMissingValue));
        }

        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 1 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 1, 1, 2, 2, 2, 1, 2, 1, 2 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 1, 1, 8, 2, 2, 2, 3, 1, 1, 1, 3, 3 }, new int[] { 1, 8, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1, 3, 4, 2, 1 }, new int[] { 3, 2, 1, 4 })]
        public void RemoveDuplicatesTest(int[] inputArray, int[] expectedDistinctValues)
        {
            Assert.That(ArrayQuestions.RemoveDuplicates(inputArray), Is.EqualTo(expectedDistinctValues).AsCollection);
        }

        [TestCase(new int[] { 1 }, new int[] { })]
        [TestCase(new int[] { 1, 1, 2 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 2 }, new int[] { 2 })]
        [TestCase(new int[] { 3, 3, 1, 2, 2, 3 }, new int[] { 3, 2 })]
        [TestCase(new int[] { 1, 3, 2, 1 }, new int[] { 1 })]
        [TestCase(new int[] { 3, 2, 1, 3, 4, 2, 1 }, new int[] { 3, 2, 1 })]
        public void FindDuplicatedNumbersTest(int[] inputArray, int[] expectedDuplicates)
        {
            Assert.That(ArrayQuestions.FindDuplicatedNumbersUsingDictionary(inputArray), Is.EqualTo(expectedDuplicates).AsCollection);
            Assert.That(ArrayQuestions.FindDuplicatedNumbersUsingLinq(inputArray), Is.EqualTo(expectedDuplicates).AsCollection);
        }
    }
}
