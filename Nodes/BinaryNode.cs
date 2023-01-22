namespace Data_Structures.Nodes;

public class BinaryNode<T> : SortedNode<T> where T : IComparable<T>
{
    public BinaryNode(T value) : base(value) { }

    public virtual BinaryNode<T>? Left { get; set; }
    public virtual BinaryNode<T>? Right { get; set; }
    public virtual BinaryNode<T>? Parent { get; set; }
    public int Height { get; set; } = 1;

    public bool IsLeftChild => Parent is { Left: { } left } && Equals(left);
    public bool IsRightChild => Parent is { Right: { } right } && Equals(right);

    public virtual void Deconstruct(out BinaryNode<T>? left, out BinaryNode<T>? right)
    {
        (left, right) = (Left, Right);
    }
    public virtual void Deconstruct(out BinaryNode<T>? left, out BinaryNode<T>? right, out BinaryNode<T>? parent)
    {
        (left, right, parent) = (Left, Right, Parent);
    }
}