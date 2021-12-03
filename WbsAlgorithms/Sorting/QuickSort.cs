using System;

namespace WbsAlgorithms.Sorting
{
    public class QuickSort
    {
        private static Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        // Sorts an input array in-place.
        public static void Sort(int[] a)
        {
            SortRecursively(a, 0, a.Length - 1);
        }

        private static void SortRecursively(int[] a, int l, int r)
        {
            // Base case. A 0- or 1-element subarray.
            if (l >= r)
                return;

            // Choose a pivot randomly.
            int i = _rnd.Next(l, r + 1);

            // Make the pivot first.
            int tmp = a[l];
            a[l] = a[i];
            a[i] = tmp;

            // Get the new pivot position.
            int j = Partition(a, l, r);

            // Recursively sort the first part of the array.
            SortRecursively(a, l, j - 1);

            // Recursively sort the second part of the array.
            SortRecursively(a, j + 1, r);
        }

        private static int Partition(int[] a, int l, int r)
        {
            // Get the pivot from the first element of the subarray. 
            int p = a[l];

            // j keeps track of processed elements.
            // i keeps track of the boundary between processed elements that are 
            // less than and greater than the pivot

            // All the elements between the pivot and i are less than the pivot.
            // All the elements between i and j are greater than the pivot.

            int i = l + 1;
            int tmp;
            for (int j = l + 1; j <= r; ++j)
            {
                // If a[j] < p, we need to restore the invariant.
                // Otherwise, we don't need to do anything.
                if (a[j] < p)
                {
                    // Swap elements to restore the invariant.
                    tmp = a[j];
                    a[j] = a[i];
                    a[i] = tmp;

                    // Update the boundary between elements less than and greater than the pivot.
                    ++i;
                }
            }

            // Swap the pivot into the correct position.
            tmp = a[l];
            a[l] = a[i - 1];
            a[i - 1] = tmp;

            // Return the final pivot position.
            return i - 1;
        }
    }
}
