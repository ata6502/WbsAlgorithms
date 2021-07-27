using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// A circular linked list has no null links and the value of the tail.next pointer
    /// points to the head whenever the list is not empty.
    /// 
    /// [Sedgewick] 1.3.29 p.165 - A queue implementation using a circular linked list.
    /// </summary>
    public class QueueCircularLinkedList<T>
    {
        // We need too keep just a single node - a pointer to the most recently added node.
        private ListNode<T> _tail;

        /// <summary>
        /// Indicates whether the queue is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _tail == null);

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Enqueue(T item)
        {
            // Create a new node.
            var newNode = new ListNode<T>(item, null);

            // Add the new node.
            if (IsEmpty)
            {
                // There is only one node in the list.
                newNode.Next = newNode;
            }
            else
            {
                // Add the new node to the end of the queue.
                // Also, wrap up the queue to make it circular.
                newNode.Next = _tail.Next;
                _tail.Next = newNode;
            }

            // Set the new last node.
            _tail = newNode;

            Size++;
        }

        /// <summary>
        /// Removes the least recently added item to the queue.
        /// </summary>
        /// <returns>The least recently added item</returns>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new ArgumentException("The queue is empty.");

            // Keep the value from the first node in the queue (tail.Next points to the first node).
            // We can use head.Next.Item because head.Next is never null in a circular linked list.
            // It always points to the linked list's head.
            var item = _tail.Next.Item;

            // Remove the node from the beginning of the list.
            if (Size == 1)
                // The list has only one node. Remove it.
                _tail = null;
            else
                // Wrapping up the list to the next node after the head.
                _tail.Next = _tail.Next.Next;

            Size--;

            return item;
        }
    }
}
