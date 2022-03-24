using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to find duplicated numbers in an array? [FindDuplicatedNumbers]
    public class ArrayQuestions
    {
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
