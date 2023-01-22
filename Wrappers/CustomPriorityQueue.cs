using Data_Structures.Arrays;

namespace Data_Structures.Wrappers;

public class CustomPriorityQueue<TValue, TPriority> where TPriority : IComparable<TPriority>
{
    private readonly d_AryMinHeap<Entry<TPriority, TValue>> _heap;

    public int Count => _heap.Count;

    public CustomPriorityQueue(int n, int capacity = 6)
    {
        _heap = new d_AryMinHeap<Entry<TPriority, TValue>>(n, capacity);
    }

    public void Enqueue(TValue value, TPriority priority)
    {
        _heap.Push(new Entry<TPriority, TValue>(priority, value));
    }
    public TValue? Dequeue()
    {
        return _heap.Pop().Value;
    }
    public bool Peek(out TValue? result)
    {
        result = default;
        if (!_heap.Peek(out var value)) return false;
        result = value.Value;
        return true;
    }
}