using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class QueueCircularLinkedListTest
    {
        [Test]
        public void EmptyQueueTest()
        {
            var queue = new QueueCircularLinkedList<int>();

            Assert.That(queue.IsEmpty, Is.True);
            Assert.That(queue.Size, Is.EqualTo(0));

            // If the queue is empty, it should throw an exception.
            Assert.Throws<ArgumentException>(() => queue.Dequeue());
        }

        [Test]
        public void OneElement()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(1));

            var item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(1));
        }

        [Test]
        public void TwoElements()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(1));

            queue.Enqueue(2);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(2));

            var item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(1));

            item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(2));
        }

        [Test]
        public void ThreeElements()
        {
            var queue = new QueueCircularLinkedList<int>();

            queue.Enqueue(1);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(1));

            queue.Enqueue(2);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(2));

            queue.Enqueue(3);

            Assert.That(queue.IsEmpty, Is.False);
            Assert.That(queue.Size, Is.EqualTo(3));

            var item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(1));

            item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(2));

            item = queue.Dequeue();
            Assert.That(item, Is.EqualTo(3));
        }
    }
}
