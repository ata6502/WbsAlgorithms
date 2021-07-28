namespace WbsAlgorithms.Common
{
    public class LinkedListBuilder
    {
        /// <summary>
        /// Creates a singly-linked list and populates it with given items.
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

        /// <summary>
        /// Creates a doubly-linked list and populates it with given items.
        /// </summary>
        /// <param name="items">Items to populate the linked list</param>
        /// <returns>The head of the newly created linked list</returns>
        public static DoubleListNode<T> CreateDoublyLinkedList<T>(T[] items)
        {
            DoubleListNode<T> head = null;
            DoubleListNode<T> prev = null;

            foreach (var item in items)
            {
                var node = new DoubleListNode<T>(item);

                if (prev == null)
                    head = node;
                else
                {
                    prev.Next = node;
                    node.Prev = prev;
                }

                prev = node;
            }

            return head;
        }
    }
}
