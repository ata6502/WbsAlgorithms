using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// The move-to-front strategy maintains a linked list with no duplicates:
    /// - When a previously unseen element is read, it is inserted at the front of the list.
    /// - When a duplicate element is read, it is deleted from the list and reinserted at the front of the list.
    ///
    /// The move-to-front strategy is useful for:
    /// - caching
    /// - data compression
    /// - wherever items that have been recently accessed are more likely to be reaccessed
    /// 
    /// [Sedgewick] 1.3.40 p.169 - Implement move-to-front strategy that keeps items in a linked list.
    /// </summary>
    public class MoveToFront<T> where T : IComparable<T>
    {
        private ListNode<T> _head; // the front of the list

        /// <summary>
        /// Returns a pointer to the head of the linked list. It's used
        /// for testing the move-to-front strategy.
        /// </summary>
        public ListNode<T> Head => _head;

        /// <summary>
        /// Adds an item according to the move-to-front strategy.
        /// </summary>
        /// <param name="item">An item do add</param>
        public void Add(T item)
        {
            // If the list is empty, add the new item as the list's head.
            if (_head == null)
            {
                _head = new ListNode<T>(item);
                return;
            }

            // Try to find a node with the value 'item'. Also, keep the node previous to the found one.
            ListNode<T> prev = null;
            ListNode<T> node = _head;

            while (node != null && node.Item.CompareTo(item) != 0)
            {
                prev = node;
                node = node.Next;
            }

            var isItemFound = node != null && node.Item.CompareTo(item) == 0;

            // If the node already exists in the list, remove it.
            if (isItemFound)
            {
                if (prev == null)
                    // The only node that does not have a previous node is the head. Remove the head.
                    _head = node.Next; 
                else
                    // Remove the node.
                    prev.Next = node.Next; 
            }

            // Inserted or re-insert (if it was a duplicate) the item at the front of the list
            _head = new ListNode<T>(item, _head);
        }
    }
}
