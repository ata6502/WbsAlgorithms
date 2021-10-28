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
        /// a number with a pair of indices i and j such that:
        /// a[i,j] < a[i+1,j] and a[i,j] < a[i,j+1] and a[i,j] < a[i-1,j] and a[i,j] < a[i,j-1]
        /// i.e., a number is a local minimum if it is smaller than all of its neighbours. 
        /// A neighbour of a number is one immediately above, below, to the left, or the right. 
        /// Most numbers have four neighbors; numbers on the side have three; the four corners have two.
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
            var i = 0;
            var j = 0;

            // We use an infinite loop because the matrix guarantees that there is at least one
            // local minimum. It ensures that the loop terminates at some point. The local minimum
            // always exists because the values in the matrix are distinct.
            while (true)
            {
                var (rowMin, colMin) = GetLocalMinimumIndices(a, (i, j));

                if (rowMin == i && colMin == j)
                    return (i, j);

                i = rowMin;
                j = colMin;
            }
        }

        /// <summary>
        /// Returns indices of one of the local minimums in a matrix. This recursive algorithm
        /// runs in linear time.
        ///
        /// Complexity: 6N + 6N/2 + 6N/4 + ... = 12N for N x N matrix 
        /// which is linear complexity O(N)
        /// 
        /// References:
        /// http://stackoverflow.com/questions/18525179/find-local-minimum-in-n-x-n-matrix-in-on-time
        /// http://stackoverflow.com/questions/10063289/find-a-local-minimum-in-a-2-d-array
        /// http://www.sciencedirect.com/science/article/pii/0166218X93900048
        ///
        /// [Sedgewick] 1.4.19 p.210 - Given an N-by-N matrix of N^2 distinct integers, 
        /// design an algorithm that finds a local minimum. The running time of your algorithm 
        /// should be proportional to N in the worst case.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static (int, int) FindLocalMinimumInMatrixLinear(int[,] a)
        {
            var maxRowIndex = a.GetUpperBound(0);
            var maxColIndex = a.GetUpperBound(1);

            return FindLocalMinimumInMatrixRecursive(a, 0, 0, 0, maxRowIndex, 0, maxColIndex);
        }

        // rowMin, colMin - indices of the smallest element found so far.
        // minRowIndex, maxRowIndex, minColIndex, maxColIndex - indices that determine the boundaries of a sub-matrix in the input matrix.
        private static (int, int) FindLocalMinimumInMatrixRecursive(int[,] a, int rowMin, int colMin, int minRowIndex, int maxRowIndex, int minColIndex, int maxColIndex)
        {
            // Find the smallest element in the boundaries of the given sub-matrix.
            // Also, check if there is even smaller element in the center row and
            // the center column.

            var centerRowIndex = (maxRowIndex - minRowIndex) / 2;
            var centerColIndex = (maxColIndex - minColIndex) / 2;

            // O(3N)
            for (var j = minColIndex; j <= maxColIndex; ++j)
            {
                // The upper row.
                if (a[minRowIndex,j] < a[rowMin, colMin])
                {
                    rowMin = minRowIndex;
                    colMin = j;
                }

                // The bottom row.
                if (a[maxRowIndex, j] < a[rowMin, colMin])
                {
                    rowMin = maxRowIndex;
                    colMin = j;
                }

                // The center row.
                if (a[centerRowIndex, j] < a[rowMin, colMin])
                {
                    rowMin = centerRowIndex;
                    colMin = j;
                }
            }

            // O(3M)
            for(var i = minRowIndex; i <= maxRowIndex; ++i)
            {
                // The left column.
                if (a[i,minColIndex] < a[rowMin, colMin])
                {
                    rowMin = i;
                    colMin = minColIndex;
                }

                // The right column.
                if (a[i,maxColIndex] < a[rowMin, colMin])
                {
                    rowMin = i;
                    colMin = maxColIndex;
                }

                // The center column.
                if (a[i,centerColIndex] < a[rowMin, colMin])
                {
                    rowMin = i;
                    colMin = centerColIndex;
                }
            }

            // Check if the smallest element is the local minimum in the matrix.
            var (row, col) = GetLocalMinimumIndices(a, (rowMin, colMin));

            if (rowMin == row && colMin == col)
                return (rowMin, colMin);

            // Determine the boundaries of a new sub-matrix that contains the smallest element
            // found so far. Quadrants are defined as follows:
            //
            // ---------
            // | 2 | 1 |
            // ---------
            // | 3 | 4 |
            // ---------

            // 1st quadrant
            if (row < centerRowIndex && col > centerColIndex)
            {
                maxRowIndex = centerRowIndex - 1;
                minColIndex = centerColIndex + 1;
            }
            // 2nd quadrant
            else if (row < centerRowIndex && col < centerColIndex)
            {
                maxRowIndex = centerRowIndex - 1;
                maxColIndex = centerColIndex - 1;
            }
            // 3rd quadrant
            else if (row > centerRowIndex && col < centerColIndex)
            {
                minRowIndex = centerRowIndex + 1;
                maxColIndex = centerColIndex - 1;
            }
            // 4th quadrant
            else // newRowMin > centerRowIndex && newColMin > centerColIndex
            {
                minRowIndex = centerRowIndex + 1;
                minColIndex = centerColIndex + 1;
            }

            // Each iteration shrinks the input matrix from N-by-M to N/2-by-M/2 
            // where N = maxRowIndex - minRowIndex, M = maxColIndex - minColIndex
            return FindLocalMinimumInMatrixRecursive(a, row, col, minRowIndex, maxRowIndex, minColIndex, maxColIndex);
        }

        // A helper method that returns indices of a minimum value in the neighbourhood
        // of the given element m. If the element m is the smallest element, the method
        // returns its indices.
        private static (int, int) GetLocalMinimumIndices(int[,] a, (int, int) m)
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
    }
}
