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

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(copy.IsEmpty, Is.False);

            Assert.That(stack.Size, Is.EqualTo(3));
            Assert.That(copy.Size, Is.EqualTo(3));

            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(copy.Pop(), Is.EqualTo(3));
            Assert.That(stack.Size, Is.EqualTo(2));
            Assert.That(copy.Size, Is.EqualTo(2));

            Assert.That(stack.Pop(), Is.EqualTo(2));
            Assert.That(copy.Pop(), Is.EqualTo(2));
            Assert.That(stack.Size, Is.EqualTo(1));
            Assert.That(copy.Size, Is.EqualTo(1));

            Assert.That(stack.Pop(), Is.EqualTo(1));
            Assert.That(copy.Pop(), Is.EqualTo(1));
            Assert.That(stack.Size, Is.EqualTo(0));
            Assert.That(copy.Size, Is.EqualTo(0));
        }

        #region Actual tests with asserts
        private void IsEmptyTest<T>(IStack<T> stack)
        {
            Assert.That(stack.IsEmpty, Is.True);
            Assert.That(stack.Size, Is.EqualTo(0));

            // If the stack is empty, it should throw an exception.
            Assert.Throws<NullReferenceException>(() => stack.Pop());
        }

        private void OneElement(IStack<int> stack)
        {
            stack.Push(1);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(1));

            var item = stack.Pop();
            Assert.That(item, Is.EqualTo(1));
        }

        private void TwoElements(IStack<int> stack)
        {
            stack.Push(1);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(1));

            stack.Push(2);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(2));

            var item = stack.Pop();
            Assert.That(item, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(1));

            item = stack.Pop();
            Assert.That(item, Is.EqualTo(1));
        }

        private void ThreeElements(IStack<int> stack)
        {
            stack.Push(1);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(1));

            stack.Push(2);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(2));

            stack.Push(3);

            Assert.That(stack.IsEmpty, Is.False);
            Assert.That(stack.Size, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(3));

            var item = stack.Pop();
            Assert.That(item, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(2));

            item = stack.Pop();
            Assert.That(item, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(1));

            item = stack.Pop();
            Assert.That(item, Is.EqualTo(1));
        }

        private void MixedOperations(IStack<int> stack)
        {
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.That(stack.Size, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(2));
            Assert.That(stack.Size, Is.EqualTo(1));

            stack.Push(4);
            stack.Push(5);
            Assert.That(stack.Size, Is.EqualTo(3));
            Assert.That(stack.Pop(), Is.EqualTo(5));
            Assert.That(stack.Pop(), Is.EqualTo(4));
            Assert.That(stack.Size, Is.EqualTo(1));

            stack.Push(6);
            Assert.That(stack.Size, Is.EqualTo(2));
            Assert.That(stack.Pop(), Is.EqualTo(6));
            Assert.That(stack.Pop(), Is.EqualTo(1));

            Assert.That(stack.IsEmpty, Is.True);
        }
        #endregion
    }
}
