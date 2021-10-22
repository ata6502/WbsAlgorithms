namespace WbsAlgorithms.PairMinMax
{
    public class LocalMinimum
    {
        private static int _comparisonCounter;

        /// <summary>
        /// Returns an index of a local minimum in an array. The local minimum a[i]
        /// is such that a[i-1] < a[i] < a[i+1]. The method finds just one local minimum
        /// even if multiple minimums exist in the array.
        /// 
        /// [Sedgewick] 1.4.18 p.210 - Find a local minimum in an array. Your program
        /// should use ~2lg(N) comparisons in the worst case (N is the size of the input
        /// array).
        /// </summary>
        /// <param name="a">An array containing distinct integer values</param>
        /// <returns>An index of a local minimum and the number of comparisons</returns>
        public static (int Index, int Counter) FindLocalMinimumIndexInArray(int[] a)
        {
            _comparisonCounter = 0;

            // Special cases:
            // - one element in the input array
            // - two elements in the input array
            if (a.Length == 1)
                return (0, _comparisonCounter);
            if (a.Length == 2)
                return (a[0] < a[1] ? 0 : 1, _comparisonCounter);

            var index = FindLocalMinimumIndexInArrayRecursive(a, 0, a.Length);
            return (index, _comparisonCounter);
        }

        private static int FindLocalMinimumIndexInArrayRecursive(int[] a, int low, int high)
        {
            int mid = (low + high) / 2;

            // Special cases:
            // - the local minimum is the first or the second element 
            // - the local minimum is the last or the one-before-last element
            if (mid == 0)
                return a[mid] < a[mid + 1] ? mid : mid + 1;
            if (mid == a.Length - 1)
                return a[mid] < a[mid - 1] ? mid : mid - 1;

            // The following approach is simple and provides correct results.
            // However, it requires more than 2*ln(n) comparisons.
            /*
            _comparisonCounter += 2;

            // Examine the middle value and its two neighbours. 
            if (a[mid] < a[mid - 1] && a[mid] < a[mid + 1])
                // a[mid] is a local minimum.
                return mid;

            _comparisonCounter += 1;

            // Search in the half with the smaller neighbour as it may be the local minimum.
            if (a[mid - 1] < a[mid + 1])
                return FindLocalMinimumIndexInArrayRecursive(a, low, mid - 1);
            else
                return FindLocalMinimumIndexInArrayRecursive(a, mid + 1, high);
            */

            // The following approach is more complicated but it requires less than 
            // 2*ln(n) comparisons in almost all cases.
            _comparisonCounter++;

            if (a[mid] < a[mid - 1])
            {
                _comparisonCounter++;

                if (a[mid] < a[mid + 1])
                    return mid;
                return FindLocalMinimumIndexInArrayRecursive(a, mid + 1, high);
            }
            else
            {
                _comparisonCounter++;

                if (a[mid - 1] < a[mid + 1])
                    return FindLocalMinimumIndexInArrayRecursive(a, low, mid - 1);
                else
                    return FindLocalMinimumIndexInArrayRecursive(a, mid + 1, high);
            }

        }
    }
}
