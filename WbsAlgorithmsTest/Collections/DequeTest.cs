using NUnit.Framework;
using System;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    class DequeTest
    {
        [Test]
        public void EmptyDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            Assert.IsTrue(deque.IsEmpty);
            Assert.AreEqual(0, deque.Size);

            Assert.Throws<ArgumentException>(() => deque.PopLeft());
            Assert.Throws<ArgumentException>(() => deque.PopRight());
        }

        [Test]
        public void OneElementDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushRight(1);
            Assert.AreEqual(1, deque.PopRight());
            Assert.IsTrue(deque.IsEmpty);

            deque.PushRight(1);
            Assert.AreEqual(1, deque.PopLeft());
            Assert.IsTrue(deque.IsEmpty);

            deque.PushLeft(1);
            Assert.AreEqual(1, deque.PopRight());
            Assert.IsTrue(deque.IsEmpty);

            deque.PushLeft(1);
            Assert.AreEqual(1, deque.PopLeft());
            Assert.IsTrue(deque.IsEmpty);
        }

        [Test]
        public void TwoElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(1);
            deque.PushRight(2);
            Assert.AreEqual(1, deque.PopLeft());
            Assert.AreEqual(2, deque.PopRight());
            Assert.IsTrue(deque.IsEmpty);
        }

        [Test]
        public void ThreeElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(2);
            deque.PushRight(3);
            deque.PushLeft(1);
            Assert.AreEqual(1, deque.PopLeft());
            Assert.AreEqual(3, deque.PopRight());
            Assert.AreEqual(2, deque.PopLeft());
            Assert.IsTrue(deque.IsEmpty);
        }

        [Test]
        public void MixedElementsDequeTest()
        {
            var deque = new DequeLinkedList<int>();

            deque.PushLeft(3);
            deque.PushLeft(2);
            deque.PushLeft(1);
            Assert.AreEqual(3, deque.Size);

            Assert.AreEqual(1, deque.PopLeft());
            Assert.AreEqual(2, deque.Size);

            Assert.AreEqual(3, deque.PopRight());
            Assert.AreEqual(1, deque.Size);

            deque.PushRight(4);
            deque.PushRight(5);
            Assert.AreEqual(3, deque.Size);

            Assert.AreEqual(2, deque.PopLeft());
            Assert.AreEqual(2, deque.Size);

            Assert.AreEqual(4, deque.PopLeft());
            Assert.AreEqual(1, deque.Size);

            Assert.AreEqual(5, deque.PopLeft());
            Assert.AreEqual(0, deque.Size);

            Assert.IsTrue(deque.IsEmpty);
        }
    }
}
