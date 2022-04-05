namespace WbsAlgorithms.DataStructures
{
    public interface IStack<T>
    {
        bool IsEmpty { get; }
        int Size { get; }
        void Push(T item);
        T Pop();

        // [Sedgewick] 1.3.7 p.162 - Add a method Peek to Stack that returns the most recently inserted item without popping it.
        T Peek();
    }
}
