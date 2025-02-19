using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
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

            Assert.That(q1.Count, Is.EqualTo(0));
            Assert.That(q2.Count, Is.EqualTo(0));
            Assert.That(c.FirstItem.Item, Is.EqualTo(0));
            Assert.That(c.FirstItem.Next.Item, Is.EqualTo(1));
            Assert.That(c.ToString(), Is.EqualTo("0 1 2 3 4 5"));
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

            Assert.That(s1.Count, Is.EqualTo(0));
            Assert.That(s2.Count, Is.EqualTo(0));
            Assert.That(c.FirstItem.Item, Is.EqualTo(2));
            Assert.That(c.FirstItem.Next.Item, Is.EqualTo(1));
            Assert.That(c.ToString(), Is.EqualTo("2 1 0 5 4 3"));
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

            Assert.That(s1.Size, Is.EqualTo(0));
            Assert.That(s2.Size, Is.EqualTo(0));
            Assert.That(c.FirstItem.Item, Is.EqualTo(2));
            Assert.That(c.FirstItem.Next.Item, Is.EqualTo(1));
            Assert.That(c.ToString(), Is.EqualTo("2 1 0 3 4 5"));
        }

        [Test]
        public void ConcatenateEmptyQueuesTest()
        {
            var q1 = new Queue<int>();
            var q2 = new Queue<int>();
            var c1 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c1.FirstItem, Is.Null);

            q1 = new Queue<int>();
            q1.Enqueue(0);
            q1.Enqueue(1);
            q1.Enqueue(2);
            q2 = new Queue<int>();

            var c2 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c2.ToString(), Is.EqualTo("0 1 2"));

            q1 = new Queue<int>();
            q2 = new Queue<int>();
            q2.Enqueue(3);
            q2.Enqueue(4);
            q2.Enqueue(5);

            var c3 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c3.ToString(), Is.EqualTo("3 4 5"));

            q1 = null;
            q2 = null;
            var c4 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c4.FirstItem, Is.Null);

            q1 = new Queue<int>();
            q1.Enqueue(0);
            q1.Enqueue(1);
            q1.Enqueue(2);
            q2 = null;

            var c5 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c5.ToString(), Is.EqualTo("0 1 2"));

            q1 = null;
            q2 = new Queue<int>();
            q2.Enqueue(3);
            q2.Enqueue(4);
            q2.Enqueue(5);

            var c6 = Catenation.ConcatenateQueues(q1, q2);
            Assert.That(c6.ToString(), Is.EqualTo("3 4 5"));
        }

        [Test]
        public void ConcatenateEmptyStacksTest()
        {
            var s1 = new Stack<int>();
            var s2 = new Stack<int>();
            var c1 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c1.FirstItem, Is.Null);

            s1 = new Stack<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = new Stack<int>();

            var c2 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c2.ToString(), Is.EqualTo("2 1 0"));

            s1 = new Stack<int>();
            s2 = new Stack<int>();
            s2.Push(3);
            s2.Push(4);
            s2.Push(5);

            var c3 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c3.ToString(), Is.EqualTo("5 4 3"));

            s1 = null;
            s2 = null;
            var c4 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c4.FirstItem, Is.Null);

            s1 = new Stack<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = null;

            var c5 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c5.ToString(), Is.EqualTo("2 1 0"));

            s1 = null;
            s2 = new Stack<int>();
            s2.Push(3);
            s2.Push(4);
            s2.Push(5);

            var c6 = Catenation.ConcatenateStacks(s1, s2);
            Assert.That(c6.ToString(), Is.EqualTo("5 4 3"));
        }

        [Test]
        public void ConcatenateEmptyStequesTest()
        {
            var s1 = new StequeLinkedList<int>();
            var s2 = new StequeLinkedList<int>();
            var c1 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c1.FirstItem, Is.Null);

            s1 = new StequeLinkedList<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = new StequeLinkedList<int>();

            var c2 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c2.ToString(), Is.EqualTo("2 1 0"));

            s1 = new StequeLinkedList<int>();
            s2 = new StequeLinkedList<int>();
            s2.Enqueue(3);
            s2.Enqueue(4);
            s2.Enqueue(5);

            var c3 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c3.ToString(), Is.EqualTo("3 4 5"));

            s1 = null;
            s2 = null;
            var c4 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c4.FirstItem, Is.Null);

            s1 = new StequeLinkedList<int>();
            s1.Push(0);
            s1.Push(1);
            s1.Push(2);
            s2 = null;

            var c5 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c5.ToString(), Is.EqualTo("2 1 0"));

            s1 = null;
            s2 = new StequeLinkedList<int>();
            s2.Enqueue(3);
            s2.Enqueue(4);
            s2.Enqueue(5);

            var c6 = Catenation.ConcatenateSteques(s1, s2);
            Assert.That(c3.ToString(), Is.EqualTo("3 4 5"));
        }
    }
}
