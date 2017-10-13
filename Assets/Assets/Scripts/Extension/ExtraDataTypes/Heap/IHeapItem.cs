using System;

namespace Extension.ExtraTypes.Heap
{
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex { get; set; }
    }
}
