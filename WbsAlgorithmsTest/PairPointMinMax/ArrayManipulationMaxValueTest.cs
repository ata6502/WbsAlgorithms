using NUnit.Framework;
using WbsAlgorithms.PairPointMinMax;

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
        public void GetMaxValueTest(int size, string operationsAsString, int expectedMaxValue)
        {
            var operations = ConvertOperations(operationsAsString);

            var actualMaxValue = ArrayManipulationMaxValue.GetMaxValue(size, operations);
            Assert.AreEqual(expectedMaxValue, actualMaxValue);

            var actualMaxValueBruteForce = ArrayManipulationMaxValue.GetMaxValueBruteForce(size, operations);
            Assert.AreEqual(expectedMaxValue, actualMaxValueBruteForce);
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
