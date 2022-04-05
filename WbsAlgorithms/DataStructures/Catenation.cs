using System.Collections.Generic;

namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// Catenable queue, stack, or steque.
    /// 
    /// [Sedgewick] 1.3.47 p.171 - Create an operation Concatenate
    /// that destructively concatenates two queues, stacks, or steques. 
    /// Use a circular liked list in your implementation.
    /// </summary>
    public class Catenation
    {
        /// <summary>
        /// Concatenate two queues.
        /// </summary>
        /// <param name="q1">The first queue</param>
        /// <param name="q2">The second queue</param>
        /// <returns>The circular linked list containing concatenated queues</returns>
        public static CircularLinkedList<T> ConcatenateQueues<T>(Queue<T> q1, Queue<T> q2)
        {
            var list = new CircularLinkedList<T>();

            if (q1 != null)
            {
                while (q1.Count > 0)
                    list.AddItem(q1.Dequeue());
            }

            if (q2 != null)
            {
                while (q2.Count > 0)
                    list.AddItem(q2.Dequeue());
            }

            return list;
        }

        /// <summary>
        /// Concatenate two stacks.
        /// </summary>
        /// <param name="s1">The first stack</param>
        /// <param name="s2">The second stack</param>
        /// <returns>The circular linked list containing concatenated stacks</returns>
        public static CircularLinkedList<T> ConcatenateStacks<T>(Stack<T> s1, Stack<T> s2)
        {
            var list = new CircularLinkedList<T>();

            if (s1 != null)
            {
                while (s1.Count > 0)
                    list.AddItem(s1.Pop());
            }

            if (s2 != null)
            {
                while (s2.Count > 0)
                    list.AddItem(s2.Pop());
            }

            return list;
        }

        /// <summary>
        /// Concatenate two steques (stack-ended queues).
        /// </summary>
        /// <param name="s1">The first steque</param>
        /// <param name="s2">The second steque</param>
        /// <returns>The circular linked list containing concatenated steques</returns>
        public static CircularLinkedList<T> ConcatenateSteques<T>(StequeLinkedList<T> s1, StequeLinkedList<T> s2)
        {
            var list = new CircularLinkedList<T>();

            if (s1 != null)
            {
                while (s1.Size > 0)
                    list.AddItem(s1.Pop());
            }

            if (s2 != null)
            {
                while (s2.Size > 0)
                    list.AddItem(s2.Pop());
            }

            return list;
        }
    }
}
