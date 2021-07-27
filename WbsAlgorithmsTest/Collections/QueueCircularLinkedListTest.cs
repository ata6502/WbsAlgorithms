using NUnit.Framework;
using System;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    class QueueCircularLinkedListTest
    {
        [Test]
        public void EmptyQueueTest()
        {
            var queue = new QueueCircularLinkedList<int>();

            Assert.IsTrue(queue.IsEmpty);
            Assert.AreEqual(0, queue.Size);

            // If the queue is empty, it should throw an exception.
            Assert.Throws<ArgumentException>(() => queue.Dequeue());
        }

        [Test]
        public void OneElement()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(1, queue.Size);

            var item = queue.Dequeue();
            Assert.AreEqual(1, item);
        }

        [Test]
        public void TwoElements()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(1, queue.Size);

            queue.Enqueue(2);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(2, queue.Size);

            var item = queue.Dequeue();
            Assert.AreEqual(1, item);

            item = queue.Dequeue();
            Assert.AreEqual(2, item);
        }

        [Test]
        public void ThreeElements()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(1, queue.Size);

            queue.Enqueue(2);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(2, queue.Size);

            queue.Enqueue(3);

            Assert.IsFalse(queue.IsEmpty);
            Assert.AreEqual(3, queue.Size);

            var item = queue.Dequeue();
            Assert.AreEqual(1, item);

            item = queue.Dequeue();
            Assert.AreEqual(2, item);

            item = queue.Dequeue();
            Assert.AreEqual(3, item);
        }
    }
}
