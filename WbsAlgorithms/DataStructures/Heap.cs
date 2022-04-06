using System;

namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// Implements a heap data structure a.k.a. a priority queue. The heap 
    /// keeps track of objects with keys. It can quickly return the object 
    /// with the smallest key. Keys may not be numerical as long as they can
    /// be ordered. Also, keys need to be distinct. If there are multiple
    /// objects with the smallest key, the heap returns one of them.
    /// 
    /// The two main operations supported by heaps are:
    /// - Insert in O(log n) - given a heap H and an object x, adds x to H.
    /// - ExtractMin in O(log n) - given a heap H, removes and returns 
    ///   from H an object with the smallest key.
    ///   
    /// Other operations supported by heaps:
    /// - FindMin in O(1) - given a heap H, returns an object with the smallest key.
    /// - Heapify in O(n) - given a list of objects, creates a heap containing them.
    /// - Delete in O(log n) - given a heap H and a pointer to an arbitrary object 
    ///   x in H, deletes x from H.
    ///   
    /// where n denotes the number of objects stored in the heap.
    /// 
    /// IMPORATNT: This simplified heap implementation stores only
    /// integer keys. A fully-fledged implementation would be able to use
    /// keys of any type (as long as they key type supports IComparable). 
    /// Additionally, it would store an object (or a pointer to an object) 
    /// corresponding to each key.
    /// Also, we use 1-based index rather than 0-based in the undelying 
    /// head storage (an array). It simplifies calculations of indices:
    /// - parent's index: floor(i/2) for i >= 2
    /// - left child's index: 2i for 2i <= n
    /// - right child's index: 2i+1 for 2i+1 <= n
    /// where i is an index of any key in the heap and n is the number
    /// of keys in the heap.
    /// </summary>
    public class Heap
    {
        private int[] _data;
        private int _currentIndex = 0;

        public Heap(int capacity)
        {
            _data = new int[capacity + 1];
        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="keys"></param>
        public void Insert(int[] keys)
        {
            foreach (var k in keys)
                Insert(k);
        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <param name="key"></param>
        public void Insert(int key)
        {
            if (_currentIndex == _data.Length - 1)
                throw new IndexOutOfRangeException("Maximum size limit reached.");

            ++_currentIndex;

            // Insert the new key at the end.
            _data[_currentIndex] = key;

            // Iteratively bubble up the newly added key until the heap property is restored.
            BubbleUp(_currentIndex);
        }

        /// <summary>
        /// TODO: Add comment
        /// </summary>
        /// <returns></returns>
        public int ExtractMinimum()
        {
            if (_currentIndex == 0)
                throw new IndexOutOfRangeException("Heap is empty.");

            // Obtain the minimum element from the root.
            var min = _data[1];

            // Override the root with the last leaf.
            _data[1] = _data[_currentIndex];

            // Clean up the last leaf.
            _data[_currentIndex] = 0;

            // Decrease the size of the heap.
            --_currentIndex;

            // Iteratively bubble down until the heap property is restored.
            BubbleDown(1);

            return min;
        }

        private void BubbleUp(int index)
        {
            // Calculate the parent's index.
            int parentIndex = index / 2;

            // If the parent is greater the the child...
            if (_data[parentIndex] > _data[index])
            {
                // ... swap the parent with the child.
                var tmp = _data[parentIndex];
                _data[parentIndex] = _data[index];
                _data[index] = tmp;

                BubbleUp(parentIndex);
            }
        }

        private void BubbleDown(int index)
        {
            // Determine the left child's index.
            int leftChildIndex = index * 2;

            // Check if the child's index is greater than the size of heap.
            // If it is it means that the node referenced by the index does
            // not have any children i.e., if it is a leaf.
            if (leftChildIndex > _currentIndex)
                return;

            int leftChildKey = _data[leftChildIndex];
            int rightChildKey = leftChildIndex < _currentIndex ? _data[leftChildIndex + 1] : int.MaxValue; // we assign MaxInt if the child does not exist

            // If both children are greater than the input key _data[index]
            // it means that the heap property is satisfied. We can return
            // from the method.
            if (leftChildKey > _data[index] && rightChildKey > _data[index])
                return;

            // IMPORTANT: Swap with the smaller child.
            if (leftChildKey < rightChildKey)
            {
                // swap
                _data[leftChildIndex] = _data[index];
                _data[index] = leftChildKey;
                BubbleDown(leftChildIndex);
            }
            else
            {
                // swap
                _data[leftChildIndex + 1] = _data[index];
                _data[index] = rightChildKey;
                BubbleDown(leftChildIndex + 1);
            }
        }
    }
}
