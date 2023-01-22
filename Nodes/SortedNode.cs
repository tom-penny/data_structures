namespace Data_Structures.Nodes;

public class SortedNode<T> : Node<T>, IEquatable<SortedNode<T>>, IComparable<SortedNode<T>> where T : IComparable<T>
{
    public SortedNode(T value) : base(value) { }

    public virtual bool Equals(SortedNode<T>? other)
    {
        return other is { } && Value.Equals(other.Value);
    }
    public override bool Equals(object? obj)
    {
        return obj is T other && Equals(other);
    }
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    public virtual int CompareTo(SortedNode<T>? other)
    {
        return other is { } ? Value.CompareTo(other.Value) : 1;
    }
}