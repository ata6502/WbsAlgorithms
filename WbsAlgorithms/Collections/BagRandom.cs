using System;
using System.Collections;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// A random bag stores a collection of items and supports the Add(item) operation.
    /// Random bag iterations provide the items in random order. All N! permutations are 
    /// equally likely.
    /// 
    /// [Sedgewick] 1.3.34 p.167 - Implement a random bag collection.
    /// </summary>
    public class BagRandom<T>
    {
        private Random _rnd = new Random();

        // The random bag keeps the items in an array and randomizes the order of the items
        // in the GetEnumerator method.
        private T[] _array;

        /// <summary>
        /// Indicates whether the bag is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0);

        /// <summary>
        /// Returns the number of items in the bag.
        /// </summary>
        public int Size { get; private set; }

        public BagRandom(int capacity)
        {
            _array = new T[capacity];
        }

        /// <summary>
        /// Adds an item to the bag.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Add(T item)
        {
            // Check if there is room in the array. If not, double the size of the array.
            if (Size == _array.Length)
                Resize(2 * _array.Length);

            // Add the item to the bag. First, set _array[Size]=item 
            // and then increment Size.
            _array[Size++] = item;
        }

        /// <summary>
        /// Returns items in the bag in random order. 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            // Randomize the order of the items for this iterator.
            Shuffle();

            for (int i = 0; i < Size; ++i)
            {
                yield return _array[i];
            }
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

        /// <summary>
        /// Randomly shuffles the items in the bag's array. 
        /// </summary>
        private void Shuffle()
        {
            for (int i = 0; i < Size; ++i)
            {
                // Exchange a[i] with a random element in a[i..N-1]
                // All N! permutations are equally likely.
                // This code is based on [Sedgewick] 1.1 p.36-37
                int r = _rnd.Next(i, Size);
                T tmp = _array[i];
                _array[i] = _array[r];
                _array[r] = tmp;
            }
        }
    }
}
