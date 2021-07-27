using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    public class BagTest
    {
        [Test]
        public void AddElementsTest()
        {
            var b = new BagLinkedList<int>();

            Assert.IsTrue(b.IsEmpty);

            var elementsToAdd = new List<int> { 4, 1, 3, 6, 8 };

            foreach (var v in elementsToAdd)
                b.Add(v);

            Assert.IsFalse(b.IsEmpty);
            Assert.AreEqual(5, b.Size);

            var elementsInBag = new List<int>(b.Size);
            foreach (var v in b)
                elementsInBag.Add(v);

            // Compare both collections.
            CollectionAssert.AreEqual(
                elementsToAdd.OrderBy(v => v),
                elementsInBag.OrderBy(v => v));
        }
    }
}
