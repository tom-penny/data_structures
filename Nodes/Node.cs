namespace Data_Structures.Nodes;

public interface INode<T>
{
    T Value { get; set; }
}

public abstract class Node<T> : INode<T>
{
    protected Node(T value)
    {
        Value = value;
    }

    public T Value { get; set; }
}