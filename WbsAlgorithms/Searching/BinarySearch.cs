using System;
using System.Diagnostics;

namespace WbsAlgorithms.Searching
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
        /// [Sedgewick] 1.4.21 p.210 - Binary search on distinct values.
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
        /// [Sedgewick] 1.4.11 p.209 - Implement a method that finds the number of occurrences of a given
        /// input element in time proportional to log(N) in the worst case.
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

        /// <summary>
        /// Returns the smallest index of an element that matches the search element.
        /// This method uses an iterative approach and guarantees logarithmic running time.
        /// 
        /// [Sedgewick] 1.4.10 p.209 - Return the smallest index of an element that matches 
        /// the search element.
        /// </summary>
        /// <param name="key">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The smallest index of an element that matches the input element</returns>
        public static int FirstFirstIndexIteratively<T>(T key, T[] array)
            where T : IComparable<T>
        {
            var low = 0;
            var high = array.Length - 1;

            while (low <= high)
            {
                var mid = (low + high) / 2;

                // This part is the same as for binary search.
                if (key.CompareTo(array[mid]) < 0) // key < a[mid]
                    high = mid - 1;
                else if (key.CompareTo(array[mid]) > 0) // key > a[mid]
                    low = mid + 1;

                // At this point we know that key == a[mid].

                // Check if the element before a[mid] is smaller than the key.
                // If so, we know that it is the index we are looking for.
                else if (mid > 0 && key.CompareTo(array[mid - 1]) > 0) // key == a[mid]
                    return mid;

                // Check if the key is the first element in the array. If so, it has
                // the lowest index and we can stop searching.
                else if (mid == 0) // key == a[mid]
                    return mid;

                // Otherwise, we are somewhere in the middle of a sequence of keys.
                // We need to keep searching for the first element in that sequence.
                else
                    high = mid - 1;
            }

            return -1;
        }

        /// <summary>
        /// Returns the smallest index of an element that matches the search element.
        /// This method uses a recursive approach and guarantees logarithmic running time.
        /// 
        /// [Sedgewick] 1.4.10 p.209 - Return the smallest index of an element that matches 
        /// the search element.
        /// </summary>
        /// <param name="key">The input element</param>
        /// <param name="array">A sorted array of elements some of which may be equal</param>
        /// <returns>The smallest index of an element that matches the input element</returns>
        public static int FindFirstIndexRecursively<T>(T key, T[] array)
            where T : IComparable<T>
        {
            return FindFirstIndexRecursivelyInternal(key, array, 0, array.Length - 1);
        }

        public static int FindFirstIndexRecursivelyInternal<T>(T key, T[] array, int low, int high)
            where T : IComparable<T>
        {
            if (high < low)
                return -1;

            var mid = (low + high) / 2;

            if (key.CompareTo(array[mid]) < 0) // key < a[mid]
                return FindFirstIndexRecursivelyInternal(key, array, low, mid - 1);
            else if (key.CompareTo(array[mid]) > 0) // key > a[mid]
                return FindFirstIndexRecursivelyInternal(key, array, mid + 1, high);
            else if (mid > 0 && key.CompareTo(array[mid - 1]) > 0) // key == a[mid]
                return mid;
            else if (mid == 0) // key == a[mid]
                return mid;
            else
                return FindFirstIndexRecursivelyInternal(key, array, low, mid - 1);
        }

        /// <summary>
        /// Finds an index of a given integer in a sorted array. Instead of searching based on 
        /// powers of two (binary search) we use Fibonacci numbers:
        /// 
        /// F(0)=0, F(1)=1 and F(n) = F(n-1) + F(n-2) for n > 1
        /// The sequence starts: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, ... 
        /// 
        /// [Sedgewick] 1.4.22 p.211 - Binary search with only addition and subtraction.
        /// Given an array of N distinct values in ascending order, determine whether a given
        /// integer is in the array. You may use only additions and subtractions and a constant
        /// amount of extra memory. The running time should be O(lg(N)).
        /// </summary>
        /// <param name="key">An element whose index we are looking for</param>
        /// <param name="array">A sorted array of distinct integer values in ascending order</param>
        /// <returns>If the element found, the element's index. Otherwise, -1.</returns>
        public static int FindIndexUsingFibonacci(int key, int[] array)
        {
            // Computes the greatest Fibonacci number that is smaller than the size of the input array.

            int tmp;
            var fk = 1; // F(k)
            var fk_prev = 0; // F(k-1)

            // Find a Fibonacci number F(k) that is equal or greater than the size
            // of the input array. Also, keep the previous Fibonacci number F(k-1).
            while (fk < array.Length)
            {
                tmp = fk;
                fk = fk_prev + fk;
                fk_prev = tmp;
            }

            // Initialize the interval of indices [low, high] in the array.
            var low = 0;
            var high = array.Length - 1;

            while (low <= high)
            {
                // Find a Fibonacci number F(k) that is less than the width of
                // the interval [low, high]. We will use F(k-1) to determine
                // the index of an element we want to test.
                // The condition F(k-1) > 0 ensures that the smallest F(k) is 1.
                // We are using subtraction to "go back" in the Fibonacci
                // sequence e.g., 13, 8, 5, 3, 2, 1, 1, 0
                while (fk_prev > 0 && fk >= high - low)
                {
                    tmp = fk_prev;
                    fk_prev = fk - fk_prev; // F(k-1)
                    fk = tmp; // F(k)
                }

                // Calculate the index low + F(k-1). This is analogous to dividing
                // the interval [low, high] by 2 in the binary search. We just
                // use smaller and smaller Fibonacci numbers instead.
                int index = low + fk_prev;

                if (key < array[index])
                {
                    // Update the current range to [low, low + F(k-1)]
                    high = index - 1;
                }
                else if (key > array[index])
                {
                    // Update the current range to [low + F(k-1), high]
                    low = index + 1;
                }
                else
                {
                    return index;
                }
            }

            return -1;
        }
    }
}
