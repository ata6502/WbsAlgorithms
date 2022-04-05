using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class StackTest
    {
        [Test]
        public void IsEmptyTest()
        {
            IsEmptyTest(new StackLinkedList<int>());
            IsEmptyTest(new StackArray<int>(5));
        }

        [Test]
        public void OneElement()
        {
            OneElement(new StackLinkedList<int>());
            OneElement(new StackArray<int>(1));
        }

        [Test]
        public void TwoElements()
        {
            TwoElements(new StackLinkedList<int>());
            TwoElements(new StackArray<int>(1));
        }

        [Test]
        public void ThreeElements()
        {
            ThreeElements(new StackLinkedList<int>());
            ThreeElements(new StackArray<int>(2));
        }

        [Test]
        public void MixedOperations()
        {
            MixedOperations(new StackLinkedList<int>());
            MixedOperations(new StackArray<int>(2));
        }

        [Test]
        public void CopyStack()
        {
            var stack = new StackLinkedList<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var copy = new StackLinkedList<int>(stack);

            Assert.IsFalse(stack.IsEmpty);
            Assert.IsFalse(copy.IsEmpty);

            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(3, copy.Size);

            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(3, copy.Pop());
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(2, copy.Size);

            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(2, copy.Pop());
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, copy.Size);

            Assert.AreEqual(1, stack.Pop());
            Assert.AreEqual(1, copy.Pop());
            Assert.AreEqual(0, stack.Size);
            Assert.AreEqual(0, copy.Size);
        }

        #region Actual tests with asserts
        private void IsEmptyTest<T>(IStack<T> stack)
        {
            Assert.IsTrue(stack.IsEmpty);
            Assert.AreEqual(0, stack.Size);

            // If the stack is empty, it should throw an exception.
            Assert.Throws<NullReferenceException>(() => stack.Pop());
        }

        private void OneElement(IStack<int> stack)
        {
            stack.Push(1);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, stack.Peek());

            var item = stack.Pop();
            Assert.AreEqual(1, item);
        }

        private void TwoElements(IStack<int> stack)
        {
            stack.Push(1);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, stack.Peek());

            stack.Push(2);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(2, stack.Peek());

            var item = stack.Pop();
            Assert.AreEqual(2, item);
            Assert.AreEqual(1, stack.Peek());

            item = stack.Pop();
            Assert.AreEqual(1, item);
        }

        private void ThreeElements(IStack<int> stack)
        {
            stack.Push(1);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(1, stack.Size);
            Assert.AreEqual(1, stack.Peek());

            stack.Push(2);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(2, stack.Peek());

            stack.Push(3);

            Assert.IsFalse(stack.IsEmpty);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(3, stack.Peek());

            var item = stack.Pop();
            Assert.AreEqual(3, item);
            Assert.AreEqual(2, stack.Peek());

            item = stack.Pop();
            Assert.AreEqual(2, item);
            Assert.AreEqual(1, stack.Peek());

            item = stack.Pop();
            Assert.AreEqual(1, item);
        }

        private void MixedOperations(IStack<int> stack)
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(1, stack.Size);

            stack.Push(4);
            stack.Push(5);
            Assert.AreEqual(3, stack.Size);
            Assert.AreEqual(5, stack.Pop());
            Assert.AreEqual(4, stack.Pop());
            Assert.AreEqual(1, stack.Size);

            stack.Push(6);
            Assert.AreEqual(2, stack.Size);
            Assert.AreEqual(6, stack.Pop());
            Assert.AreEqual(1, stack.Pop());

            Assert.IsTrue(stack.IsEmpty);
        }
        #endregion
    }
}
