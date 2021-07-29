namespace WbsAlgorithms.Common
{
    /// <summary>
    /// Defines a node of a doubly-linked list.
    /// </summary>
    public class DoubleListNode<T>
    {
        public T Item { get; set; }

        // A node in a doubly-linked list contains a reference to the item preceding it
        // and the item following it (null if there is no such item).
        public DoubleListNode<T> Prev { get; set; }
        public DoubleListNode<T> Next { get; set; }

        public DoubleListNode(T item, DoubleListNode<T> prev = null, DoubleListNode<T> next = null)
        {
            Item = item;
            Prev = prev;
            Next = next;
        }
        public override string ToString()
        {
            var prevItem = (Prev == null ? "NULL" : Prev.Item.ToString());
            var nextItem = (Next == null ? "NULL" : Next.Item.ToString());

            return $"{(prevItem, Item, nextItem)}";
        }
    }
}
