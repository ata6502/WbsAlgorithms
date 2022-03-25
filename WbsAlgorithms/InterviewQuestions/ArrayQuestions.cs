using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to find duplicated numbers in an array? [FindDuplicatedNumbers]
    // 2. How to remove duplicates from an array? [RemoveDuplicates]
    // 3. How to find a missing integer in an array that contains a range of consecutive integers? [FindMissingValue]
    public class ArrayQuestions
    {
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

            var sum = 0;
            foreach (var n in a)
                sum += n;

            var len = a.Length + 1;
            var calculatedSum = len * (2 * a[0] + (len - 1)) / 2;

            var missingValue = calculatedSum - sum;
            if (missingValue == 0)
                return -1;
            return missingValue;
        }

        // Returns an array without duplicates i.e., only with distinct elements.
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
