using NUnit.Framework;
using WbsAlgorithms.DataStructures;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class HeapTest
    {
        [TestCase(@"Data\Tree1.txt", 1, TestName = "Tree1")]
        [TestCase(@"Data\Tree2.txt", 225, TestName = "Tree2")]
        [TestCase(@"Data\Tree3.txt", 1, TestName = "Tree3")]
        [TestCase(@"Data\Tree4.txt", 1, TestName = "Tree4")]
        [TestCase(@"Data\TreeBig.txt", 1, TestName = "TreeBig")]
        public void ExtractMinimumTest(string filename, int expectedMinimum)
        {
            var keys = DataReader.ReadIntegers(filename);
            var h = new Heap(keys.Length);
            h.Insert(keys);

            var min = h.ExtractMinimum();

            Assert.AreEqual(expectedMinimum, min);
        }
    }
}
