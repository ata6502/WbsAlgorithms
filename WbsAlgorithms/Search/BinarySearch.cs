using System;

namespace WbsAlgorithms.Search
{
    public static class BinarySearch
    {
        /// <summary>
        /// Finds an index of an element in a sorted array using an iterative approach.
        /// 
        /// [Sedgewick] p.47 - Binary search implemented using a while loop.
        /// </summary>
        /// <typeparam name="T">A type that supports IComparable</typeparam>
        /// <param name="element">An element whose index we are looking for</param>
        /// <param name="array">A sorted array</param>
        /// <returns>If the element found, an index of the input element. Otherwise, -1.</returns>
        public static int FindIndexIteratively<T>(T element, T[] array) 
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

        /// <summary>
        /// Finds an index of an element in a sorted array using a recursive approach.
        /// 
        /// [Sedgewick] 1.1.22 p.58 - Binary search implemented recursively.
        /// </summary>
        /// <typeparam name="T">A type that supports IComparable</typeparam>
        /// <param name="element">An element whose index we are looking for</param>
        /// <param name="array">A sorted array</param>
        /// <returns>If the element found, an index of the input element. Otherwise, -1.</returns>
        public static int FindIndexRecursively<T>(T element, T[] array)
            where T : IComparable<T>
        {
            return FindIndexRecursivelyInternal(element, array, 0, array.Length - 1);
        }

        private static int FindIndexRecursivelyInternal<T>(T element, T[] array, int low, int high)
            where T : IComparable<T>
        {
            if (high < low)
                return -1;

            var mid = (low + high) / 2;
            if (element.CompareTo(array[mid]) < 0) // key < a[mid]
                return FindIndexRecursivelyInternal(element, array, low, mid - 1);
            else if (element.CompareTo(array[mid]) > 0) // key > a[mid]
                return FindIndexRecursivelyInternal(element, array, mid + 1, high);
            else
                return mid;
        }

        /// <summary>
        /// Finds an index of an element in a sorted array using brute-force i.e., linear search.
        /// 
        /// [Sedgewick] 1.1.38 p.61 - Binary search implemented using brute-force search.
        /// </summary>
        /// <typeparam name="T">A type that supports IComparable</typeparam>
        /// <param name="element">An element whose index we are looking for</param>
        /// <param name="array">A sorted array</param>
        /// <returns>If the element found, an index of the input element. Otherwise, -1.</returns>
        public static int FindIndexLinearly<T>(T element, T[] array)
            where T : IComparable<T>
        {
            for (var i = 0; i < array.Length; ++i)
                if (array[i].CompareTo(element) == 0)
                    return i;
            return -1;
        }

        /// <summary>
        /// Returns the number of elements that are smaller than the input element.
        /// 
        /// [Sedgewick] 1.1.29 p.59 - Implement the Rank method.
        /// </summary>
        /// <param name="element">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The number of elements that are smaller than the input element</returns>
        public static int Rank<T>(T element, T[] array)
            where T : IComparable<T>
            => GetLessThanElement(element, array, 0, array.Length - 1);

        /// <summary>
        /// Returns the number of elements that are equal to the input element.
        /// 
        /// [Sedgewick] 1.1.29 p.59 - Implement the Count method.
        /// </summary>
        /// <param name="element">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The number of elements that are equal to the input element</returns>
        public static int Count<T>(T element, T[] array)
            where T : IComparable<T>
        {
            var lessThanElementCount = GetLessThanElement(element, array, 0, array.Length - 1);
            var greaterThanElementCount = GetGreaterThanElement(element, array, 0, array.Length - 1);
            var allElementsCount = array.Length;

            return allElementsCount - (lessThanElementCount + greaterThanElementCount);
        }

        /// <summary>
        /// Returns the number of elements that are smaller than the input element.
        /// </summary>
        /// <param name="element">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The number of elements that are smaller than the input element</returns>
        private static int GetLessThanElement<T>(T element, T[] array, int low, int high)
            where T : IComparable<T>
        {
            if (high < low)
                return low;

            var mid = (low + high) / 2;

            if (element.CompareTo(array[mid]) > 0) // key > a[mid]
                return GetLessThanElement(element, array, mid + 1, high);
            else // key <= a[mid]
                return GetLessThanElement(element, array, low, mid - 1);
        }

        /// <summary>
        /// Returns the number of elements that are greater than the input element.
        /// </summary>
        /// <param name="element">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The number of elements that are greater than the input element</returns>
        private static int GetGreaterThanElement<T>(T element, T[] array, int low, int high)
            where T : IComparable<T>
        {
            if (high < low)
                return array.Length - high - 1;

            var mid = (low + high) / 2;

            if (element.CompareTo(array[mid]) < 0) // key < a[mid]
                return GetGreaterThanElement(element, array, low, mid - 1);
            else // key >= a[mid]
                return GetGreaterThanElement(element, array, mid + 1, high);
        }
    }
}
