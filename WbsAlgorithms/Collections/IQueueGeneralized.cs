namespace WbsAlgorithms.Collections
{
    public interface IQueueGeneralized<T>
    {
        bool IsEmpty { get; }
        int Size { get; }
        void Insert(T item);
        T Delete(int k);
    }
}
