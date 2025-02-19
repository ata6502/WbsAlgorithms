using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;
using WbsAlgorithms.Common;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class SinglyLinkedListTest
    {
        [Test]
        public void InsertFirstTest()
        {
            ListNode<int> head = null;

            // Insert a node to an empty list.
            head = SinglyLinkedList.InsertFirst(head, new ListNode<int>(2));
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next, Is.Null);

            // Insert a node at the beginning.
            head = SinglyLinkedList.InsertFirst(head, new ListNode<int>(1));
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);
        }

        [Test]
        public void RemoveFirstTest()
        {
            ListNode<int> head = null;

            // Nothing to remove from an empty list.
            head = SinglyLinkedList.RemoveFirst(head);
            Assert.That(head, Is.Null);

            // Remove the first element from a one-element list.
            head = new ListNode<int>(1);
            head = SinglyLinkedList.RemoveFirst(head);
            Assert.That(head, Is.Null);

            // Remove the first element from a two-element list.
            head = new ListNode<int>(1, new ListNode<int>(2));
            head = SinglyLinkedList.RemoveFirst(head);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next, Is.Null);
        }

        [Test]
        public void AddLastTest()
        {
            ListNode<int> head = null;

            // Add a node at the end of an empty list.
            head = SinglyLinkedList.AddLast(head, new ListNode<int>(1));
            Assert.That(head.Item, Is.EqualTo(1));

            // Add a node at the end of a one-element list.
            head = new ListNode<int>(1);
            head = SinglyLinkedList.AddLast(head, new ListNode<int>(2));
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // Add a node at the end of a two-element list.
            head = new ListNode<int>(1, new ListNode<int>(2));
            head = SinglyLinkedList.AddLast(head, new ListNode<int>(3));
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Next, Is.Null);
        }

        [Test]
        public void RemoveLastTest()
        {
            ListNode<int> head = null;

            // An empty list.
            head = SinglyLinkedList.RemoveLast(head);
            Assert.That(head, Is.Null);

            // A one-element list.
            head = new ListNode<int>(1);
            head = SinglyLinkedList.RemoveLast(head);
            Assert.That(head, Is.Null);

            // A two-element list.
            head = new ListNode<int>(1, new ListNode<int>(2));
            head = SinglyLinkedList.RemoveLast(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // A three-element list.
            head = new ListNode<int>(1, new ListNode<int>(2, new ListNode<int>(3)));
            head = SinglyLinkedList.RemoveLast(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);
        }

        [Test]
        public void RemoveByIndexTest()
        {
            ListNode<int> head = null;

            // An empty list.
            head = SinglyLinkedList.RemoveByIndex(head, 0);
            Assert.That(head, Is.Null);

            // A one-element list - element found.
            head = new ListNode<int>(10);
            head = SinglyLinkedList.RemoveByIndex(head, 0);
            Assert.That(head, Is.Null);

            // A one-element list - element not found.
            head = new ListNode<int>(10);
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByIndex(head, 10)); // there is no element with index 10

            // A two-element list - remove first element.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            head = SinglyLinkedList.RemoveByIndex(head, 0);
            Assert.That(head.Item, Is.EqualTo(20));
            Assert.That(head.Next, Is.Null);

            // A two-element list - remove second element.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            head = SinglyLinkedList.RemoveByIndex(head, 1);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next, Is.Null);

            // A two-element list - element not found.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByIndex(head, 10));

            // A three-element list - remove first element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByIndex(head, 0);
            Assert.That(head.Item, Is.EqualTo(20));
            Assert.That(head.Next.Item, Is.EqualTo(30));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - remove middle element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByIndex(head, 1);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next.Item, Is.EqualTo(30));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - remove last element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByIndex(head, 2);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next.Item, Is.EqualTo(20));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - element not found.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByIndex(head, 10));
        }

        [Test]
        public void RemoveByValueTest()
        {
            ListNode<int> head = null;

            // An empty list.
            head = SinglyLinkedList.RemoveByValue(head, 0);
            Assert.That(head, Is.Null);

            // A one-element list - element found.
            head = new ListNode<int>(10);
            head = SinglyLinkedList.RemoveByValue(head, 10);
            Assert.That(head, Is.Null);

            // A one-element list - element not found.
            head = new ListNode<int>(10);
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByValue(head, 999));

            // A two-element list - remove first element.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            head = SinglyLinkedList.RemoveByValue(head, 10);
            Assert.That(head.Item, Is.EqualTo(20));
            Assert.That(head.Next, Is.Null);

            // A two-element list - remove second element.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            head = SinglyLinkedList.RemoveByValue(head, 20);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next, Is.Null);

            // A two-element list - element not found.
            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByValue(head, 999));

            // A three-element list - remove first element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByValue(head, 10);
            Assert.That(head.Item, Is.EqualTo(20));
            Assert.That(head.Next.Item, Is.EqualTo(30));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - remove middle element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByValue(head, 20);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next.Item, Is.EqualTo(30));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - remove last element.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            head = SinglyLinkedList.RemoveByValue(head, 30);
            Assert.That(head.Item, Is.EqualTo(10));
            Assert.That(head.Next.Item, Is.EqualTo(20));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - element not found.
            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            Assert.Throws<ArgumentException>(() => SinglyLinkedList.RemoveByValue(head, 999));
        }

        [Test]
        public void DoesNodeExist()
        {
            ListNode<int> head = null;

            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.False);

            head = new ListNode<int>(1);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);

            head = new ListNode<int>(1);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 8), Is.False);

            head = SinglyLinkedList.Create(new int[] { 1, 2 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 8), Is.False);

            head = SinglyLinkedList.Create(new int[] { 1, 1 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.False);

            head = SinglyLinkedList.Create(new int[] { 1, 1, 1 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.False);

            head = SinglyLinkedList.Create(new int[] { 2, 1, 1 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.True);

            head = SinglyLinkedList.Create(new int[] { 1, 2, 1 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.True);

            head = SinglyLinkedList.Create(new int[] { 1, 1, 2 });
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 1), Is.True);
            Assert.That(SinglyLinkedList.DoesNodeExist(head, 2), Is.True);
        }

        [Test]
        public void RemoveAfterTest()
        {
            ListNode<int> node = null;

            // Nothing to remove from an empty list.
            node = SinglyLinkedList.RemoveAfter(node);
            Assert.That(node, Is.Null);

            // Nothing to remove from a one-element list (there is no element to remove after the first element).
            node = new ListNode<int>(1);
            node = SinglyLinkedList.RemoveAfter(node);
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next, Is.Null);

            // Remove an element from a two-element list.
            node = new ListNode<int>(1, new ListNode<int>(2));
            node = SinglyLinkedList.RemoveAfter(node);
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next, Is.Null);
        }

        [Test]
        public void InsertAfterTest()
        {
            ListNode<int> node;

            // Do nothing if either argument is null.
            node = SinglyLinkedList.InsertAfter<int>(null, null);
            Assert.That(node, Is.Null);

            node = SinglyLinkedList.InsertAfter(new ListNode<int>(1), null);
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next, Is.Null);

            node = SinglyLinkedList.InsertAfter(null, new ListNode<int>(1));
            Assert.That(node, Is.Null);

            // Insert a node into a one element list.
            node = new ListNode<int>(1);
            node = SinglyLinkedList.InsertAfter(node, new ListNode<int>(2));
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next.Item, Is.EqualTo(2));
            Assert.That(node.Next.Next, Is.Null);

            // Insert a node into a two element list after the first element.
            node = new ListNode<int>(1, new ListNode<int>(2));
            node = SinglyLinkedList.InsertAfter(node, new ListNode<int>(3));
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next.Item, Is.EqualTo(3));
            Assert.That(node.Next.Next.Item, Is.EqualTo(2));
            Assert.That(node.Next.Next.Next, Is.Null);

            // Insert a node into a two element list after the second element.
            // InsertAfter returns the pointer to the second node.
            node = new ListNode<int>(1, new ListNode<int>(2));
            node = SinglyLinkedList.InsertAfter(node.Next, new ListNode<int>(3));
            Assert.That(node.Item, Is.EqualTo(2));
            Assert.That(node.Next.Item, Is.EqualTo(3));
            Assert.That(node.Next.Next, Is.Null);
        }

        [Test]
        public void RemoveAllByValueUsingWhileLoopTest() => RemoveAllByValueHelper((ListNode<int> head, int item) => SinglyLinkedList.RemoveAllByValueUsingWhileLoop(head, item));

        [Test]
        public void RemoveAllByValueUsingForLoopTest() => RemoveAllByValueHelper((ListNode<int> head, int item) => SinglyLinkedList.RemoveAllByValueUsingForLoop(head, item));

        // Signature: ListNode<int> removeAllByValue(ListNode<int> head, int item)
        private void RemoveAllByValueHelper(Func<ListNode<int>, int, ListNode<int>> removeAllByValue)
        {
            ListNode<int> head = null;

            // Nothing to remove from an empty list.
            head = removeAllByValue(head, 0);
            Assert.That(head, Is.Null);

            // Remove one element from a one element list.
            head = new ListNode<int>(1);
            head = removeAllByValue(head, 1);
            Assert.That(head, Is.Null);

            // Do not remove anything from a one element list.
            head = new ListNode<int>(1);
            head = removeAllByValue(head, 8);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove the first element from a two element list.
            head = SinglyLinkedList.Create(new int[] { 1, 2 });
            head = removeAllByValue(head, 1);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next, Is.Null);

            // Remove the second element from a two element list.
            head = SinglyLinkedList.Create(new int[] { 1, 2 });
            head = removeAllByValue(head, 2);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove 2nd and 3rd element from a list.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 2 });
            head = removeAllByValue(head, 2);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove 1st and 2nd element from a list.
            head = SinglyLinkedList.Create(new int[] { 2, 2, 1 });
            head = removeAllByValue(head, 2);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove 1st and 3rd element from a list.
            head = SinglyLinkedList.Create(new int[] { 2, 1, 2 });
            head = removeAllByValue(head, 2);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove 2nd element from a list.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 1 });
            head = removeAllByValue(head, 2);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);

            // Remove all three elements from a list.
            head = SinglyLinkedList.Create(new int[] { 2, 2, 2 });
            head = removeAllByValue(head, 2);
            Assert.That(head, Is.Null);

            // Do not remove anything from a list. 
            head = SinglyLinkedList.Create(new int[] { 3, 4, 5 });
            head = removeAllByValue(head, 8);
            Assert.That(head.Item, Is.EqualTo(3));
            Assert.That(head.Next.Item, Is.EqualTo(4));
            Assert.That(head.Next.Next.Item, Is.EqualTo(5));
            Assert.That(head.Next.Next.Next, Is.Null);
        }

        [Test]
        public void FindMaxValueTest()
        {
            ListNode<int> head = SinglyLinkedList.Create(new[] { 1 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(1));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(1));

            head = SinglyLinkedList.Create(new[] { 1, 2 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(2));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(2));

            head = SinglyLinkedList.Create(new[] { 2, 1 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(2));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(2));

            head = SinglyLinkedList.Create(new[] { 1, 3, 2 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(3));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(3));

            head = SinglyLinkedList.Create(new[] { 1, 2, 3 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(3));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(3));

            head = SinglyLinkedList.Create(new[] { 3, 2, 1 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(3));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(3));

            head = SinglyLinkedList.Create(new[] { 3, 7, 4, 14, 6, 10, 13, 1 });
            Assert.That(SinglyLinkedList.FindMaxValueIteratively(head), Is.EqualTo(14));
            Assert.That(SinglyLinkedList.FindMaxValueRecusively(head), Is.EqualTo(14));
        }

        [Test]
        public void ReverseIterativelyTest()
        {
            // Reverse an empty list.
            ListNode<int> head = null;
            var reversed = SinglyLinkedList.ReverseIteratively(head);

            Assert.That(reversed, Is.Null);

            // Reverse a one-node list.
            head = new ListNode<int>(0, null);
            reversed = SinglyLinkedList.ReverseIteratively(head);

            Assert.That(reversed.Item, Is.EqualTo(0));
            Assert.That(reversed.Next, Is.Null);

            // Reverse a two-node list.
            head = SinglyLinkedList.Create(new int[] { 0, 1 });
            reversed = SinglyLinkedList.ReverseIteratively(head);

            Assert.That(reversed.Item, Is.EqualTo(1));
            Assert.That(reversed.Next.Item, Is.EqualTo(0));
            Assert.That(reversed.Next.Next, Is.Null);

            // Reverse a five-node list.
            head = SinglyLinkedList.Create(new int[] { 0, 1, 2, 3, 4 });
            reversed = SinglyLinkedList.ReverseIteratively(head);

            Assert.That(reversed.Item, Is.EqualTo(4));
            Assert.That(reversed.Next.Item, Is.EqualTo(3));
            Assert.That(reversed.Next.Next.Item, Is.EqualTo(2));
            Assert.That(reversed.Next.Next.Next.Item, Is.EqualTo(1));
            Assert.That(reversed.Next.Next.Next.Next.Item, Is.EqualTo(0));
            Assert.That(reversed.Next.Next.Next.Next.Next, Is.Null);
        }

        [Test]
        public void ReverseRecursivelyTest()
        {
            // Reverse an empty list.
            ListNode<int> head = null;
            var reversed = SinglyLinkedList.ReverseRecursively(head);

            Assert.That(reversed, Is.Null);

            // Reverse a one-node list.
            head = new ListNode<int>(0, null);
            reversed = SinglyLinkedList.ReverseRecursively(head);

            Assert.That(reversed.Item, Is.EqualTo(0));
            Assert.That(reversed.Next, Is.Null);

            // Reverse a two-node list.
            head = SinglyLinkedList.Create(new int[] { 0, 1 });
            reversed = SinglyLinkedList.ReverseRecursively(head);

            Assert.That(reversed.Item, Is.EqualTo(1));
            Assert.That(reversed.Next.Item, Is.EqualTo(0));
            Assert.That(reversed.Next.Next, Is.Null);

            // Reverse a five-node list.
            head = SinglyLinkedList.Create(new int[] { 0, 1, 2, 3, 4 });
            reversed = SinglyLinkedList.ReverseRecursively(head);

            Assert.That(reversed.Item, Is.EqualTo(4));
            Assert.That(reversed.Next.Item, Is.EqualTo(3));
            Assert.That(reversed.Next.Next.Item, Is.EqualTo(2));
            Assert.That(reversed.Next.Next.Next.Item, Is.EqualTo(1));
            Assert.That(reversed.Next.Next.Next.Next.Item, Is.EqualTo(0));
            Assert.That(reversed.Next.Next.Next.Next.Next, Is.Null);
        }

        [Test]
        public void RemoveDuplicatesTest() => RemoveDuplicatesTestHelper(SinglyLinkedList.RemoveDuplicates);

        [Test]
        public void RemoveDuplicatesUsingRunnerTest() => RemoveDuplicatesTestHelper(SinglyLinkedList.RemoveDuplicatesUsingRunner);

        private void RemoveDuplicatesTestHelper(Func<ListNode<int>, ListNode<int>> func)
        {
            ListNode<int> head = null;

            // An empty list.
            head = func(head);
            Assert.That(head, Is.Null);

            // A one-element list.
            head = new ListNode<int>(1);
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // A two-element list - no duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 2 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // A two-element list - one duplicate.
            head = SinglyLinkedList.Create(new int[] { 1, 1 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // A three-element list - no duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 3 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Next, Is.Null);

            // A three-element list - one duplicate.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 2 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // A three-element list - two duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 1, 1 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // A four-element list - one duplicate.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 3, 2 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Next, Is.Null);

            // A four-element list - one duplicate.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 2, 3 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Next, Is.Null);

            // A four-element list - two duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 2, 1 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // A four-element list - two duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 2, 2 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // A four-element list - three duplicates.
            head = SinglyLinkedList.Create(new int[] { 1, 1, 1, 1 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // A six-element list - one duplicate.
            head = SinglyLinkedList.Create(new int[] { 1, 2, 3, 4, 3, 5 });
            head = func(head);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Next.Item, Is.EqualTo(4));
            Assert.That(head.Next.Next.Next.Next.Item, Is.EqualTo(5));
            Assert.That(head.Next.Next.Next.Next.Next, Is.Null);
        }

        [Test]
        public void FindFromLastTest()
        {
            ListNode<int> head = null;

            // An empty list.
            Assert.That(SinglyLinkedList.FindFromLast(head, 1), Is.Null);

            head = SinglyLinkedList.Create(new int[] { 10, 20 });
            Assert.That(SinglyLinkedList.FindFromLast(head, 1).Item, Is.EqualTo(20));
            Assert.That(SinglyLinkedList.FindFromLast(head, 2).Item, Is.EqualTo(10));
            Assert.That(SinglyLinkedList.FindFromLast(head, 3), Is.Null);

            head = SinglyLinkedList.Create(new int[] { 10, 20, 30 });
            Assert.That(SinglyLinkedList.FindFromLast(head, 1).Item, Is.EqualTo(30));
            Assert.That(SinglyLinkedList.FindFromLast(head, 2).Item, Is.EqualTo(20));
            Assert.That(SinglyLinkedList.FindFromLast(head, 3).Item, Is.EqualTo(10));
            Assert.That(SinglyLinkedList.FindFromLast(head, 4), Is.Null);

            head = SinglyLinkedList.Create(new int[] { 10, 20, 30, 40 });
            Assert.That(SinglyLinkedList.FindFromLast(head, 1).Item, Is.EqualTo(40));
            Assert.That(SinglyLinkedList.FindFromLast(head, 2).Item, Is.EqualTo(30));
            Assert.That(SinglyLinkedList.FindFromLast(head, 3).Item, Is.EqualTo(20));
            Assert.That(SinglyLinkedList.FindFromLast(head, 4).Item, Is.EqualTo(10));
            Assert.That(SinglyLinkedList.FindFromLast(head, 5), Is.Null);

            head = SinglyLinkedList.Create(new int[] { 10, 20, 30, 40, 50 });
            Assert.That(SinglyLinkedList.FindFromLast(head, 1).Item, Is.EqualTo(50));
            Assert.That(SinglyLinkedList.FindFromLast(head, 2).Item, Is.EqualTo(40));
            Assert.That(SinglyLinkedList.FindFromLast(head, 3).Item, Is.EqualTo(30));
            Assert.That(SinglyLinkedList.FindFromLast(head, 4).Item, Is.EqualTo(20));
            Assert.That(SinglyLinkedList.FindFromLast(head, 5).Item, Is.EqualTo(10));
            Assert.That(SinglyLinkedList.FindFromLast(head, 7), Is.Null);
        }

        [Test]
        public void DeleteMiddleNodeTest()
        {
            ListNode<int> head = null;

            // An empty list.
            Assert.That(SinglyLinkedList.DeleteMiddleNode(head), Is.Null);

            head = SinglyLinkedList.Create(new int[] { 1, 2, 3 });

            Assert.Throws<ArgumentException>(() => SinglyLinkedList.DeleteMiddleNode(head.Next.Next)); // try to remove the last node
            SinglyLinkedList.DeleteMiddleNode(head.Next); // 2
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next, Is.Null);

            head = SinglyLinkedList.Create(new int[] { 1, 2, 3, 4, 5 });

            SinglyLinkedList.DeleteMiddleNode(head.Next.Next); // 3
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Item, Is.EqualTo(4));
            Assert.That(head.Next.Next.Next.Item, Is.EqualTo(5));
            Assert.That(head.Next.Next.Next.Next, Is.Null);

            SinglyLinkedList.DeleteMiddleNode(head.Next); // 2
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(4));
            Assert.That(head.Next.Next.Item, Is.EqualTo(5));
            Assert.That(head.Next.Next.Next, Is.Null);

            SinglyLinkedList.DeleteMiddleNode(head); // 1
            Assert.That(head.Item, Is.EqualTo(4));
            Assert.That(head.Next.Item, Is.EqualTo(5));
            Assert.That(head.Next.Next, Is.Null);
        }

        [TestCase(new int[] { 1 }, 1, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, 1, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2 }, 2, new int[] { 1, 2 })]
        [TestCase(new int[] { 3, 1, 2 }, 3, new int[] { 2, 1, 3 })]
        [TestCase(new int[] { 3, 1, 2 }, 1, new int[] { 3, 1, 2 })]
        [TestCase(new int[] { 3, 1, 2 }, 2, new int[] { 1, 3, 2 })]
        [TestCase(new int[] { 3, 5, 8, 5, 9, 2, 1 }, 5, new int[] { 1, 2, 3, 5, 8, 5, 9 })]
        [TestCase(new int[] { 3, 5, 8, 5, 9, 2, 1 }, 8, new int[] { 1, 2, 5, 5, 3, 8, 9 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 3, new int[] { 2, 1, 5, 6, 3, 6, 7, 3, 4, 8, 9, 5 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 6, new int[] { 2, 5, 4, 3, 3, 1, 5, 6, 6, 7, 8, 9 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 8, new int[] { 2, 5, 4, 3, 7, 6, 3, 1, 6, 5, 8, 9 })]
        public void PartitionTest(int[] inputList, int partitionItem, int[] expectedList)
        {
            var linkedList = SinglyLinkedList.Create(inputList);

            var node = SinglyLinkedList.Partition(linkedList, partitionItem);
            foreach (var item in expectedList)
            {
                Assert.That(node.Item, Is.EqualTo(item));
                node = node.Next;
            }
        }

        [TestCase(new int[] { 1 }, 1, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, 1, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2 }, 2, new int[] { 1, 2 })]
        [TestCase(new int[] { 3, 1, 2 }, 3, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 1, 2 }, 1, new int[] { 3, 1, 2 })]
        [TestCase(new int[] { 3, 1, 2 }, 2, new int[] { 1, 3, 2 })]
        [TestCase(new int[] { 3, 5, 8, 5, 9, 2, 1 }, 5, new int[] { 3, 2, 1, 5, 8, 5, 9 })]
        [TestCase(new int[] { 3, 5, 8, 5, 9, 2, 1 }, 8, new int[] { 3, 5, 5, 2, 1, 8, 9 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 3, new int[] { 1, 2, 5, 6, 3, 6, 7, 3, 4, 8, 9, 5 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 6, new int[] { 5, 1, 3, 3, 4, 5, 2, 6, 6, 7, 8, 9 })]
        [TestCase(new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 8, 9, 5, 2 }, 8, new int[] { 5, 6, 1, 3, 6, 7, 3, 4, 5, 2, 8, 9 })]
        public void PartitionPreserveOrderTest(int[] inputList, int partitionItem, int[] expectedList)
        {
            var linkedList = SinglyLinkedList.Create(inputList);

            var node = SinglyLinkedList.PartitionPreserveOrder(linkedList, partitionItem);
            foreach (var item in expectedList)
            {
                Assert.That(node.Item, Is.EqualTo(item));
                node = node.Next;
            }
        }
    }
}