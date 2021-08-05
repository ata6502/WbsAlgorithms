using System;
using System.Linq;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// The stack collection allows users to add and remove items based on LIFO policy.
    /// 
    /// [Sedgewick] p.132-137 - Stack implementation with items stored in a resizable array
    /// </summary>
    public class StackArray<T> : IStack<T>
    {
        private T[] _array; // stack entries

        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0);

        /// <summary>
        /// Returns the number of items on the stack i.e., the number of elements in the array.
        /// </summary>
        public int Size { get; private set; }

        public StackArray(int capacity)
        {
            _array = new T[capacity];
        }

        /// <summary>
        /// Adds an item to the stack.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Push(T item)
        {
            // Check if the array is too small. If so, double the array's size.
            if (Size == _array.Length)
                Resize(2 * _array.Length);

            // Add the item to the top of the stack.
            _array[Size++] = item; // set _array[Size]=item and then increment Size
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

            // Remove an item from the top of the stack.
            T item = _array[--Size]; // decrement Size and then assign _array[Size] to an item; _array[Size] becomes an orphan (applicable to reference types)
            _array[Size] = default(T); // avoid loitering by overwriting the orphaned reference (applicable to reference types)

            // Half the array size if it is too large.
            // We need to check if the stack size is equal to one-fourth the array size.
            // As a result, after the array is halved, the stack will be about half full.
            if (Size > 0 && Size == _array.Length / 4)
                Resize(_array.Length / 2);

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

            return _array[Size - 1];
        }

        public override string ToString() =>
            string.Join(' ', _array.Take(Size).Select(x => x.ToString()));

        /// <summary>
        /// Moves an array of Size <= newSize into an array of a different size.
        /// </summary>
        /// <param name="newSize">The size of the new array</param>
        private void Resize(int newSize)
        {
            T[] tmp = new T[newSize];
            for (int i = 0; i < Size; ++i)
                tmp[i] = _array[i];
            _array = tmp;
        }
    }
}
