using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to find duplicated numbers in an array? [FindDuplicatedNumbers]
    // 2. How to remove duplicates from an array? [RemoveDuplicates]
    // 3. How to find a missing integer in an array that contains a range of consecutive integers? [FindMissingValue]
    // 4. How to find the second largest element in an array? [FindSecondLargestValue]
    // 5. How to reverse an array? [ReverseArray]
    // 6. How to rotate in-place an N x N matrix by 90 degress clockwise? [RotateMatrix] [CodingInterview] 1.7 p.91
    // 7. How to zero columns and rows in a matrix? [ZeroMatrix/SetZerosInMatrix] [CodingInterview] 1.8 p.91
    public class ArrayQuestions
    {
        // If an element in an M x N matrix is 0, set the entire row and column to 0.
        public static void SetZerosInMatrix(int[,] a)
        {
            // Determine the dimensions M x N of the input matrix A.
            var m = a.GetUpperBound(0) - a.GetLowerBound(0) + 1;
            var n = a.GetUpperBound(1) - a.GetLowerBound(1) + 1;

            // Collect elements that have a value 0.
            var zeros = new List<(int row, int column)>(); // could be a pair of bool arrays
            for (var i = 0; i < m; ++i)
                for (var j = 0; j < n; ++j)
                    if (a[i, j] == 0)
                        zeros.Add((i, j));

            // Set 0s in the columns and rows of each zero element.
            foreach(var zero in zeros)
            {
                for (var j = 0; j < n; ++j)
                    a[zero.row, j] = 0;

                for (var i = 0; i < m; ++i)
                    a[i, zero.column] = 0;
            }
        }

        // If an element in an M x N matrix is 0, set the entire row and column to 0.
        // Do not use any additional storage i.e., keep space complexity O(1).
        public static void SetZerosInMatrixWithoutAdditionalStorage(int[,] a)
        {
            // Determine the dimensions M x N of the input matrix A.
            var m = a.GetUpperBound(0) - a.GetLowerBound(0) + 1;
            var n = a.GetUpperBound(1) - a.GetLowerBound(1) + 1;

            // The idea is to keep track of which column and row has 0 in the first column
            // and the first row.

            // Check if the first row has any 0s.
            var firstRowHasZero = false;
            for(var j = 0; j < n; ++j)
            {
                if (a[0,j] == 0)
                {
                    firstRowHasZero = true;
                    break;
                }
            }

            // Check if the first column has any 0s.
            var firstColumnHasZero = false;
            for(var i = 0; i < m; ++i)
            {
                if (a[i,0] == 0)
                {
                    firstColumnHasZero = true;
                    break;
                }
            }

            // Iterate through the rest of the matrix. Set 0 in the first column
            // and the first row if an element in the matrix is 0.
            for (var i = 1; i < m; ++i)
            {
                for (var j = 1; j < n; ++j)
                {
                    if (a[i, j] == 0)
                    {
                        a[0, j] = 0;
                        a[i, 0] = 0;
                    }
                }
            }

            // Set 0s in the rows.
            for(var i = 1; i < m; ++i)
            {
                if (a[i, 0] == 0)
                {
                    for (var j = 1; j < n; ++j)
                        a[i, j] = 0;
                }
            }

            // Set 0s in the columns.
            for(var j = 1; j < n; ++j)
            {
                if (a[0, j] == 0)
                {
                    for (var i = 1; i < m; ++i)
                        a[i, j] = 0;
                }
            }

            // Set 0s in the first row.
            if (firstRowHasZero)
            {
                for (var j = 0; j < n; ++j)
                    a[0, j] = 0;
            }

            // Set 0s in the first column.
            if (firstColumnHasZero)
            {
                for (var i = 0; i < m; ++i)
                    a[i, 0] = 0;
            }
        }

        public static void RotateMatrix(int[,] m)
        {
            Debug.Assert(m.GetLowerBound(0) == m.GetLowerBound(1));
            Debug.Assert(m.GetLowerBound(0) == 0);
            Debug.Assert(m.GetUpperBound(0) == m.GetUpperBound(1));

            // The size of the matrix is N x N where N == size.
            var size = m.GetUpperBound(0) + 1;

            // Traverse the square "layers" of the matrix starting
            // from the outer layer.
            //
            //         [4]
            //     X------->|         X is m[k,i]
            //     ^        |         we move [4] --> tmp
            // [1] |        | [3]             [1] --> [4]
            //     |        v                 [2] --> [1]
            //     |<--------                 [3] --> [2]
            //         [2]                    tmp --> [3]
            //
            for (var k = 0; k < size / 2; ++k)
            {
                // Traverse the entire row or a column.
                for(var i = k; i <= size - 2 - k; ++i)
                {
                    // Swap elements in each layer index-by-index.
                    var tmp = m[k, i];                                  // temp = top
                    m[k, i] = m[size - 1 - i, k];                       // top = left
                    m[size - 1 - i, k] = m[size - 1 - k, size - 1 - i]; // left = bottom
                    m[size - 1 - k, size - 1 - i] = m[i, size - 1 - k]; // bottom = right
                    m[i, size - 1 - k] = tmp;                           // right = temp
                }
            }
        }

        // Reverses elements in-place in an array. Returns the reversed input array.
        public static int[] ReverseArray(int[] a)
        {
            for(int i = 0, j = a.Length-1; i < j; ++i, --j)
            {
                var tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
            }

            return a;
        }

        // Returns the second largest value from an array; -1 if the array does not have the second largest value
        // for example when it has fewer than two elements or when all elements have the same value.
        public static int FindSecondLargestValue(int[] a)
        {
            Debug.Assert(a != null);

            if (a.Length < 2)
                return -1;

            var max = int.MinValue;
            var secondMax = int.MinValue;

            foreach(var n in a)
            {
                if (n > max)
                {
                    secondMax = max;
                    max = n;
                }
                else if (n > secondMax)
                {
                    secondMax = n;
                }
            }

            return secondMax == int.MinValue || secondMax == max ? -1 : secondMax;
        }

        // Returns a missing value from an array containing a range of consecutive integers; -1 if no value is missing.
        public static int FindMissingValue(int[] a)
        {
            Debug.Assert(a != null);

            if (a.Length == 0)
                return -1;

            var n = a[0];
            for(var i = 0; i < a.Length; ++i)
            {
                if (a[i] != n)
                    return n;
                ++n;
            }

            return -1;
        }

        public static int FindMissingValueUsingFormula(int[] a)
        {
            Debug.Assert(a != null);

            if (a.Length == 0)
                return -1;

            // Add all elements in the array.
            var sum = 0;
            foreach (var n in a)
                sum += n;

            var len = a.Length + 1; // +1 because we know one value is missing

            // The formula for the sum of an arithmetic sequence: S = N/2 * (2a + (N - 1))
            // where 'N' is the number of elements of the sequence, 'a' is the first element in the sequence
            // Note that for a=1, the formula simplifies to S = N * (N + 1) / 2
            var calculatedSum = len * (2 * a[0] + (len - 1)) / 2;

            // Calculate the missing value.
            var missingValue = calculatedSum - sum;
            return missingValue;
        }

        // Returns an array without duplicates i.e., an array containing only distinct values.
        public static int[] RemoveDuplicates(int[] a)
        {
            Debug.Assert(a != null);

            var distinctNumbers = new HashSet<int>();
            foreach (var n in a)
                if (!distinctNumbers.TryGetValue(n, out _))
                    distinctNumbers.Add(n);
            return distinctNumbers.ToArray();
        }

        public static int[] RemoveDuplicatesUsingLinq(int[] a)
        {
            Debug.Assert(a != null);

            return a.Distinct().ToArray();
        }

        // Returns an array of numbers that occur in the input array at least twice.
        public static int[] FindDuplicatedNumbersUsingDictionary(int[] a)
        {
            Debug.Assert(a != null);

            var d = new Dictionary<int, int>();

            foreach(var n in a)
            {
                if (d.TryGetValue(n, out _))
                    ++d[n];
                else
                    d[n] = 1;
            }

            var duplicates = new List<int>();
            foreach(var kvp in d)
            {
                if (kvp.Value > 1)
                    duplicates.Add(kvp.Key);
            }

            return duplicates.ToArray();
        }
        
        public static int[] FindDuplicatedNumbersUsingLinq(int[] a)
        {
            Debug.Assert(a != null);

            var groups = from n in a
                         group n by n into g
                         where g.Count() > 1
                         select g.Key;

            return groups.ToArray();
        }
    }
}
