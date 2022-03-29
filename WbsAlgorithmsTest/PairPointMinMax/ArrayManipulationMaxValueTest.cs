using NUnit.Framework;
using WbsAlgorithms.PairPointMinMax;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.PairPointMinMax
{
    [TestFixture]
    public class ArrayManipulationMaxValueTest
    {
        [TestCase(10, "1 5 3,4 8 7,6 9 1", 10)]
        [TestCase(5, "1 2 100,2 5 100,3 4 100", 200)]
        [TestCase(10, "1 5 3,4 8 7,6 9 1,9 10 10", 11)]
        [TestCase(10, "2 6 8,3 5 7,1 8 1,5 9 15", 31)]
        [TestCase(9, "1 5 1,3 8 2,8 9 4,2 3 4", 7)]
        [TestCase(9, "1 5 1,3 8 2,8 9 6,2 3 4", 8)]
        public void GetMaxValueTest(int size, string operationsAsString, long expectedMaxValue)
        {
            var operations = ConvertOperations(operationsAsString);

            var actualMaxValue = ArrayManipulationMaxValue.GetMaxValue(size, operations);
            Assert.AreEqual(expectedMaxValue, actualMaxValue);

            var actualMaxValueBruteForce = ArrayManipulationMaxValue.GetMaxValueBruteForce(size, operations);
            Assert.AreEqual(expectedMaxValue, actualMaxValueBruteForce);
        }

        [TestCase(4000, @"Data\ArrayManipulationMaxValue1.txt", 30_000, 3, 7542539201)]
        [TestCase(10_000_000, @"Data\ArrayManipulationMaxValue2.txt", 100_000, 3, 2497169732)]
        public void GetMaxValueLargeTestCases(int size, string operationsFile, int rowCount, int columnCount, long expectedMaxValue)
        {
            var operations = DataReader.ReadIntegerMatrix(operationsFile, rowCount, columnCount);

            var actualMaxValue = ArrayManipulationMaxValue.GetMaxValue(size, operations);
            Assert.AreEqual(expectedMaxValue, actualMaxValue);
        }

        private int[,] ConvertOperations(string operationsAsString)
        {
            var data = operationsAsString.Split(',');
            var result = new int[data.Length, 3];

            for (var i = 0; i < data.Length; ++i)
            {
                var nums = data[i].Split(' ');
                int.TryParse(nums[0], out result[i, 0]); // startIndex
                int.TryParse(nums[1], out result[i, 1]); // stopIndex
                int.TryParse(nums[2], out result[i, 2]); // value
            }

            return result;
        }
    }
}
