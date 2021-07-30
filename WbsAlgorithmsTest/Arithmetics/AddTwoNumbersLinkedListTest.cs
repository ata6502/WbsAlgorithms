using NUnit.Framework;
using WbsAlgorithms.Arithmetics;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class AddTwoNumbersLinkedListTest
    {
        [Test]
        public void AddTwoNumbersTest()
        {
            var l1 = SinglyLinkedList.Create(new int[] { 2, 4, 3 });
            var l2 = SinglyLinkedList.Create(new int[] { 5, 6, 4 });

            var result1 = AddTwoNumbersLinkedList.AddTwoNumbers(l1, l2);
            Assert.AreEqual(7, result1.Item);
            Assert.AreEqual(0, result1.Next.Item);
            Assert.AreEqual(8, result1.Next.Next.Item);

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.AreEqual(7, result2.Item);
            Assert.AreEqual(0, result2.Next.Item);
            Assert.AreEqual(8, result2.Next.Next.Item);
        }

        [Test]
        public void AddTwoNumbersOneDigitAndTwoDigitsTest()
        {
            var l1 = SinglyLinkedList.Create(new int[] { 1 });
            var l2 = SinglyLinkedList.Create(new int[] { 9, 9 });

            var result1 = AddTwoNumbersLinkedList.AddTwoNumbers(l1, l2);
            Assert.AreEqual(0, result1.Item);
            Assert.AreEqual(0, result1.Next.Item);
            Assert.AreEqual(1, result1.Next.Next.Item);

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.AreEqual(0, result2.Item);
            Assert.AreEqual(0, result2.Next.Item);
            Assert.AreEqual(1, result2.Next.Next.Item);
        }

        [Test]
        public void AddTwoNumbersOneDigitTest()
        {
            var l1 = SinglyLinkedList.Create(new int[] { 5 });
            var l2 = SinglyLinkedList.Create(new int[] { 5 });

            var result1 = AddTwoNumbersLinkedList.AddTwoNumbers(l1, l2);
            Assert.AreEqual(0, result1.Item);
            Assert.AreEqual(1, result1.Next.Item);

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.AreEqual(0, result2.Item);
            Assert.AreEqual(1, result2.Next.Item);
        }
    }
}
