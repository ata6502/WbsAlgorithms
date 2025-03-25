using System;
using System.Collections.Generic;

namespace WbsAlgorithms.Arrays
{
    /*
        Odd Occurrences in Array

        [codility]: https://app.codility.com/programmers/lessons/2-arrays/odd_occurrences_in_array/ 

        A non-empty array A consists of N integers. The array contains an odd number of elements, 
        and each element of the array can be paired with another element that has the same value, 
        except for one element that is left unpaired.

        Example: Input: [9, 3, 9, 3, 9, 7, 9] Output: 7

        Input: all but one of the values in A occur an even number of times.
    */
    public class OddOccurrencesInArray
    {
        // Returns the unpaired element in O(n).
        public static int GetOddNumber(int[] a)
        {
            var d = new Dictionary<int, int>();

            // Perfrom two passes over a dictionary.

            // Count the number of occurences of each element.
            foreach (var n in a)
            {
                if (d.TryGetValue(n, out int _))
                {
                    ++d[n];
                }
                else
                {
                    d.Add(n, 1);
                }
            }

            // Find an element that has the odd number of occurences.
            foreach (var e in d)
            {
                if (e.Value % 2 != 0)
                    return e.Key;
            }

            throw new ArgumentException("No odd element.");
        }
    }
}
