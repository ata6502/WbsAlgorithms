using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// A doubly-linked list enables arbitrary insertions and deletions unlike singly-linked list
    /// which requires traversing the list each time we want to insert or delete a node. It is undesirable 
    /// because it takes time proportional to the length of the list.
    /// 
    /// [Sedgewick] 1.3.31 p.166 - Implement doubly-linked list with the following operations:
    /// - Insert a node at the beginning (InsertFirst).
    /// - Insert a node at the end (InsertLast).
    /// - Remove a node from the beginning (RemoveFirst).
    /// - Remove a node from the end (RemoveLast).
    /// - Insert a node before a given node (InsertBefore).
    /// - Insert a node after a given node (InsertAfter).
    /// - Remove a given node (Remove).
    /// </summary>
    public class DoublyLinkedList
    {
        /// <summary>
        /// Insert a node at the beginning of a doubly-linked list. 
        /// </summary>
        /// <param name="head">The head of the list</param>
        /// <param name="node">The node to insert</param>
        /// <returns>The head of the list</returns>
        public static DoubleListNode<T> InsertFirst<T>(DoubleListNode<T> head, DoubleListNode<T> node)
        {
            // Nothing to insert.
            if (node == null)
                return head;

            // The list is empty. Insert the first node.
            if (head == null)
            {
                head = node;
                head.Prev = null;
                head.Next = null;
                return head;
            }

            // Insert the new node at the beginning.
            node.Next = head;

            // Make sure that the old head references the new node as its previous node.
            head.Prev = node;

            // Set the new node as the head of the list.
            head = node;

            return head;
        }

        /// <summary>
        /// Insert a node at the end of a doubly-linked list.
        /// </summary>
        /// <param name="head">The head of the list</param>
        /// <param name="node">The node to insert</param>
        /// <returns>The head of the list</returns>
        public static DoubleListNode<T> InsertLast<T>(DoubleListNode<T> head, DoubleListNode<T> node)
        {
            // Nothing to insert.
            if (node == null)
                return head;

            // The list is empty. Insert the first node.
            if (head == null)
            {
                head = node;
                head.Prev = null;
                head.Next = null;
                return head;
            }

            // Find the last node.
            var last = head;
            while (last.Next != null)
                last = last.Next;

            // Insert the new node at the end.
            node.Prev = last;
            node.Next = null;
            last.Next = node;

            return head;
        }

        /// <summary>
        /// Remove a node from the beginning of a doubly-linked list.
        /// </summary>
        /// <param name="head">The head of the list</param>
        /// <returns>The head of the list</returns>
        public static DoubleListNode<T> RemoveFirst<T>(DoubleListNode<T> head)
        {
            // Nothing to remove.
            if (head == null)
                return null;

            // If the list has one node, remove it.
            if (head.Next == null)
            {
                head = null;
                return head;
            }

            // Remove the first node.
            head = head.Next;
            head.Prev = null;

            return head;
        }

        /// <summary>
        /// Remove a node from the end of a doubly-linked list.
        /// </summary>
        /// <param name="head">The head of the list</param>
        /// <returns>The head of the list</returns>
        public static DoubleListNode<T> RemoveLast<T>(DoubleListNode<T> head)
        {
            // Nothing to remove.
            if (head == null)
                return null;

            // If the list has one node, remove it.
            if (head.Next == null)
            {
                head = null;
                return head;
            }

            // Find the last node.
            var last = head;
            while (last.Next != null)
                last = last.Next;

            // Remove the last node.
            last.Prev.Next = null;

            return head;
        }

        /// <summary>
        /// Insert a node to a doubly-linked list before a given node.
        /// </summary>
        /// <param name="node">The node before which we want to insert the new node</param>
        /// <param name="newNode">The new node to insert</param>
        /// <returns>The new node</returns>
        public static DoubleListNode<T> InsertBefore<T>(DoubleListNode<T> node, DoubleListNode<T> newNode)
        {
            if (node == null || newNode == null)
                throw new ArgumentNullException("The input node is null.");

            // Keep the previous node of the given node.
            var prev = node.Prev;

            // Update the given node.
            node.Prev = newNode;

            // Update the newly inserted node.
            newNode.Next = node;
            newNode.Prev = prev;

            // Update the node before the new node.
            if (prev != null)
                prev.Next = newNode;

            return newNode;
        }

        /// <summary>
        /// Insert a node to a doubly-linked list after a given node.
        /// </summary>
        /// <param name="node">The node after which we want to insert the new node</param>
        /// <param name="newNode">The new node to insert</param>
        /// <returns>The given node</returns>
        public static DoubleListNode<T> InsertAfter<T>(DoubleListNode<T> node, DoubleListNode<T> newNode)
        {
            if (node == null || newNode == null)
                throw new ArgumentNullException("The input node is null.");

            // Keep the next node of the given node. It could be null if
            // the given node is the last one in the list.
            var next = node.Next;

            // Insert the new node between the given node and the next node.
            newNode.Prev = node;
            newNode.Next = next;

            // Update the newly inserted node.
            node.Next = newNode;

            // Update the new node before the next node if it exists.
            if (next != null)
                next.Prev = newNode;

            return node;
        }

        /// <summary>
        /// Remove a node from a doubly-linked list.
        /// </summary>
        /// <param name="head">The head of the list</param>
        /// <param name="node">A node to remove</param>
        /// <returns>The head of the list</returns>
        public static DoubleListNode<T> Remove<T>(DoubleListNode<T> head, DoubleListNode<T> node)
        {
            if (head == null)
                return null;

            // Nothing to remove.
            if (node == null)
                return head;

            // Try to find the node to remove.
            var r = head;
            while (r != node && r != null)
                r = r.Next;

            if (r == null)
                throw new ArgumentException("The node to remove not found.");

            // Check if the node to remove is the only node in the list.
            if (r.Prev == null && r.Next == null)
            {
                head = null;
            }
            // Check if the node to remove is the first node i.e., if it is the head.
            else if (r.Prev == null)
            {
                head = head.Next;
                head.Prev = null;
            }
            // Check if the node to remove is the last node i.e., if it is the tail.
            else if (r.Next == null)
            {
                r.Prev.Next = null;
            }
            else
            {
                // Otherwise the node to remove is somewhere in the list with
                // one node on the left and another node on the right.
                r.Prev.Next = r.Next;
                r.Next.Prev = r.Prev;
            }

            return head;
        }

        /// <summary>
        /// Creates a doubly-linked list and populates it with given items.
        /// </summary>
        /// <param name="items">Items to populate the linked list</param>
        /// <returns>The head of the newly created linked list</returns>
        public static DoubleListNode<T> Create<T>(T[] items)
        {
            DoubleListNode<T> head = null;
            DoubleListNode<T> prev = null;

            foreach (var item in items)
            {
                var node = new DoubleListNode<T>(item);

                if (prev == null)
                    head = node;
                else
                {
                    prev.Next = node;
                    node.Prev = prev;
                }

                prev = node;
            }

            return head;
        }
    }
}
