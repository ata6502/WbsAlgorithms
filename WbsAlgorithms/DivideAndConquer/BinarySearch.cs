using System;

namespace WbsAlgorithms.DivideAndConquer
{
    public static class BinarySearch
    {
        /// <summary>
        /// Finds an index of an element in a sorted array using an iterative approach.
        /// 
        /// [Sedgewick] p.47 - Binary search implemented using a while loop
        /// </summary>
        /// <typeparam name="T">A type that supports IComparable</typeparam>
        /// <param name="element">An element whose index we are looking for</param>
        /// <param name="array">A sorted array</param>
        /// <returns>If the element found, an index of the input element. Otherwise, -1.</returns>
        public static int FindElementIteratively<T>(T element, T[] array) 
            where T : IComparable<T>
        {
            var low = 0;
            var high = array.Length-1;

            while (low <= high)
            {
                var mid = (low + high) / 2; // the same as low + (high - low) / 2
                if (element.CompareTo(array[mid]) < 0) // key < a[mid]
                    high = mid - 1;
                else if (element.CompareTo(array[mid]) > 0) // key > a[mid]
                    low = mid + 1;
                else
                    return mid;

            }

            return -1;
        }
    }
}
