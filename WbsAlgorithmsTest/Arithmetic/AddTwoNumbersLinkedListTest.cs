using NUnit.Framework;
using WbsAlgorithms.Arithmetic;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.Arithmetic
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
            Assert.That(result1.Item, Is.EqualTo(7));
            Assert.That(result1.Next.Item, Is.EqualTo(0));
            Assert.That(result1.Next.Next.Item, Is.EqualTo(8));

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.That(result2.Item, Is.EqualTo(7));
            Assert.That(result2.Next.Item, Is.EqualTo(0));
            Assert.That(result2.Next.Next.Item, Is.EqualTo(8));
        }

        [Test]
        public void AddTwoNumbersOneDigitAndTwoDigitsTest()
        {
            var l1 = SinglyLinkedList.Create(new int[] { 1 });
            var l2 = SinglyLinkedList.Create(new int[] { 9, 9 });

            var result1 = AddTwoNumbersLinkedList.AddTwoNumbers(l1, l2);
            Assert.That(result1.Item, Is.EqualTo(0));
            Assert.That(result1.Next.Item, Is.EqualTo(0));
            Assert.That(result1.Next.Next.Item, Is.EqualTo(1));

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.That(result2.Item, Is.EqualTo(0));
            Assert.That(result2.Next.Item, Is.EqualTo(0));
            Assert.That(result2.Next.Next.Item, Is.EqualTo(1));
        }

        [Test]
        public void AddTwoNumbersOneDigitTest()
        {
            var l1 = SinglyLinkedList.Create(new int[] { 5 });
            var l2 = SinglyLinkedList.Create(new int[] { 5 });

            var result1 = AddTwoNumbersLinkedList.AddTwoNumbers(l1, l2);
            Assert.That(result1.Item, Is.EqualTo(0));
            Assert.That(result1.Next.Item, Is.EqualTo(1));

            var result2 = AddTwoNumbersLinkedList.AddTwoNumbersDummyHead(l1, l2);
            Assert.That(result2.Item, Is.EqualTo(0));
            Assert.That(result2.Next.Item, Is.EqualTo(1));
        }
    }
}
