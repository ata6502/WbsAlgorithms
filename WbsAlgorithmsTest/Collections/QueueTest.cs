using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class QueueTest
    {
        [Test]
        public void IsEmptyTest()
        {
            var queue = new QueueLinkedList<int>();

            Assert.IsTrue(queue.IsEmpty);

            queue.Enqueue(1);
            queue.Dequeue();

            Assert.IsTrue(queue.IsEmpty);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();

            Assert.IsFalse(queue.IsEmpty);
        }

        [Test]
        public void EnqueueDequeueTest()
        {
            var queue = new QueueLinkedList<int>();

            queue.Enqueue(1);
            Assert.AreEqual(1, queue.Size);

            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Size);

            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Size);

            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(2, queue.Size);

            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(1, queue.Size);

            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(0, queue.Size);

            // The queue is empty. It should throw an exception.
            Assert.Throws<ArgumentException>(() => queue.Dequeue());
        }

        [Test]
        public void CopyTest()
        {
            var queue = new QueueLinkedList<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            var copy = new QueueLinkedList<int>(queue);

            Assert.AreEqual(3, queue.Size);
            Assert.AreEqual(3, copy.Size);

            // Assert that the original queue and the copy are independent.
            queue.Enqueue(4);
            Assert.AreEqual(4, queue.Size);

            Assert.AreEqual(1, copy.Dequeue());
            Assert.AreEqual(2, copy.Size);

            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(3, queue.Size);

            Assert.AreEqual(2, copy.Dequeue());
            Assert.AreEqual(1, copy.Size);

            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(2, queue.Size);

            Assert.AreEqual(3, copy.Dequeue());
            Assert.IsTrue(copy.IsEmpty);

            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(1, queue.Size);

            Assert.AreEqual(4, queue.Dequeue());
            Assert.IsTrue(queue.IsEmpty);
        }

        [Test]
        public void ReverseTest()
        {
            var queue = new QueueLinkedList<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            queue.Reverse();

            Assert.AreEqual(3, queue.Size);
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
        }

        [Test]
        public void RandomQueueTest()
        {
            var queue = new QueueRandom<string>(2);

            // Enqueue a few item.
            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Enqueue("C");

            var item = queue.Dequeue();

            // The item can be any of the items.
            Assert.IsTrue(item == "A" || item == "B" || item == "C");

            // The Sample method may return any item in the queue
            // except the the one that has been dequeued.
            var sample = queue.Sample();

            Assert.AreNotEqual(item, sample);
        }
    }
}
