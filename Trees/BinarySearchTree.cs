using Data_Structures.Nodes;

namespace Data_Structures.Trees;

public abstract class BinarySearchTree<T> : BinaryCollection<T> where T : IComparable<T>
{
    public T this[T arg]
    {
        get => Find(Root, arg).Value;
        set
        {
            var node = Find(Root, arg) ?? null;
            if (node != null) node.Value = value;
            else Add(arg);
        }
    }

    public virtual void Add(T value)
    {
        Root = Add(Root, new BinaryNode<T>(value));
        Count++;
    }
    public virtual void Remove(T value)
    {
        Root = Remove(Root, value);
        Count--;
    }
    public bool TryGetValue(T arg, out T? result)
    {
        result = default;
        if (Find(Root, arg) is not { Value: var value }) return false;
        result = value;
        return true;
    }
    public void Traverse(Action<Node<T>> process, Traversal order)
    {
        Traverse(Root, process, order);
    }
    public void Traverse(Traversal traversal)
    {
        Console.Write($"{traversal}: ");
        Traverse(node =>
        {
            Console.Write($"{node.Value}, ");
        }, traversal);
        Console.WriteLine();
    }

    protected virtual BinaryNode<T> Add(BinaryNode<T>? node, BinaryNode<T> newNode)
    {
        if (node is null) return newNode;
        if (newNode.CompareTo(node) < 0)
        {
            node.Left = Add(node.Left, newNode);
            node.Left.Parent = node;
        }
        else if (newNode.CompareTo(node) > 0)
        {
            node.Right = Add(node.Right, newNode);
            node.Right.Parent = node;
        }
        return node;
    }
    protected virtual BinaryNode<T>? Remove(BinaryNode<T>? node, T value)
    {
        if (node is null) return null;
        if (value.CompareTo(node.Value) < 0)
        {
            node.Left = Remove(node.Left, value);
        }
        else if (value.CompareTo(node.Value) > 0)
        {
            node.Right = Remove(node.Right, value);
        }
        else
        {
            switch (node)
            {
                case (null, _): return node.Right;
                case (_, null): return node.Left;
            }
            node.Value = GetMax(node.Left).Value;
            node.Left = Remove(node.Left, node.Value);
        }
        return node;
    }
    protected virtual BinaryNode<T> GetMin(BinaryNode<T> node)
    {
        return node.Left is { } ? GetMin(node.Left) : node;
    }
    protected virtual BinaryNode<T> GetMax(BinaryNode<T> node)
    {
        return node.Right is { } ? GetMax(node.Right) : node;
    }
    protected virtual BinaryNode<T>? Find(BinaryNode<T>? node, T value) => node switch
    {
        null => null,
        _ when value.CompareTo(node.Value) == 0 => node,
        _ when value.CompareTo(node.Value) < 0 => Find(node.Left, value),
        _ when value.CompareTo(node.Value) > 0 => Find(node.Right, value),
        _ => null
    };
}