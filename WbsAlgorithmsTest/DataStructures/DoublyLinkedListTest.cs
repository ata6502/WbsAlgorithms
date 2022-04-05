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
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.IsNull(head.Next);

            // Insert a node to a one-node list.
            head = DoublyLinkedList.InsertFirst(
                DoublyLinkedList.Create(new int[] { 2 }), 
                new DoubleListNode<int>(1));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);

            // Insert a node to a two-node list.
            head = DoublyLinkedList.InsertFirst(
                DoublyLinkedList.Create(new int[] { 2, 3 }),
                new DoubleListNode<int>(1));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.AreEqual(3, head.Next.Next.Item);
            Assert.AreEqual(2, head.Next.Next.Prev.Item);
            Assert.IsNull(head.Next.Next.Next);
        }

        [Test]
        public static void InsertLastTest()
        {
            DoubleListNode<int> head;

            // Insert a node to an empty list.
            head = DoublyLinkedList.InsertLast(null, new DoubleListNode<int>(1));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.IsNull(head.Next);

            // Insert a node to a one-node list.
            head = DoublyLinkedList.InsertLast(
                DoublyLinkedList.Create(new int[] { 1 }),
                new DoubleListNode<int>(2));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);

            // Insert a node to a two-node list.
            head = DoublyLinkedList.InsertLast(
                DoublyLinkedList.Create(new int[] { 1, 2 }),
                new DoubleListNode<int>(3));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.AreEqual(3, head.Next.Next.Item);
            Assert.AreEqual(2, head.Next.Next.Prev.Item);
            Assert.IsNull(head.Next.Next.Next);
        }

        [Test]
        public static void RemoveFirstTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.RemoveFirst(head);
            Assert.IsNull(head);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1 }));
            Assert.IsNull(head);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1, 2 }));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(2, head.Item);
            Assert.IsNull(head.Next);

            head = DoublyLinkedList.RemoveFirst(DoublyLinkedList.Create(new int[] { 1, 2, 3 }));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(2, head.Item);
            Assert.AreEqual(3, head.Next.Item);
            Assert.AreEqual(2, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);
        }

        [Test]
        public static void RemoveLastTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.RemoveLast(head);
            Assert.IsNull(head);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1 }));
            Assert.IsNull(head);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1, 2 }));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.IsNull(head.Next);

            head = DoublyLinkedList.RemoveLast(DoublyLinkedList.Create(new int[] { 1, 2, 3 }));
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);
        }

        [Test]
        public static void InsertBeforeTest()
        {
            // If the node is null, InsertBefore should throw an exception.
            Assert.Throws<ArgumentNullException>(() => DoublyLinkedList.InsertBefore(null, new DoubleListNode<int>(1)));

            var list = DoublyLinkedList.Create(new int[] { 2 });
            var newNode = DoublyLinkedList.InsertBefore(list, new DoubleListNode<int>(1));
            Assert.IsNull(newNode.Prev);
            Assert.AreEqual(1, newNode.Item);
            Assert.AreEqual(2, newNode.Next.Item);
            Assert.AreEqual(1, newNode.Next.Prev.Item);
            Assert.IsNull(newNode.Next.Next);

            list = DoublyLinkedList.Create(new int[] { 1, 3 });
            newNode = DoublyLinkedList.InsertBefore(list.Next, new DoubleListNode<int>(2)); // insert before 3

            Assert.IsNull(list.Prev);
            Assert.AreEqual(1, list.Item);
            Assert.AreEqual(2, list.Next.Item);
            Assert.AreEqual(1, list.Next.Prev.Item);
            Assert.AreEqual(3, list.Next.Next.Item);
            Assert.AreEqual(2, list.Next.Next.Prev.Item);
            Assert.IsNull(list.Next.Next.Next);

            Assert.AreEqual(1, newNode.Prev.Item);
            Assert.AreEqual(2, newNode.Item);
            Assert.AreEqual(3, newNode.Next.Item);
            Assert.AreEqual(2, newNode.Next.Prev.Item);
            Assert.IsNull(newNode.Next.Next);
        }

        [Test]
        public static void InsertAfterTest()
        {
            // If the node is null, InsertAfter should throw an exception.
            Assert.Throws<ArgumentNullException>(() => DoublyLinkedList.InsertAfter(null, new DoubleListNode<int>(1)));

            var list = DoublyLinkedList.Create(new int[] { 1 });
            var node = DoublyLinkedList.InsertAfter(list, new DoubleListNode<int>(2)); // node is the same as list

            Assert.IsNull(list.Prev);
            Assert.AreEqual(1, list.Item);
            Assert.AreEqual(2, list.Next.Item);
            Assert.AreEqual(1, list.Next.Prev.Item);
            Assert.IsNull(list.Next.Next);

            Assert.IsNull(node.Prev);
            Assert.AreEqual(1, node.Item);
            Assert.AreEqual(2, node.Next.Item);
            Assert.AreEqual(1, node.Next.Prev.Item);
            Assert.IsNull(node.Next.Next);

            list = DoublyLinkedList.Create(new int[] { 1, 2, 4 });
            node = DoublyLinkedList.InsertAfter(list.Next, new DoubleListNode<int>(3)); // insert after 2; node is list.Next

            Assert.IsNull(list.Prev);
            Assert.AreEqual(1, list.Item);
            Assert.AreEqual(2, list.Next.Item);
            Assert.AreEqual(1, list.Next.Prev.Item);
            Assert.AreEqual(3, list.Next.Next.Item);
            Assert.AreEqual(2, list.Next.Next.Prev.Item);
            Assert.AreEqual(4, list.Next.Next.Next.Item);
            Assert.AreEqual(3, list.Next.Next.Next.Prev.Item);
            Assert.IsNull(list.Next.Next.Next.Next);

            // node is list.Next i.e., 2
            Assert.AreEqual(1, node.Prev.Item);
            Assert.AreEqual(2, node.Item);
            Assert.AreEqual(3, node.Next.Item);
            Assert.AreEqual(2, node.Next.Prev.Item);
        }

        [Test]
        public static void RemoveTest()
        {
            DoubleListNode<int> head = null;

            head = DoublyLinkedList.Remove(head, head);
            Assert.IsNull(head);

            // Remove the only node in a list.
            head = DoublyLinkedList.Create(new int[] { 1 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.IsNull(head);

            // Remove the first node (the head).
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.IsNull(head.Prev);
            Assert.AreEqual(2, head.Item);
            Assert.IsNull(head.Next);

            // Remove the second element.
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            head = DoublyLinkedList.Remove(head, head.Next);
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.IsNull(head.Next);

            // Remove the first node (the head).
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head);
            Assert.IsNull(head.Prev);
            Assert.AreEqual(2, head.Item);
            Assert.AreEqual(3, head.Next.Item);
            Assert.AreEqual(2, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);

            // Remove the second element.
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head.Next);
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(3, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);

            // Remove the third element.
            head = DoublyLinkedList.Create(new int[] { 1, 2, 3 });
            head = DoublyLinkedList.Remove(head, head.Next.Next);
            Assert.IsNull(head.Prev);
            Assert.AreEqual(1, head.Item);
            Assert.AreEqual(2, head.Next.Item);
            Assert.AreEqual(1, head.Next.Prev.Item);
            Assert.IsNull(head.Next.Next);

            // If the node is not found, InsertAfter should throw an exception.
            head = DoublyLinkedList.Create(new int[] { 1, 2 });
            Assert.Throws<ArgumentException>(() => DoublyLinkedList.Remove(head, new DoubleListNode<int>(3)));
        }
    }
}
