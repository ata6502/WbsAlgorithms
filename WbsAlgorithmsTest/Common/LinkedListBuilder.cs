using WbsAlgorithms.Common;

namespace WbsAlgorithmsTest.Common
{
    public class LinkedListBuilder
    {
        /// <summary>
        /// Create a singly-linked linked list and populate it with given items.
        /// </summary>
        /// <param name="items">Items to populate the linked list</param>
        /// <returns>The head of the newly created linked list</returns>
        public static ListNode<T> CreateSinglyLinkedList<T>(T[] items)
        {
            ListNode<T> head = null;
            ListNode<T> prev = null;

            foreach (var item in items)
            {
                var node = new ListNode<T>(item);

                if (prev == null)
                    head = node;
                else
                    prev.Next = node;

                prev = node;
            }

            return head;
        }
    }
}
