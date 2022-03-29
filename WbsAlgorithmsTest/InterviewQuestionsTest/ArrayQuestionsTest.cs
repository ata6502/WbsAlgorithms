using NUnit.Framework;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class ArrayQuestionsTest
    {
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, new int[] { 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, new int[] { 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 6, 5, 4, 3, 2, 1 })]
        public void ReverseArrayTest(int[] inputArray, int[] expectedReversedArray)
        {
            // ReverseArray modifies the input array. It reverses the input array in-place.
            CollectionAssert.AreEqual(expectedReversedArray, ArrayQuestions.ReverseArray(inputArray));
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
            Assert.AreEqual(expectedSecondLargestValue, ArrayQuestions.FindSecondLargestValue(inputArray));
        }

        [TestCase(new int[] { 1, 3 }, 2)]
        [TestCase(new int[] { 2, 4 }, 3)]
        [TestCase(new int[] { 1, 2, 3, 5 }, 4)]
        [TestCase(new int[] { 3, 4, 6, 7, 8 }, 5)]
        public void FindMissingValueTest(int[] inputArray, int expectedMissingValue)
        {
            Assert.AreEqual(expectedMissingValue, ArrayQuestions.FindMissingValue(inputArray));
            Assert.AreEqual(expectedMissingValue, ArrayQuestions.FindMissingValueUsingFormula(inputArray));
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
            CollectionAssert.AreEqual(expectedDistinctValues, ArrayQuestions.RemoveDuplicates(inputArray));
        }

        [TestCase(new int[] { 1 }, new int[] { })]
        [TestCase(new int[] { 1, 1, 2 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 2 }, new int[] { 2 })]
        [TestCase(new int[] { 3, 3, 1, 2, 2, 3 }, new int[] { 3, 2 })]
        [TestCase(new int[] { 1, 3, 2, 1 }, new int[] { 1 })]
        [TestCase(new int[] { 3, 2, 1, 3, 4, 2, 1 }, new int[] { 3, 2, 1 })]
        public void FindDuplicatedNumbersTest(int[] inputArray, int[] expectedDuplicates)
        {
            CollectionAssert.AreEqual(expectedDuplicates, ArrayQuestions.FindDuplicatedNumbersUsingDictionary(inputArray));
            CollectionAssert.AreEqual(expectedDuplicates, ArrayQuestions.FindDuplicatedNumbersUsingLinq(inputArray));
        }
    }
}
