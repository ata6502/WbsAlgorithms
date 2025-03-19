namespace WbsAlgorithms.Arrays
{
    public class IndexEqualsValue
    {
        /*
            [AlgoIlluminated-1] - Problem 3.3 (p.91, eb.103)
        
            You are given a sorted (from smallest to largest) array A of n distinct integers which can be positive, 
            negative, or zero. You want to decide whether or not there is an index i such that A[i] = i. 
            Design the fastest algorithm you can for solving this problem.

            A solution from the book: A variation on binary search solves the problem in O(log(n)) time. 
            If the input array is empty, answer "no". Otherwise, check the middle element and compare its value A[i] 
            to its position i in the array. If they're the same number, answer "yes"; otherwise, recurse on either 
            the left side of the array (if A[i] > i) or the right side (if A[i] < i).
        */

        // Searches for the index in the range a[i..j]
        // Time complexity: O(log(n))
        public static int FindIndexRecursive(int[] a, int i, int j)
        {
            // base case
            if (i > j)
                return -1;
            else
            {
                var m = (i + j) / 2; // the same as (j - i) / 2 + i;

                if (a[m] > m)
                    return FindIndexRecursive(a, i, m - 1); // identity surpassed
                else if (a[m] < m)
                    return FindIndexRecursive(a, m + 1, j); // identity ahead

                return m;
            }
        }

        // Returns the found index or -1 otherwise.
        // Time complexity: O(n)
        public static int FindIndexBruteForce(int[] a)
        {
            for (var i = 0; i < a.Length; ++i)
            {
                if (a[i] == i)
                    return i;
            }

            return -1;
        }
    }
}
