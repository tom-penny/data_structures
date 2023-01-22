namespace Data_Structures.Arrays;

public class d_AryMinHeap<T> : MinHeap<T> where T : IComparable<T>
{
    private readonly int _n;

    public d_AryMinHeap(int n, int capacity = 6) : base(capacity)
    {
        _n = n;
    }

    protected override int GetLeftIndex(int index)
    {
        return index * _n + 1;
    }
    protected override int GetRightIndex(int index)
    {
        return index * _n + _n;
    }
    protected override int GetParentIndex(int index)
    {
        return (index - 1) / _n;
    }
    protected override void HeapifyDown()
    {
        var index = 0;
        var root = Heap[index];
        var swap = GetMinDescendant(index);
        while (GetMinDescendant(index) < Count && Heap[swap].CompareTo(root) < 0)
        {
            Heap[index] = Heap[swap];
            index = swap;
            swap = GetMinDescendant(index);
        }
        Heap[index] = root;
    }

    private int GetMinDescendant(int index)
    {
        var descendant = GetLeftIndex(index);
        for (var i = descendant + 1; i <= GetRightIndex(index) && i < Count; i++)
        {
            if (Heap[i].CompareTo(Heap[descendant]) < 0) descendant = i;
        }
        return descendant;
    }
}