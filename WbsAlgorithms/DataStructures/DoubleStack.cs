namespace WbsAlgorithms.DataStructures
{
    /// <summary>
    /// [Sedgewick] 1.3.48 p.171 - Implementat two stacks using a single deque. 
    /// Each stack operation takes a constant number of deque operations.
    /// </summary>
    public class DoubleStack<T>
    {
        private DequeLinkedList<T> d = new DequeLinkedList<T>();

        /// <summary>
        /// Indicates whether the left stack is empty.
        /// </summary>
        public bool IsLeftEmpty { get { return LeftSize == 0; } }

        /// <summary>
        /// The size of the left stack.
        /// </summary>
        public int LeftSize { get; private set; }

        /// <summary>
        /// Indicates whether the right stack is empty.
        /// </summary>
        public bool IsRightEmpty { get { return RightSize == 0; } }

        /// <summary>
        /// The size of the right stack.
        /// </summary>
        public int RightSize { get; private set; }

        /// <summary>
        /// Adds an item to the left stack.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void PushLeft(T item)
        {
            d.PushLeft(item);
            ++LeftSize;
        }

        /// <summary>
        /// Adds an item to the right stack.
        /// </summary>
        /// <param name="item">An item to add</param>
        public void PushRight(T item)
        {
            d.PushRight(item);
            ++RightSize;
        }

        /// <summary>
        /// Removes an item from the left stack and returns it.
        /// </summary>
        /// <returns>The removed item</returns>
        public T PopLeft()
        {
            // Deque.PopLeft throws an exception if the deque is empty.
            T item = d.PopLeft();
            --LeftSize;
            return item;
        }

        /// <summary>
        /// Removes an item from the right stack and returns it.
        /// </summary>
        /// <returns>The removed item</returns>
        public T PopRight()
        {
            // Deque.PopRight throws an exception if the deque is empty.
            T item = d.PopRight();
            --RightSize;
            return item;
        }
    }
}
