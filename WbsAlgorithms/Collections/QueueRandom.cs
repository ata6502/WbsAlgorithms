using System;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// A random queue stores a collection of items. It returns a random item rather than 
    /// the least recently added item. It supports the following API:
    /// - Add an item to the queue (Enqueue).
    /// - Remove and return a random item (Dequeue).
    /// - Return a random item, but do not remove it (Sample).
    /// 
    /// [Sedgewick] 1.3.35 p.168 - Implement a random queue collection.
    /// </summary>
    public class QueueRandom<T> where T : class
    {
        private Random _rnd = new Random();

        // The random queue keeps the items in an array.
        private T[] _array;

        /// <summary>
        /// Indicates whether the queue is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0);

        /// <summary>
        /// Returns the number of items in the queue.
        /// </summary>
        public int Size { get; private set; }

        public QueueRandom(int capacity)
        {
            _array = new T[capacity];
        }

        /// <summary>
        /// Adds an item to the queue.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Enqueue(T item)
        {
            // Check if there is room in the array. If not, double the size of the array.
            if (Size == _array.Length)
                Resize(2 * _array.Length);

            // Add the item to the end of queue. First, set _array[Size]=item 
            // and then increment Size.
            _array[Size++] = item;
        }

        /// <summary>
        /// Removes and returns a random item. 
        /// </summary>
        /// <returns>A random item removed from the queue</returns>
        public T Dequeue()
        {
            // Get a random index in the array.
            int i = _rnd.Next(0, Size); // i = [0, Size-1]

            // Swap an item at the random i-th position with 
            // the one at the last position (index Size-1).
            T tmp = _array[i];
            _array[i] = _array[Size - 1];
            _array[Size - 1] = tmp;

            // Delete and return the last item. Decrement Size first and then 
            // get the array's element.
            T item = _array[--Size];

            // _array[Size] is an orphan. Avoid loitering by overwriting the orphaned reference.
            _array[Size] = null; 

            // Half the array size if it is too large.
            if (Size > 0 && Size == _array.Length / 4)
                Resize(_array.Length / 2);

            return item;
        }

        /// <summary>
        /// Returns a random item, but does not remove it.
        /// </summary>
        /// <returns>A random item in the queue</returns>
        public T Sample()
        {
            // Get a random index in the array.
            int i = _rnd.Next(0, Size); // i = [0, Size-1]

            return _array[i];
        }

        /// <summary>
        /// Move the _array of size N <= newSize to a new array of size newSize.
        /// </summary>
        /// <param name="newSize">The new size of the array</param>
        private void Resize(int newSize)
        {
            T[] tmp = new T[newSize];
            for (int i = 0; i < Size; ++i)
                tmp[i] = _array[i];
            _array = tmp;
        }
    }
}
