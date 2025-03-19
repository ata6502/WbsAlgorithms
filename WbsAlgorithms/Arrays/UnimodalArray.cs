namespace WbsAlgorithms.Arrays
{
    public class UnimodalArray
    {
        /*
            Maximum value in a unimodal array.

            [AlgoIlluminated-1] - Problem 3.2 (p.91, eb.103)
            You are given a unimodal array of n distinct elements, meaning that its entries are in increasing order up 
            until its maximum element, after which its elements are in decreasing order. Give an algorithm to compute 
            the maximum element of a unimodal array that runs in O(log(n)) time.

            In the notation of the master method, a = 1, b = 2, and d = 0, so the running time is O(log(n)) (by case #1).
        */
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

            // The loop continues until left == right, which is the peak index.
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
