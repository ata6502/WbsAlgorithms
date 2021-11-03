namespace WbsAlgorithms.Searching
{
    public static class BitonicSearch
    {
        private static int _comparisonCounter;

        /// <summary>
        /// Finds an index of an element in a bitonic array. An array is bitonic if it is 
        /// comprised of an increasing sequence of integers followed by a decreasing sequence
        /// of integers.
        /// 
        /// The elements in the input bitonic array must be unique.
        /// 
        /// A few examples of bitonic arrays:
        /// a = [1,3,2]
        /// b = [1,5,8,7,4,2]
        /// c = [1,6,8,4]
        /// d = [4,8,6,1]
        /// 
        /// Corresponding patterns:
        /// 
        /// a)      b)      c)      d)
        ///           X       X       X
        ///  X       X X     X X     X X
        /// X X     X   X   X           X
        /// 
        /// Note: There are algorithms performing the bitonic search in ~2lg(N):
        /// https://stackoverflow.com/questions/19372930/given-a-bitonic-array-and-element-x-in-the-array-find-the-index-of-x-in-2logn
        /// https://github.com/reneargento/algorithms-sedgewick-wayne/blob/master/src/chapter1/section4/Exercise20_BitonicSearch_2lgN.java
        /// 
        /// [Sedgewick] 1.4.20 p.210 - Write a program that, given a bitonic array of N distinct
        /// values, determines whether a given value is in the array. Your program should use
        /// ~3lg(N) comparisons in the worst case.
        /// </summary>
        /// <param name="a">A bitonic array containing unique elements</param>
        /// <param name="element">An input element to find</param>
        /// <returns>
        /// If found, the index of the input element. Otherwise, -1. Also, returns the number of
        /// comparisons performed.
        /// </returns>
        public static (int Index, int Counter) FindIndex(int[] a, int element)
        {
            _comparisonCounter = 0;

            var max = FindMaximumIndex(a, 0, a.Length - 1);

            var left = AscendingBinarySearch(a, 0, max, element);
            if (left != -1)
                // Element found: array[left]
                return (left, _comparisonCounter);

            var right = DescendingBinarySearch(a, max + 1, a.Length - 1, element);
            if (right != -1)
                // Element found: array[right]
                return (right, _comparisonCounter);

            // Element not found.
            return (-1, _comparisonCounter);
        }

        /// <summary>
        /// Finds the tipping point i.e., the maximum value in a bitonic array.
        /// </summary>
        /// <param name="a">A bitonic array</param>
        /// <param name="low">The beginning index in the input bitonic array</param>
        /// <param name="high">The ending index in the input bitonic array</param>
        /// <returns>The index of the tipping point element</returns>
        private static int FindMaximumIndex(int[] a, int low, int high)
        {
            var mid = (low + high) / 2;

            if (mid == 0)
            {
                // Check if the array has just one element.
                if (a.Length == 1)
                    return mid;

                _comparisonCounter++;

                // Check if the tipping point is the first element in the array.
                if (a[mid] > a[mid + 1])
                    return mid;

                mid++; // go up
            }

            _comparisonCounter++;

            if (a[mid - 1] < a[mid])
            {
                // Check if the tipping point is the last element in the input array
                if (mid == a.Length - 1)
                    return mid;

                _comparisonCounter++;

                // Check if a[mid] is the tipping point: a[mid-1] < a[mid] < a[mid+1]
                if (a[mid] > a[mid + 1])
                    return mid;

                // Continue searching if a[mid] is not the tipping point.
                low = mid + 1; // go up
            }
            else
            {
                // Continue searching. a[mid] is not the tipping point.
                high = mid - 1; // go down
            }

            return FindMaximumIndex(a, low, high);
        }

        /// <summary>
        /// Finds the index of an element in the sub-array a[low,high] that has the value 'element'.
        /// Performs the search by moving up the array towards higher indices.
        /// </summary>
        /// <param name="a">A sorted array</param>
        private static int AscendingBinarySearch(int[] a, int low, int high, int element)
        {
            while (low <= high)
            {
                var mid = (low + high) / 2;

                _comparisonCounter++;

                if (element < a[mid])
                    high = mid - 1;
                else if (element > a[mid])
                    low = mid + 1;
                else
                    return mid;
            }

            return -1; // the 'element' value not found
        }

        /// <summary>
        /// Finds the index of an element in the sub-array a[low,high] that has the value 'element'.
        /// Performs the search by moving down the array towards lower indices.
        /// </summary>
        /// <param name="a">A sorted array</param>
        private static int DescendingBinarySearch(int[] a, int low, int high, int element)
        {
            while (low <= high)
            {
                _comparisonCounter++;

                var mid = (low + high) / 2;

                if (element < a[mid])
                    low = mid + 1;
                else if (element > a[mid])
                    high = mid - 1;
                else
                    return mid;
            }

            return -1; // the 'element' value not found
        }
    }
}
