using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WbsAlgorithms.Common
{
    /// <summary>
    /// A bag collection with items stored in a singly-linked list.
    /// Refer to DataStructures/BagLinkedList for more information.
    /// </summary>
    public class Bag<T> : IEnumerable<T>
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

        // Required by IEnumerable<T>
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        public override string ToString()
        {
            var sb = new StringBuilder();
            var node = _head;
            
            while(node != null)
            {
                sb.Append(node.Item.ToString());
                if (node.Next != null)
                    sb.Append(", ");
                node = node.Next;
            }

            return sb.ToString();
        }
    }
}
