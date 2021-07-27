using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// A bag is a collection where removing items is not supported. 
    /// It allows users to iterate through the collection. 
    /// The order of item iteration is unspecified.
    /// 
    /// [Sedgewick] p.154-155 Bag implementation with items stored in a singly-linked linked list
    /// </summary>
    public class BagLinkedList<T>
    {
        // The first node of a linked list.
        private ListNode<T> _head;

        /// <summary>
        /// Checks whether the bag is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _head == null);

        /// <summary>
        /// Returns the number of items in the bag.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Adds an item to the bag.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void Add(T item)
        {
            var newHead = new ListNode<T>(item, _head);

            // Insert the item at the beginning of the linked list.
            // This effectively reverses the order of elements although
            // the element order for a bag collection is not relevant.
            _head = newHead;

            ++Size;
        }

        /// <summary>
        /// Enumerates items in the bag. 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (ListNode<T> n = _head; n != null; n = n.Next)
            {
                yield return n.Item;
            }
        }
    }
}
