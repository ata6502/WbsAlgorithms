using System;

namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// GeneralizedQueue is a kind of queue that allows deleting a k-th item.
    /// This implementation uses an array to store items. As a consequence:
    /// - Finding the k-th item is very fast as it is accessed by an index.
    /// - Deletion may be slow as it requires shifting elements in order to fill up a gap left after a removed item.
    ///
    /// [Sedgewick] 1.3.38 p.169 - Implement a collection that supports the following API:
    /// - Add an item to the queue (Insert).
    /// - Delete and return the k-th least recently inserted item (Delete).
    /// </summary>
    public class QueueGeneralizedUsingArray<T> : IQueueGeneralized<T>
    {
        private T[] _array; // queue entries

        /// <summary>
        /// Checks whether the queue is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0);

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Size { get; private set; }

        public QueueGeneralizedUsingArray(int capacity)
        {
            _array = new T[capacity];
        }

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Insert(T item)
        {
            // Check if the array is too small. If so, double the array's size.
            if (Size == _array.Length)
                Resize(2 * _array.Length);

            // Add the item to the end of queue.
            _array[Size++] = item;
        }

        /// <summary>
        /// Deletes and returns the k-th least recently inserted item.
        /// </summary>
        /// <param name="k">The index k</param>
        /// <returns>The k-th least recently inserted item</returns>
        public T Delete(int k)
        {
            // Check if the index is out of range.
            if (k > Size - 1)
                throw new ArgumentOutOfRangeException($"The index {k} is out of range.");

            T item = _array[k];

            // Shift all the items left.
            for (int i = k; i < Size - 1; ++i)
                _array[i] = _array[i + 1];

            _array[Size - 1] = default(T); // a[N-1] is an orphan; avoid loitering by overwriting the orphaned reference

            --Size;

            // Half the array size if it is too large.
            if (Size > 0 && Size == _array.Length / 4)
                Resize(_array.Length / 2);

            return item;
        }

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
