﻿using NUnit.Framework;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class ArrayQuestionsTest
    {
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