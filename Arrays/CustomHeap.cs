namespace Data_Structures.Arrays;

public abstract class CustomHeap<T> where T : IComparable<T>
{
    protected T[] Heap;

    protected CustomHeap(int capacity = 6)
    {
        Heap = new T[capacity];
    }

    public int Count { get; protected set; }

    public void Push(T value)
    {
        if (Heap.Length == Count) IncreaseSize();
        HeapifyUp(value);
        Count++;
    }
    public T Pop()
    {
        if (Count < 1) throw new IndexOutOfRangeException();
        var value = Heap[0];
        Heap[0] = Heap[--Count];
        if (Count < Heap.Length * 0.25 && Heap.Length / 2 > 0)
        {
            ReduceSize();
        }
        HeapifyDown();
        return value;
    }
    public bool Peek(out T? result)
    {
        result = default;
        if (Count < 1) return false;
        result = Heap[0];
        return true;
    }

    protected abstract void HeapifyUp(T value);
    protected abstract void HeapifyDown();
    protected virtual int GetLeftIndex(int index)
    {
        return index * 2 + 1;
    }
    protected virtual int GetRightIndex(int index)
    {
        return index * 2 + 2;
    }
    protected virtual int GetParentIndex(int index)
    {
        return (index - 1) / 2;
    }
    protected virtual void Swap(int index1, int index2)
    {
        (Heap[index1], Heap[index2]) = (Heap[index2], Heap[index1]);
    }

    protected virtual void IncreaseSize()
    {
        Array.Resize(ref Heap, Heap.Length * 2);
    }
    protected virtual void ReduceSize()
    {
        Array.Resize(ref Heap, Heap.Length / 2);
    }
}

public class MinHeap<T> : CustomHeap<T> where T : IComparable<T>
{
    public MinHeap(int capacity = 6) : base(capacity) { }

    protected override void HeapifyUp(T value)
    {
        var index = Count;
        var parent = GetParentIndex(index);
        while (index > 0 && value.CompareTo(Heap[parent]) < 0)
        {
            Heap[index] = Heap[parent];
            index = parent;
            parent = GetParentIndex(index);
        }
        Heap[index] = value;
    }
    protected override void HeapifyDown()
    {
        var index = 0;
        while (GetLeftIndex(index) < Count)
        {
            var left = GetLeftIndex(index);
            var right = GetRightIndex(index);
            var swap = right < Count && Heap[left].CompareTo(Heap[right]) < 0 ? left : right;
            if (Heap[index].CompareTo(Heap[swap]) < 0) break;
            Swap(index, swap);
            index = swap;
        }
    }
}

public class MaxHeap<T> : CustomHeap<T> where T : IComparable<T>
{
    public MaxHeap(int capacity = 6) : base(capacity) { }

    protected override void HeapifyUp(T value)
    {
        var index = Count;
        var parent = GetParentIndex(index);
        while (index > 0 && value.CompareTo(Heap[parent]) > 0)
        {
            Heap[index] = Heap[parent];
            index = parent;
            parent = GetParentIndex(index);
        }
        Heap[index] = value;
    }
    protected override void HeapifyDown()
    {
        var index = 0;
        while (GetLeftIndex(index) < Count)
        {
            var left = GetLeftIndex(index);
            var right = GetRightIndex(index);
            var swap = right < Count && Heap[left].CompareTo(Heap[right]) > 0 ? left : right;
            if (Heap[index].CompareTo(Heap[swap]) > 0) break;
            Swap(index, swap);
            index = swap;
        }
    }
}