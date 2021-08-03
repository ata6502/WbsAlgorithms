using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// Deque is a double-ended queue. It's like a stack or a queue
    /// but it supports adding and removing items at both ends.
    /// This implementation uses a doubly-linked list to store items.
    /// 
    /// [Sedgewick] 1.3.33 p.167 - Implement a deque data structure that supports the following API:
    /// - Add an item to the left end (PushLeft).
    /// - Add an item to the right end (PushRight).
    /// - Remove an item from the left end (PopLeft).
    /// - Remove an item from the right end (PopRight).
    /// </summary>
    public class DequeLinkedList<T>
    {
        private DoubleListNode<T> _head; // the first element in the deque
        private DoubleListNode<T> _tail; // the last element in the deque

        /// <summary>
        /// Indicates whether the deque is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _head == null && _tail == null);

        /// <summary>
        /// Returns the number of items in the deque.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Adds an item to the left end of the deque.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void PushLeft(T item)
        {
            if (IsEmpty)
            {
                _head = new DoubleListNode<T>(item, null, null);
                _tail = _head;
            }
            else
            {
                // Create a new node and insert it at the beginning of the list.
                var newNode = new DoubleListNode<T>(item, null, _head);
                _head.Prev = newNode;

                // Set the new node as the list's head.
                _head = newNode;
            }

            ++Size;
        }

        /// <summary>
        /// Adds an item to the right end of the deque.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void PushRight(T item)
        {
            if (IsEmpty)
            {
                _head = new DoubleListNode<T>(item, null, null);
                _tail = _head;
            }
            else
            {
                // Create a new node and insert it at the end of the list.
                var newNode = new DoubleListNode<T>(item, _tail, null);
                _tail.Next = newNode;

                // Set the new node as the list's last node.
                _tail = newNode;
            }

            ++Size;
        }

        /// <summary>
        /// Removes an item from the left end of the deque and returns it.
        /// </summary>
        /// <returns>The removed item</returns>
        public T PopLeft()
        {
            // Throw an ArgumentException if the Pop method is called on an empty queue.
            if (IsEmpty)
                throw new ArgumentException("The deque is empty.");

            // Keep the value in the first node in the list.
            var item = _head.Item;

            // Remove the first node.
            _head = _head.Next;

            if (_head != null)
                _head.Prev = null;
            else
                _tail = null; // the list is empty

            --Size;

            return item;
        }

        /// <summary>
        /// Removes an item from the right end of the deque and returns it.
        /// </summary>
        /// <returns>The removed item</returns>
        public T PopRight()
        {
            // Throw the ArgumentException if the Pop method is called on an empty queue.
            if (IsEmpty)
                throw new ArgumentException("The deque is empty.");

            // Keep the value in the last node in the list.
            var item = _tail.Item;

            // Remove the last node.
            _tail = _tail.Prev;

            if (_tail != null)
                _tail.Next = null;
            else
                _head = null; // the list is empty

            --Size;

            return item;
        }
    }
}
