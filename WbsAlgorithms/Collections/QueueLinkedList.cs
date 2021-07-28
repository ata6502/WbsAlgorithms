using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// The queue collection allows users to add and remove items based on FIFO policy. The queue preserves
    /// the relative order of items: the items come out in the same order in which they were put in.
    /// 
    /// [Sedgewick] p.150-152 - Queue implementation with items stored in a singly-linked list.
    /// [Sedgewick] 1.3.6 p.162 - Reverse the order of elements in a queue.
    /// [Sedgewick] 1.3.41 p.169 - Copy a queue: Create a constructor that makes a new and independent copy of the given queue.
    /// </summary>
    public class QueueLinkedList<T>
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

        // Default ctor.
        public QueueLinkedList() { }

        /// <summary>
        /// Initializes the queue with another queue passed as the parameter.
        /// </summary>
        /// <param name="q">A queue to copy</param>
        public QueueLinkedList(QueueLinkedList<T> q)
        {
            int size = q.Size;

            // Remove all of the nodes from q and add these nodes to both q and 'this'.
            for (var i = 0; i < size; ++i)
            {
                T item = q.Dequeue();
                Enqueue(item);   // add the item to 'this' queue
                q.Enqueue(item); // re-add the value to q
            }
        }

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Enqueue(T item)
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
        /// Removes the least recently added item to the queue.
        /// </summary>
        /// <returns>The least recently added item</returns>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new ArgumentException("The queue is empty.");

            var item = _head.Item;

            // Remove the node from the beginning of the list.
            _head = _head.Next;

            Size--;

            if (Size == 0)
                _tail = null;

            return item;
        }

        /// <summary>
        /// Reverses the order of elements in the queue.
        /// </summary>
        public void Reverse()
        {
            var stack = new StackLinkedList<T>();
            while (!IsEmpty)
                stack.Push(Dequeue());
            while (!stack.IsEmpty)
                Enqueue(stack.Pop());
        }
    }
}
