using System;
using System.Text;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// The stack collection allows users to add and remove items based on LIFO policy.
    /// 
    /// [Sedgewick] p.147-149 - Stack implementation with items stored in a singly-linked list.
    /// [Sedgewick] 1.3.42 p.170 - Copy a stack: Create a constructor that makes a new and independent copy of the given stack. 
    /// </summary>
    public class StackLinkedList<T> : IStack<T>
    {
        private ListNode<T> _head; // the top of the stack i.e., the most recently added node

        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _head == null);

        /// <summary>
        /// Returns the number of items on the stack.
        /// </summary>
        public int Size { get; private set; }

        // Default ctor.
        public StackLinkedList() { }

        /// <summary>
        /// Initializes the stack with another stack passed as the parameter.
        /// </summary>
        /// <param name="s">A stack to copy</param>
        public StackLinkedList(StackLinkedList<T> s)
        {
            // The 'reversed' stack will contain the elements from the input stack in reversed order.
            var reversed = new StackLinkedList<T>();

            int size = s.Size;

            // Pop elements from the input stack and push them to the 'reversed' stack in reversed order.
            // This empties the input stack.
            for (var i = 0; i < size; ++i)
            {
                T item = s.Pop();
                reversed.Push(item);
            }

            // Pop elements from the 'reversed' stack and push them to the new stack (the 'this' object).
            // Also, push the same elements back to the input stack. This rebuilds the input stack in correct order.
            for (var i = 0; i < size; ++i)
            {
                T item = reversed.Pop();
                Push(item); // add the element to 'this' stack
                s.Push(item); // re-add the element to the input stack
            }
        }

        /// <summary>
        /// Adds an item to the stack.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Push(T item)
        {
            var newHead = new ListNode<T>(item, _head);

            // Add the item to the top of the stack i.e., insert the item at the beginning of the linked list.
            _head = newHead;

            ++Size;
        }

        /// <summary>
        /// Removes the most recently added item to the stack.
        /// </summary>
        /// <returns>The most recently added item</returns>
        public T Pop()
        {
            // Throw the NullReferenceException if the Pop method is called on an empty stack.
            if (IsEmpty)
                throw new NullReferenceException("The stack is empty.");

            var item = _head.Item;

            // Remove the item from the top of stack i.e., remove the item from the beginning of the linked list.
            _head = _head.Next;

            --Size;

            return item;
        }

        /// <summary>
        /// Returns the most recently inserted item without popping it.
        /// </summary>
        /// <returns>The most recently inserted item</returns>
        public T Peek()
        {
            if (IsEmpty)
                throw new NullReferenceException("The stack is empty.");

            return _head.Item;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Size);
            var node = _head;

            while(node != null)
            {
                sb.Append(node.Item.ToString());
                if (node.Next != null)
                    sb.Append(' ');
                node = node.Next;
            }

            return sb.ToString();
        }
    }
}
