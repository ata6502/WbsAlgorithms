using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// GeneralizedQueue is a kind of queue that allows deleting a k-th item.
    /// This implementation uses a linked list to store items. As a consequence:
    /// - Finding the k-th item may be slow as it requires traversing the linked list.
    /// - Deletion is fast as it only requires re-assigning a reference.
    ///
    /// [Sedgewick] 1.3.38 p.169 - Implement a collection that supports the following API:
    /// - Add an item to the queue (Insert).
    /// - Delete and return the k-th least recently inserted item (Delete).
    /// </summary>
    public class QueueGeneralizedUsingLinkedList<T> : IQueueGeneralized<T>
    {
        private ListNode<T> _head; // a pointer to the least recently added node
        private ListNode<T> _tail; // a pointer to the most recently added node

        /// <summary>
        /// Indicates whether the queue is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _head == null && _tail == null);

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Adds an item to the queue. It is the same as the queue's Enqueue operation.
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            // Create a new node.
            var newNode = new ListNode<T>(item, null);

            // Add the new node to the end of the list.
            if (IsEmpty)
                _head = newNode;
            else
                _tail.Next = newNode;

            // Set the new last node.
            _tail = newNode;

            Size++;
        }

        /// <summary>
        /// Deletes and returns the k-th least recently inserted item.
        /// The implementation of this method is based on
        /// [Sedgewick] 1.3.20 - Remove a node from a linked list at a given index (RemoveByIndex).
        /// </summary>
        /// <param name="k">The index k</param>
        /// <returns>The k-th least recently inserted item</returns>
        public T Delete(int k)
        {
            // Check if the list is empty.
            if (IsEmpty)
                throw new ArgumentOutOfRangeException($"The queue is empty.");

            T item;

            // Check if we need to remove the first node.
            if (k == 0)
            {
                // Get the value of the first node. This is the value we need to return.
                item = _head.Item;

                // Remove the first node.
                _head = _head.Next;

                // If the list is empty, clear the pointer to the last node. 
                if (_head == null)
                    _tail = null;
            }
            else
            {
                // Find the node just before the k-th node.
                var node = _head;
                var n = 0;
                while (n < k - 1 && node != null)
                {
                    node = node.Next;
                    ++n;
                }

                // If the node is not found it means the input index k-th is out of range.
                if (node == null)
                    throw new ArgumentOutOfRangeException($"The element with the index {k} does not exist in the queue.");

                // Get the value of the k-th node. This is the value we need to return.
                item = node.Next.Item;

                // node.Next is our k-th node. Remove it.
                node.Next = node.Next.Next;

                // Adjust the list's pointer to the last node if the new k-th node is the last one.
                if (node.Next == null)
                    _tail = node;
            }

            Size--;

            return item;
        }
    }
}
