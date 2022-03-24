namespace WbsAlgorithms.Sorting
{
    public class InsertionSort
    {
        /// <summary>
        /// Sorts elements in-place using the insertion sort algorithm.
        /// InsertionSort works fine with duplicated values.
        /// InsertionSort sorts the input array in-place.
        /// 
        /// [Cormen] p.18
        /// </summary>
        /// <param name="a">An array of integers to sort</param>
        public static void SortAscending(int[] a)
        {
            // Start from the second element.
            for (var j = 1; j < a.Length; ++j)
            {
                // Grab the value of the current element and store it in
                // a temporary variable key.
                var key = a[j];

                // Initialize i to the index of the previous element.
                var i = j - 1;

                // Find the space for the key. It has to be before an element
                // greater then the key.
                while (i >= 0 && a[i] > key)
                {
                    // Move the element up.
                    a[i + 1] = a[i];

                    // Look into the previous element.
                    --i;
                }

                // Store the key in the found slot. Note that we need to use
                // i+1 as we decreased the value of i using --i.
                a[i + 1] = key;
            }
        }

        /// <summary>
        /// Sorts elements in-place in descending order using the insertion sort algorithm.
        /// [Cormen] 2.1-2 p.22
        /// </summary>
        /// <param name="a"></param>
        public static void SortDescending(int[] a)
        {
            for (var j = 1; j < a.Length; ++j)
            {
                var key = a[j];

                var i = j - 1;

                while (i >= 0 && a[i] < key) // this is the only difference: a[i]<key instead of a[i]>key
                {
                    a[i + 1] = a[i];
                    --i;
                }

                a[i + 1] = key;
            }
        }
    }
}
