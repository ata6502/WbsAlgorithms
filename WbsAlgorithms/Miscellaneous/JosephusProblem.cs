using System.Collections.Generic;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithms.Miscellaneous
{
    /// <summary>
    /// In the Josephus problem, N people arrange themselves in a circle (at positions
    /// numbered from 0 to N-1). Then, every m-th person is eliminated until only one 
    /// person is left. 
    /// The question: What position a person should take to avoid being eliminated?
    /// 
    /// The following implementation uses a queue to find the order in which people 
    /// are eliminated. A person in the last position in the output list is safe.
    ///  
    /// [Sedgewick] 1.3.37 p.168 - Write a program that solves the Josephus problem.
    /// Reference: https://en.wikipedia.org/wiki/Josephus_problem
    /// </summary>
    public class JosephusProblem
    {
        /// <summary>
        /// Returns a list of positions (from 0 to n-1) in which people are eliminated. 
        /// The last position in the list is safe.
        /// </summary>
        /// <param name="n">The number of people</param>
        /// <param name="m">Every m-th person in eliminated</param>
        /// <returns>A list of positions in which people are eliminated</returns>
        public static List<int> GetEliminatedPositions(int n, int m)
        {
            var q = new QueueLinkedList<int>();
            var list = new List<int>();

            // Populate the queue with n numbers from 0 to n-1.
            for (var i = 0; i < n; ++i)
                q.Enqueue(i);

            while (!q.IsEmpty)
            {
                for (int i = 0; i < m; ++i)
                {
                    if (i < m - 1)
                        // If a person is skipped, re-attach their position to the end of the queue.
                        q.Enqueue(q.Dequeue()); 
                    else
                        // If a person is eliminated, add their position to the output list.
                        list.Add(q.Dequeue());
                }
            }

            return list;
        }
    }
}
