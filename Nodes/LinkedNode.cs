namespace Data_Structures.Nodes;

public class LinkedNode<T> : Node<T>
{
    public LinkedNode(T value) : base(value) { }

    public LinkedNode<T>? Next { get; set; }
}