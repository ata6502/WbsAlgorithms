using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class DequeTest
    {
        [Test]
        public void EmptyDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            Assert.That(deque.IsEmpty, Is.True);
            Assert.That(deque.Size, Is.EqualTo(0));

            Assert.Throws<ArgumentException>(() => deque.PopLeft());
            Assert.Throws<ArgumentException>(() => deque.PopRight());
        }

        [Test]
        public void OneElementDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushRight(1);
            Assert.That(deque.PopRight(), Is.EqualTo(1));
            Assert.That(deque.IsEmpty, Is.True);

            deque.PushRight(1);
            Assert.That(deque.PopLeft(), Is.EqualTo(1));
            Assert.That(deque.IsEmpty, Is.True);

            deque.PushLeft(1);
            Assert.That(deque.PopRight(), Is.EqualTo(1));
            Assert.That(deque.IsEmpty, Is.True);

            deque.PushLeft(1);
            Assert.That(deque.PopLeft(), Is.EqualTo(1));
            Assert.That(deque.IsEmpty, Is.True);
        }

        [Test]
        public void TwoElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(1);
            deque.PushRight(2);
            Assert.That(deque.PopLeft(), Is.EqualTo(1));
            Assert.That(deque.PopRight(), Is.EqualTo(2));
            Assert.That(deque.IsEmpty, Is.True);
        }

        [Test]
        public void ThreeElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(2);
            deque.PushRight(3);
            deque.PushLeft(1);
            Assert.That(deque.PopLeft(), Is.EqualTo(1));
            Assert.That(deque.PopRight(), Is.EqualTo(3));
            Assert.That(deque.PopLeft(), Is.EqualTo(2));
            Assert.That(deque.IsEmpty, Is.True);
        }

        [Test]
        public void MixedElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(3);
            deque.PushLeft(2);
            deque.PushLeft(1);
            Assert.That(deque.Size, Is.EqualTo(3));

            Assert.That(deque.PopLeft(), Is.EqualTo(1));
            Assert.That(deque.Size, Is.EqualTo(2));

            Assert.That(deque.PopRight(), Is.EqualTo(3));
            Assert.That(deque.Size, Is.EqualTo(1));

            deque.PushRight(4);
            deque.PushRight(5);
            Assert.That(deque.Size, Is.EqualTo(3));

            Assert.That(deque.PopLeft(), Is.EqualTo(2));
            Assert.That(deque.Size, Is.EqualTo(2));

            Assert.That(deque.PopLeft(), Is.EqualTo(4));
            Assert.That(deque.Size, Is.EqualTo(1));

            Assert.That(deque.PopLeft(), Is.EqualTo(5));
            Assert.That(deque.Size, Is.EqualTo(0));

            Assert.That(deque.IsEmpty, Is.True);
        }
    }
}
