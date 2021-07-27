namespace WbsAlgorithms.Common
{
    /// <summary>
    /// Defines a node of a singly-linked linked list.
    /// </summary>
    public class ListNode<T>
    {
        public T Item { get; set; }
        public ListNode<T> Next { get; set; }

        public ListNode(T item, ListNode<T> next = null)
        {
            Item = item;
            Next = next;
        }

        public override string ToString() => $"{Item},{(Next == null ? "NULL" : Next.Item.ToString())}";
    }
}
