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
        public static (int Index, int Counter) FindLocalMinimumInArray(int[] a)
        {
            _comparisonCounter = 0;

            // Special cases:
            // - one element in the input array
            // - two elements in the input array
            if (a.Length == 1)
                return (0, _comparisonCounter);
            if (a.Length == 2)
                return (a[0] < a[1] ? 0 : 1, _comparisonCounter);

            var index = FindLocalMinimumInArrayRecursive(a, 0, a.Length);
            return (index, _comparisonCounter);
        }

        private static int FindLocalMinimumInArrayRecursive(int[] a, int low, int high)
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
                return FindLocalMinimumInArrayRecursive(a, mid + 1, high);
            }
            else
            {
                _comparisonCounter++;

                if (a[mid - 1] < a[mid + 1])
                    return FindLocalMinimumInArrayRecursive(a, low, mid - 1);
                else
                    return FindLocalMinimumInArrayRecursive(a, mid + 1, high);
            }

        }

        /// <summary>
        /// Returns indices of one of the local minimums in a matrix. The local minimum a[i,j] is 
        /// a pair of indices i and j such that:
        /// a[i,j] < a[i+1,j] and a[i,j] < a[i,j+1] and a[i,j] < a[i-1,j] and a[i,j] < a[i,j-1]
        /// 
        /// We call the elements a[i+1,j], a[i,j+1], a[i-1,j], and a[i,j-1] the neighbours of 
        /// the element a[i,j]. If a[i,j] is located on one of the edges of the matrix or in 
        /// the corners, then it has fewer neighbours.
        ///  
        /// This is a sketch of an algorithm to find a local minimum in a matrix:
        /// 1. Start with an element in the left-top corner of the matrix. 
        /// 2. Check if the element is a local minimum.
        /// 3. If so, we are done.
        /// 4. Otherwise, take the smallest of the element's neighbours and go back to Step 2.
        /// 5. Eventually, the loop 2,3,4 terminates because every step leads to a smaller element.
        ///
        /// Although the algorithm does not check all elements, its running time is greater 
        /// than linear. In fact, the values in the matrix may meander up/down (or left/right etc.) 
        /// This results in the running time of this algorithm to be O(n^2)
        /// </summary>
        /// <param name="a">An N-by-N matrix represented by a two-dimensional array</param>
        /// <returns>A pair (row, column) of 0-based indices pointing to a local minimum in the input matrix</returns>
        public static (int, int) FindLocalMinimumInMatrixSimple(int[,] a)
        {
            // A helper method that returns indices of a minimum value
            // in the neighbourhood of the given element m. If the element m
            // is the smallest element, the method returns its indices.
            (int, int) GetMinimum(int[,] a, (int, int) m)
            {
                var (i, j) = m;
                var rowCount = a.GetUpperBound(0) + 1;
                var colCount = a.GetUpperBound(1) + 1;
                var rowMin = i;
                var colMin = j;

                // Test a[i,j] < a[i+1,j]
                if (i < rowCount - 1 && a[i + 1, j] < a[i, j])
                {
                    rowMin = i + 1;
                    colMin = j;
                }

                // Test a[i,j] < a[i,j+1]
                if (j < colCount - 1 && a[i, j + 1] < a[i, j])
                {
                    rowMin = i;
                    colMin = j + 1;
                }

                // Test a[i,j] < a[i-1,j]
                if (i > 0 && a[i - 1, j] < a[i, j])
                {
                    rowMin = i - 1;
                    colMin = j;
                }

                // Test a[i,j] < a[i,j-1]
                if (j > 0 && a[i, j - 1] < a[i, j])
                {
                    rowMin = i;
                    colMin = j - 1;
                }

                return (rowMin, colMin);
            }

            var i = 0;
            var j = 0;

            // We use an infinite loop because the matrix guarantees that there is at least one
            // local minimum. It ensures that the loop terminates at some point. The local minimum
            // always exists because the values in the matrix are distinct.
            while (true)
            {
                var (rowMin, colMin) = GetMinimum(a, (i, j));

                if (rowMin == i && colMin == j)
                    return (i, j);

                i = rowMin;
                j = colMin;
            }
        }
    }
}
