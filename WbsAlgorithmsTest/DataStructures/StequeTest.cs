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

            Assert.That(s.IsEmpty, Is.True);
            Assert.That(s.Size, Is.EqualTo(0));

            // If the steque is empty, it should throw an exception.
            Assert.Throws<NullReferenceException>(() => s.Pop());
        }

        [Test]
        public void PushPopOneElementTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));

            var val = s.Pop();
            Assert.That(val, Is.EqualTo(1));
        }

        [Test]
        public void PushPopTwoElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));

            s.Push(2);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(2));

            var val = s.Pop();
            Assert.That(val, Is.EqualTo(2));

            val = s.Pop();
            Assert.That(val, Is.EqualTo(1));
        }

        [Test]
        public void PushPopThreeElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));

            s.Push(2);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(2));

            s.Push(3);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(3));

            var val = s.Pop();
            Assert.That(val, Is.EqualTo(3));

            val = s.Pop();
            Assert.That(val, Is.EqualTo(2));

            val = s.Pop();
            Assert.That(val, Is.EqualTo(1));
        }

        [Test]
        public void PushPopEnqueueMultipleElementsTest()
        {
            var s = new StequeLinkedList<int>();

            s.Push(1);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));

            s.Push(2);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(2));

            s.Enqueue(3);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(3));

            var v = s.Pop();

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(2));
            Assert.That(v, Is.EqualTo(2));

            v = s.Pop();

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));
            Assert.That(v, Is.EqualTo(1));

            v = s.Pop();

            Assert.That(s.IsEmpty, Is.True);
            Assert.That(s.Size, Is.EqualTo(0));
            Assert.That(v, Is.EqualTo(3));

            Assert.Throws<NullReferenceException>(() => s.Pop());

            s.Enqueue(4);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));

            s.Enqueue(5);

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(2));

            v = s.Pop();

            Assert.That(s.IsEmpty, Is.False);
            Assert.That(s.Size, Is.EqualTo(1));
            Assert.That(v, Is.EqualTo(4));

            v = s.Pop();

            Assert.That(s.IsEmpty, Is.True);
            Assert.That(s.Size, Is.EqualTo(0));
            Assert.That(v, Is.EqualTo(5));
        }

        [Test]
        public void EnqueuePopTest()
        {
            var s = new StequeLinkedList<int>();

            s.Enqueue(10);
            s.Enqueue(11);
            s.Enqueue(12);

            Assert.That(s.Pop(), Is.EqualTo(10));
            Assert.That(s.Pop(), Is.EqualTo(11));
            Assert.That(s.Pop(), Is.EqualTo(12));
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

            Assert.That(s.Pop(), Is.EqualTo(9));
            Assert.That(s.Pop(), Is.EqualTo(7));
            Assert.That(s.Pop(), Is.EqualTo(6));
            Assert.That(s.Pop(), Is.EqualTo(2));
            Assert.That(s.Pop(), Is.EqualTo(1));
            Assert.That(s.Pop(), Is.EqualTo(3));
            Assert.That(s.Pop(), Is.EqualTo(4));
            Assert.That(s.Pop(), Is.EqualTo(5));
            Assert.That(s.Pop(), Is.EqualTo(8));
            Assert.That(s.Pop(), Is.EqualTo(10));

            Assert.Throws<NullReferenceException>(() => s.Pop());
        }
    }
}
