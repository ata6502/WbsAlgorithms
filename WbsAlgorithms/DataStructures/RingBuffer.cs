using System;

namespace WbsAlgorithms.DataStructs
{
    /// <summary>
    /// A ring buffer (a.k.a. a circular queue) is a FIFO data structure
    /// useful for transferring data between asynchronous processes or
    /// for storing log files.
    /// 
    /// This implementation uses a fixed-size array with circular 
    /// wrap-around to store items.
    /// 
    /// [Sedgewick] 1.3.39 p.169 - Implement a fixed-size ring buffer with circular wrap-around.
    /// </summary>
    public class RingBuffer<T>
    {
        private T[] _array; // buffer entries
        private int _capacity;
        private bool _isOverflow = false;

        private int _first = -1; // the index of the first item
        private int _last = -1; // the index of the last item

        /// <summary>
        /// Indicates whether the buffer is empty.
        /// </summary>
        public bool IsEmpty => Size == 0;

        /// <summary>
        /// Returns the number of items in the buffer.
        /// </summary>
        public int Size { get; private set; }

        public RingBuffer(int capacity)
        {
            _capacity = capacity;
            _array = new T[capacity];
        }

        /// <summary>
        /// Adds an item to the buffer.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Put(T item)
        {
            ++_last;

            // Wrap-around if the buffer capacity has been exceeded.
            if (_last >= _capacity)
            {
                _last = 0;
                _isOverflow = true;
            }

            _array[_last] = item;

            if (_isOverflow)
                _first = _last;

            Size = Math.Min(Size+1, _capacity);
        }

        /// <summary>
        /// Removes and returns an item from the buffer. Throws an exception if the buffer is empty.
        /// </summary>
        /// <returns>An item removed from the buffer</returns>
        public T Get()
        {
            if (Size == 0)
                throw new IndexOutOfRangeException("Buffer is empty");

            ++_first;

            // wrap-around
            if (_first >= _capacity)
                _first = 0;

            T item = _array[_first];

            // Avoid loitering by overwriting the orphaned reference (applicable to reference types).
            _array[_first] = default(T); 

            _isOverflow = false;
            --Size;

            return item;
        }

        public override string ToString()
        {
            return string.Join(' ', _array);
        }
    }
}
