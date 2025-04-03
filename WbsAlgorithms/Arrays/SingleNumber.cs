using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Arrays
{
    /*
        Single Number 

        Given a non-empty array of integers, every number appears twice except one. Find that single number.
        Note: Your algorithm should have a linear runtime complexity. Could you implement it without using extra memory?

        [leetcode]: https://leetcode.com/problems/single-number/
    */
    public class SingleNumber
    {
        /*
            1. Iterate over all the elements in the input array.
            2. Keep the number of occurences of each element.
            3. Return the element which appeared only once.

            Time complexity: O(n). Time complexity of the for loop is O(n). Time complexity of the dictionary lookup is O(1)
            Space complexity: O(n). The space required by the dictionary is equal to the number of elements in the input array.
        */
        public static int GetSingleNumberUsingDictionary(int[] nums)
        {
            // key: an integer from the input array
            // val: the number of occurences in the array
            var d = new Dictionary<int, int>();

            foreach (var n in nums)
            {
                // Keep the number of occurences of each element.
                if (d.TryGetValue(n, out var _))
                    d[n]++;
                else
                    d.Add(n, 1);
            }

            // Find the number that occured only once.
            foreach (var n in d)
            {
                if (n.Value == 1)
                    return n.Key;
            }

            throw new ArgumentException("The input array does not contain the single number.");
        }

        /*
            Concept: 2*(a+b+c) − (a+a+b+b+c) = c  i.e.  2*sumOfSet - sumOfNums = SingleNumber
            The elements a and b occur in the input exactly twice.

            Time complexity: O(n + n) = O(n) because of the number of elements n in nums.
            Space complexity: O(n + n) = O(n) the HashSet needs space for the elements in nums.
        */
        public static int GetSingleNumberUsingMath(int[] nums)
        {
            var sumOfSet = 0; // the sum of numers that have a pair
            var sumOfNums = 0; // the sum of all numbers including the single number we are looking for

            // We use HashSet to store all elements from nums with no duplications.
            var set = new HashSet<int>();

            foreach (int n in nums)
            {
                if (!set.TryGetValue(n, out var _))
                {
                    set.Add(n);
                    sumOfSet += n;
                }
                sumOfNums += n;
            }

            return 2 * sumOfSet - sumOfNums;
        }

        /*
            Time complexity: O(n). We only iterate through nums, so the time complexity is the number of elements in nums.
            Space complexity: O(1)
        */
        public static int GetSingleNumberUsingXor(int[] nums)
        {
            var r = 0;
            foreach (var n in nums)
                r ^= n;
            return r;
        }
    }
}
