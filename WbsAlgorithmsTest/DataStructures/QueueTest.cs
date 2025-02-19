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

            Assert.That(queue.IsEmpty, Is.True);

            queue.Enqueue(1);
            queue.Dequeue();

            Assert.That(queue.IsEmpty, Is.True);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();

            Assert.That(queue.IsEmpty, Is.False);
        }

        [Test]
        public void EnqueueDequeueTest()
        {
            var queue = new QueueLinkedList<int>();

            queue.Enqueue(1);
            Assert.That(queue.Size, Is.EqualTo(1));

            queue.Enqueue(2);
            Assert.That(queue.Size, Is.EqualTo(2));

            queue.Enqueue(3);
            Assert.That(queue.Size, Is.EqualTo(3));

            Assert.That(queue.Dequeue(), Is.EqualTo(1));
            Assert.That(queue.Size, Is.EqualTo(2));

            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            Assert.That(queue.Size, Is.EqualTo(1));

            Assert.That(queue.Dequeue(), Is.EqualTo(3));
            Assert.That(queue.Size, Is.EqualTo(0));

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

            Assert.That(queue.Size, Is.EqualTo(3));
            Assert.That(copy.Size, Is.EqualTo(3));

            // Assert that the original queue and the copy are independent.
            queue.Enqueue(4);
            Assert.That(queue.Size, Is.EqualTo(4));

            Assert.That(copy.Dequeue(), Is.EqualTo(1));
            Assert.That(copy.Size, Is.EqualTo(2));

            Assert.That(queue.Dequeue(), Is.EqualTo(1));
            Assert.That(queue.Size, Is.EqualTo(3));

            Assert.That(copy.Dequeue(), Is.EqualTo(2));
            Assert.That(copy.Size, Is.EqualTo(1));

            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            Assert.That(queue.Size, Is.EqualTo(2));

            Assert.That(copy.Dequeue(), Is.EqualTo(3));
            Assert.That(copy.IsEmpty);

            Assert.That(queue.Dequeue(), Is.EqualTo(3));
            Assert.That(queue.Size, Is.EqualTo(1));

            Assert.That(queue.Dequeue(), Is.EqualTo(4));
            Assert.That(queue.IsEmpty);
        }

        [Test]
        public void ReverseTest()
        {
            var queue = new QueueLinkedList<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            queue.Reverse();

            Assert.That(queue.Size, Is.EqualTo(3));
            Assert.That(queue.Dequeue(), Is.EqualTo(3));
            Assert.That(queue.Dequeue(), Is.EqualTo(2));
            Assert.That(queue.Dequeue(), Is.EqualTo(1));
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
            Assert.That(item == "A" || item == "B" || item == "C", Is.True);

            // The Sample method may return any item in the queue
            // except the the one that has been dequeued.
            var sample = queue.Sample();

            Assert.That(sample, Is.Not.EqualTo(item));
        }
    }
}
