using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class StequeTest
    {
        [Test]
        public void EmptyStequeTest()
        {
            var s = new StequeLinkedList<int>();

            Assert.IsTrue(s.IsEmpty);
            Assert.AreEqual(0, s.Size);

            // If the steque is empty, it should throw an exception.
            Assert.Throws<NullReferenceException>(() => s.Pop());
        }

        [Test]
        public void PushPopOneElementTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);

            var val = s.Pop();
            Assert.AreEqual(1, val);
        }

        [Test]
        public void PushPopTwoElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);

            s.Push(2);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(2, s.Size);

            var val = s.Pop();
            Assert.AreEqual(2, val);

            val = s.Pop();
            Assert.AreEqual(1, val);
        }

        [Test]
        public void PushPopThreeElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);

            s.Push(2);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(2, s.Size);

            s.Push(3);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(3, s.Size);

            var val = s.Pop();
            Assert.AreEqual(3, val);

            val = s.Pop();
            Assert.AreEqual(2, val);

            val = s.Pop();
            Assert.AreEqual(1, val);
        }

        [Test]
        public void PushPopEnqueueMultipleElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);

            s.Push(2);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(2, s.Size);

            s.Enqueue(3);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(3, s.Size);

            var v = s.Pop();

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(2, s.Size);
            Assert.AreEqual(2, v);

            v = s.Pop();

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);
            Assert.AreEqual(1, v);

            v = s.Pop();

            Assert.IsTrue(s.IsEmpty);
            Assert.AreEqual(0, s.Size);
            Assert.AreEqual(3, v);

            Assert.Throws<NullReferenceException>(() => s.Pop());

            s.Enqueue(4);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);

            s.Enqueue(5);

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(2, s.Size);

            v = s.Pop();

            Assert.IsFalse(s.IsEmpty);
            Assert.AreEqual(1, s.Size);
            Assert.AreEqual(4, v);

            v = s.Pop();

            Assert.IsTrue(s.IsEmpty);
            Assert.AreEqual(0, s.Size);
            Assert.AreEqual(5, v);
        }

        [Test]
        public void EnqueuePopTest()
        {
            var s = new StequeLinkedList<int>();

            s.Enqueue(10);
            s.Enqueue(11);
            s.Enqueue(12);

            Assert.AreEqual(10, s.Pop());
            Assert.AreEqual(11, s.Pop());
            Assert.AreEqual(12, s.Pop());
        }

        [Test]
        public void MixedPushPopEnqueTest()
        {
            var s = new StequeLinkedList<int>();

            s.Enqueue(1);   // 1
            s.Push(2);      // 2,1
            s.Enqueue(3);   // 2,1,3
            s.Enqueue(4);   // 2,1,3,4
            s.Enqueue(5);   // 2,1,3,4,5
            s.Push(6);      // 6,2,1,3,4,5
            s.Push(7);      // 7,6,2,1,3,4,5
            s.Enqueue(8);   // 7,6,2,1,3,4,5,8
            s.Push(9);      // 9,7,6,2,1,3,4,5,8
            s.Enqueue(10);  // 9,7,6,2,1,3,4,5,8,10

            Assert.AreEqual(9, s.Pop());
            Assert.AreEqual(7, s.Pop());
            Assert.AreEqual(6, s.Pop());
            Assert.AreEqual(2, s.Pop());
            Assert.AreEqual(1, s.Pop());
            Assert.AreEqual(3, s.Pop());
            Assert.AreEqual(4, s.Pop());
            Assert.AreEqual(5, s.Pop());
            Assert.AreEqual(8, s.Pop());
            Assert.AreEqual(10, s.Pop());

            Assert.Throws<NullReferenceException>(() => s.Pop());
        }
    }
}
