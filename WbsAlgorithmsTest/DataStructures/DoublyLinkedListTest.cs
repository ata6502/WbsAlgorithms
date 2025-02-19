using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;
using WbsAlgorithms.Common;

namespace WbsAlgorithmsTest.DataStructures
{
    class DoublyLinkedListTest
    {
        [Test]
        public static void InsertFirstTest()
        {
            DoubleListNode<int> head;

            // Insert a node to an empty list.
            head = DoublyLinkedList.InsertFirst(null, new DoubleListNode<int>(1));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Insert a node to a one-node list.
            head = DoublyLinkedList.InsertFirst(
                DoublyLinkedList.Create(new int[] { 2 }), 
                new DoubleListNode<int>(1));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);

            // Insert a node to a two-node list.
            head = DoublyLinkedList.InsertFirst(
                DoublyLinkedList.Create(new int[] { 2, 3 }),
                new DoubleListNode<int>(1));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Next, Is.Null);
        }

        [Test]
        public static void InsertLastTest()
        {
            DoubleListNode<int> head;

            // Insert a node to an empty list.
            head = DoublyLinkedList.InsertLast(null, new DoubleListNode<int>(1));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Insert a node to a one-node list.
            head = DoublyLinkedList.InsertLast(
                DoublyLinkedList.Create(new int[] { 1 }),
                new DoubleListNode<int>(2));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);

            // Insert a node to a two-node list.
            head = DoublyLinkedList.InsertLast(
                DoublyLinkedList.Create(new int[] { 1, 2 }),
                new DoubleListNode<int>(3));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next.Next, Is.Null);
        }

        [Test]
        public static void RemoveFirstTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.RemoveFirst(head);
            Assert.That(head, Is.Null);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1 }));
            Assert.That(head, Is.Null);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1, 2 }));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next, Is.Null);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1, 2, 3 }));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);
        }

        [Test]
        public static void RemoveLastTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.RemoveLast(head);
            Assert.That(head, Is.Null);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1 }));
            Assert.That(head, Is.Null);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1, 2 }));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1, 2, 3 }));
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);
        }

        [Test]
        public static void InsertBeforeTest()
        {
            // If the node is null, InsertBefore should throw an exception.
            Assert.Throws<ArgumentNullException>(() => DoublyLinkedList.InsertBefore(null, new DoubleListNode<int>(1)));

            var list = DoublyLinkedList.Create(new int[] { 2 });
            var newNode = DoublyLinkedList.InsertBefore(list, new DoubleListNode<int>(1));
            Assert.That(newNode.Prev, Is.Null);
            Assert.That(newNode.Item, Is.EqualTo(1));
            Assert.That(newNode.Next.Item, Is.EqualTo(2));
            Assert.That(newNode.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(newNode.Next.Next, Is.Null);

            list = DoublyLinkedList.Create(new int[] { 1, 3 });
            newNode = DoublyLinkedList.InsertBefore(list.Next, new DoubleListNode<int>(2)); // insert before 3

            Assert.That(list.Prev, Is.Null);
            Assert.That(list.Item, Is.EqualTo(1));
            Assert.That(list.Next.Item, Is.EqualTo(2));
            Assert.That(list.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(list.Next.Next.Item, Is.EqualTo(3));
            Assert.That(list.Next.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(list.Next.Next.Next, Is.Null);

            Assert.That(newNode.Prev.Item, Is.EqualTo(1));
            Assert.That(newNode.Item, Is.EqualTo(2));
            Assert.That(newNode.Next.Item, Is.EqualTo(3));
            Assert.That(newNode.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(newNode.Next.Next, Is.Null);
        }

        [Test]
        public static void InsertAfterTest()
        {
            // If the node is null, InsertAfter should throw an exception.
            Assert.Throws<ArgumentNullException>(() => DoublyLinkedList.InsertAfter(null, new DoubleListNode<int>(1)));

            var list = DoublyLinkedList.Create(new int[] { 1 });
            var node = DoublyLinkedList.InsertAfter(list, new DoubleListNode<int>(2)); // node is the same as list

            Assert.That(list.Prev, Is.Null);
            Assert.That(list.Item, Is.EqualTo(1));
            Assert.That(list.Next.Item, Is.EqualTo(2));
            Assert.That(list.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(list.Next.Next, Is.Null);

            Assert.That(node.Prev, Is.Null);
            Assert.That(node.Item, Is.EqualTo(1));
            Assert.That(node.Next.Item, Is.EqualTo(2));
            Assert.That(node.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(node.Next.Next, Is.Null);

            list = DoublyLinkedList.Create(new int[] { 1, 2, 4 });
            node = DoublyLinkedList.InsertAfter(list.Next, new DoubleListNode<int>(3)); // insert after 2; node is list.Next

            Assert.That(list.Prev, Is.Null);
            Assert.That(list.Item, Is.EqualTo(1));
            Assert.That(list.Next.Item, Is.EqualTo(2));
            Assert.That(list.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(list.Next.Next.Item, Is.EqualTo(3));
            Assert.That(list.Next.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(list.Next.Next.Next.Item, Is.EqualTo(4));
            Assert.That(list.Next.Next.Next.Prev.Item, Is.EqualTo(3));
            Assert.That(list.Next.Next.Next.Next, Is.Null);

            // node is list.Next i.e., 2
            Assert.That(node.Prev.Item, Is.EqualTo(1));
            Assert.That(node.Item, Is.EqualTo(2));
            Assert.That(node.Next.Item, Is.EqualTo(3));
            Assert.That(node.Next.Prev.Item, Is.EqualTo(2));
        }

        [Test]
        public static void RemoveTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.Remove(head, head);
            Assert.That(head, Is.Null);

            // Remove the only node in a list.
            head = DoublyLinkedList.Create(new int[] { 1 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.That(head, Is.Null);

            // Remove the first node (the head).
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next, Is.Null);

            // Remove the second element.
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            head = DoublyLinkedList.Remove(head, head.Next);
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next, Is.Null);

            // Remove the first node (the head).
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(2));
            Assert.That(head.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(2));
            Assert.That(head.Next.Next, Is.Null);

            // Remove the second element.
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head.Next);
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(3));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);

            // Remove the third element.
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head.Next.Next);
            Assert.That(head.Prev, Is.Null);
            Assert.That(head.Item, Is.EqualTo(1));
            Assert.That(head.Next.Item, Is.EqualTo(2));
            Assert.That(head.Next.Prev.Item, Is.EqualTo(1));
            Assert.That(head.Next.Next, Is.Null);

            // If the node is not found, InsertAfter should throw an exception.
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            Assert.Throws<ArgumentException>(() => DoublyLinkedList.Remove(head, new DoubleListNode<int>(3)));
        }
    }
}
