using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// Steque is a stack-ended queue. It supports Push, Pop, and Enqueue operations.
    /// This implementation uses a singly-linked list to store items.
    /// Knuth calls the steque an output-restricted deque.
    /// 
    /// [Sedgewick] 1.3.32 p.167 - Implement a steque data structure that supports the following API:
    /// - Add an item to the top of the steque (Push).
    /// - Remove the most recently added item (Pop).
    /// - Add an item to the bottom of the steque (Enqueue).
    /// </summary>
    public class StequeLinkedList<T>
    {
        private ListNode<T> _head; // a pointer to the least recently added node; the top of the steque
        private ListNode<T> _tail; // a pointer to the most recently added node; the bottom of the steque

        /// <summary>
        /// Indicates whether the steque is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _head == null && _tail == null);

        /// <summary>
        /// Returns the number of items in the steque.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        ///  Adds an item to the top of the steque.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Push(T item)
        {
            // Create a new head of the linked list providing the current head as its next node.
            var newHead = new ListNode<T>(item, _head);

            // Check if this is the first node. If so, adjust the last node.
            if (_head == null)
                _tail = newHead;

            // Assign the list's head to the new node i.e., add the item to the top of the steque.
            _head = newHead;

            ++Size;
        }

        /// <summary>
        /// Removes the most recently added item.
        /// </summary>
        /// <returns>The most recently added item</returns>
        public T Pop()
        {
            // Throw an exception if the steque is empty.
            if (IsEmpty)
                throw new NullReferenceException("The steque is empty.");

            // Keep the value of the linked list's head. We need to return it.
            var item = _head.Item;

            // Remove the node from the beginning of the linked list.
            _head = _head.Next;

            // Check if there are any nodes in the linked list. If not, adjust the last node.
            if (_head == null)
                _tail = null;

            --Size;

            return item;
        }

        /// <summary>
        /// Adds an item to the bottom of the steque.
        /// </summary>
        /// <param name="item">The item to add</param>
        public void Enqueue(T item)
        {
            // Create a new node.
            var newNode = new ListNode<T>(item, null);

            if (IsEmpty)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                // Add the new node at the end of the linked list.
                _tail.Next = newNode;
                _tail = newNode;
            }

            ++Size;
        }
    }
}
