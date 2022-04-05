using System.Text;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// A circular linked list has no null links and the value of the tail.next pointer
    /// points to the head whenever the list is not empty.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CircularLinkedList<T>
    {
        // A pointer to the last node of the circular linked list.
        private ListNode<T> _tail;

        /// <summary>
        /// Indicates whether the circular linked list is empty.
        /// </summary>
        public bool IsEmpty => (Size == 0 && _tail == null);

        /// <summary>
        /// Returns the number of items in the circular linked list.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Return a reference to the first item in the circular linked list.
        /// </summary>
        public ListNode<T> FirstItem => _tail?.Next;

        /// <summary>
        /// Adds a new item to the circular linked list.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void AddItem(T item)
        {
            // Create a new node.
            var newNode = new ListNode<T>(item, null);

            // Add the new node.
            if (IsEmpty)
            {
                // There is only one node in the list.
                newNode.Next = newNode;
            }
            else
            {
                // Add the new node to the end of the list.
                // Also, wrap up the list to make it circular.
                newNode.Next = _tail.Next;
                _tail.Next = newNode;
            }

            // Set the new last node.
            _tail = newNode;

            Size++;
        }

        public override string ToString()
        {
            if (Size == 0)
                return string.Empty;

            var sb = new StringBuilder(Size);
            var node = _tail.Next;

            for (var i = 0; i < Size; ++i)
            {
                sb.Append(node.Item.ToString());
                if (i < Size - 1)
                    sb.Append(' ');
                node = node.Next;
            }

            return sb.ToString();
        }
    }
}
