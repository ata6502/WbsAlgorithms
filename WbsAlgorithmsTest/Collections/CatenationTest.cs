using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    public class CatenationTest
    {
        [Test]
        public void ConcatenateQueuesTest()
        {
            var q1 = new Queue<int>();
            q1.Enqueue(0);
            q1.Enqueue(1);
            q1.Enqueue(2);

            var q2 = new Queue<int>();
            q2.Enqueue(3);
            q2.Enqueue(4);
            q2.Enqueue(5);

            var c = Catenation.ConcatenateQueues(q1, q2);

            Assert.AreEqual(0, q1.Count);
            Assert.AreEqual(0, q2.Count);
            Assert.AreEqual(0, c.FirstItem.Item);
            Assert.AreEqual(1, c.FirstItem.Next.Item);

            Assert.AreEqual("0 1 2 3 4 5", c.ToString());
        }

        [Test]
        public void ConcatenateStacksTest()
        {
            var s1 = new Stack<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);

            var s2 = new Stack<int>();
            s2.Push(3);
            s2.Push(4);
            s2.Push(5);

            var c = Catenation.ConcatenateStacks(s1, s2);

            Assert.AreEqual(0, s1.Count);
            Assert.AreEqual(0, s2.Count);
            Assert.AreEqual(2, c.FirstItem.Item);
            Assert.AreEqual(1, c.FirstItem.Next.Item);

            Assert.AreEqual("2 1 0 5 4 3", c.ToString());
        }

        [Test]
        public void ConcatenateStequesTest()
        {
            var s1 = new StequeLinkedList<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);

            var s2 = new StequeLinkedList<int>();
            s2.Enqueue(3);
            s2.Enqueue(4);
            s2.Enqueue(5);

            var c = Catenation.ConcatenateSteques(s1, s2);

            Assert.AreEqual(0, s1.Size);
            Assert.AreEqual(0, s2.Size);
            Assert.AreEqual(2, c.FirstItem.Item);
            Assert.AreEqual(1, c.FirstItem.Next.Item);

            Assert.AreEqual("2 1 0 3 4 5", c.ToString());
        }

        [Test]
        public void ConcatenateEmptyQueuesTest()
        {
            var q1 = new Queue<int>();
            var q2 = new Queue<int>();
            var c1 = Catenation.ConcatenateQueues(q1, q2);
            Assert.IsNull(c1.FirstItem);

            q1 = new Queue<int>();
            q1.Enqueue(0);
            q1.Enqueue(1);
            q1.Enqueue(2);
            q2 = new Queue<int>();

            var c2 = Catenation.ConcatenateQueues(q1, q2);
            Assert.AreEqual("0 1 2", c2.ToString());

            q1 = new Queue<int>();
            q2 = new Queue<int>();
            q2.Enqueue(3);
            q2.Enqueue(4);
            q2.Enqueue(5);

            var c3 = Catenation.ConcatenateQueues(q1, q2);
            Assert.AreEqual("3 4 5", c3.ToString());

            q1 = null;
            q2 = null;
            var c4 = Catenation.ConcatenateQueues(q1, q2);
            Assert.IsNull(c4.FirstItem);

            q1 = new Queue<int>();
            q1.Enqueue(0);
            q1.Enqueue(1);
            q1.Enqueue(2);
            q2 = null;

            var c5 = Catenation.ConcatenateQueues(q1, q2);
            Assert.AreEqual("0 1 2", c5.ToString());

            q1 = null;
            q2 = new Queue<int>();
            q2.Enqueue(3);
            q2.Enqueue(4);
            q2.Enqueue(5);

            var c6 = Catenation.ConcatenateQueues(q1, q2);
            Assert.AreEqual("3 4 5", c6.ToString());
        }

        [Test]
        public void ConcatenateEmptyStacksTest()
        {
            var s1 = new Stack<int>();
            var s2 = new Stack<int>();
            var c1 = Catenation.ConcatenateStacks(s1, s2);
            Assert.IsNull(c1.FirstItem);

            s1 = new Stack<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = new Stack<int>();

            var c2 = Catenation.ConcatenateStacks(s1, s2);
            Assert.AreEqual("2 1 0", c2.ToString());

            s1 = new Stack<int>();
            s2 = new Stack<int>();
            s2.Push(3);
            s2.Push(4);
            s2.Push(5);

            var c3 = Catenation.ConcatenateStacks(s1, s2);
            Assert.AreEqual("5 4 3", c3.ToString());

            s1 = null;
            s2 = null;
            var c4 = Catenation.ConcatenateStacks(s1, s2);
            Assert.IsNull(c4.FirstItem);

            s1 = new Stack<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = null;

            var c5 = Catenation.ConcatenateStacks(s1, s2);
            Assert.AreEqual("2 1 0", c5.ToString());

            s1 = null;
            s2 = new Stack<int>();
            s2.Push(3);
            s2.Push(4);
            s2.Push(5);

            var c6 = Catenation.ConcatenateStacks(s1, s2);
            Assert.AreEqual("5 4 3", c6.ToString());
        }

        [Test]
        public void ConcatenateEmptyStequesTest()
        {
            var s1 = new StequeLinkedList<int>();
            var s2 = new StequeLinkedList<int>();
            var c1 = Catenation.ConcatenateSteques(s1, s2);
            Assert.IsNull(c1.FirstItem);

            s1 = new StequeLinkedList<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = new StequeLinkedList<int>();

            var c2 = Catenation.ConcatenateSteques(s1, s2);
            Assert.AreEqual("2 1 0", c2.ToString());

            s1 = new StequeLinkedList<int>();
            s2 = new StequeLinkedList<int>();
            s2.Enqueue(3);
            s2.Enqueue(4);
            s2.Enqueue(5);

            var c3 = Catenation.ConcatenateSteques(s1, s2);
            Assert.AreEqual("3 4 5", c3.ToString());

            s1 = null;
            s2 = null;
            var c4 = Catenation.ConcatenateSteques(s1, s2);
            Assert.IsNull(c4.FirstItem);

            s1 = new StequeLinkedList<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = null;

            var c5 = Catenation.ConcatenateSteques(s1, s2);
            Assert.AreEqual("2 1 0", c5.ToString());

            s1 = null;
            s2 = new StequeLinkedList<int>();
            s2.Enqueue(3);
            s2.Enqueue(4);
            s2.Enqueue(5);

            var c6 = Catenation.ConcatenateSteques(s1, s2);
            Assert.AreEqual("3 4 5", c3.ToString());
        }
    }
}
