using System.Diagnostics;

namespace WbsAlgorithms.Sorting
{
    /// <summary>
    /// [Sedgewick] 1.1.26 p.59 - Sort three numbers.
    /// </summary>
    public class SortThreeNumbers
    {
        /// <summary>
        /// Sorts the input array in ascending order. The array is sorted in-place.
        /// </summary>
        /// <param name="a">A three-element array.</param>
        public static void SortAscending(int[] a)
        {
            Debug.Assert(a.Length == 3);

            if (a[0] > a[1])
            {
                var tmp = a[0];
                a[0] = a[1];
                a[1] = tmp;
            }

            if (a[0] > a[2])
            {
                var tmp = a[0];
                a[0] = a[2];
                a[2] = tmp;
            }

            if (a[1] > a[2])
            {
                var tmp = a[1];
                a[1] = a[2];
                a[2] = tmp;
            }
        }
    }
}
