namespace WbsAlgorithms.PairPointMinMax
{
    public class UnimodalArray
    {
        public static int FindMax(int[] a)
        {
            int left = 0, right = a.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (a[mid] > a[mid + 1])
                {
                    // Peak is in the left part including mid.
                    right = mid;
                }
                else
                {
                    // Peak is in the right part excluding mid.
                    left = mid + 1;
                }
            }

            return a[left];
        }

        public static int FindMaxLinear(int[] a)
        {
            int max = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                if (a[i] > max)
                    max = a[i];
            }

            return max;
        }
    }
}
