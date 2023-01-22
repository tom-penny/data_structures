using Data_Structures.Enumeration;
using Data_Structures.Nodes;

namespace Data_Structures.Trees;

public abstract class BinaryCollection<T> : ICustomEnumerable<T> where T : IComparable<T>
{
    protected BinaryNode<T>? Root;

    public int Count { get; protected set; }

    public virtual ICustomEnumerator<T> Custom_GetEnumerator()
    {
        var arr = Count > 0 ? new T[Count] : Array.Empty<T>();
        var count = 0;
        Traverse(Root, node =>
        {
            arr[count] = node.Value;
            count++;
        }, Traversal.InOrder);
        return new CustomEnumerator<T>(arr);
    }

    protected virtual void Traverse(BinaryNode<T>? node, Action<BinaryNode<T>> process, Traversal order)
    {
        switch (node, order)
        {
            case (null, _): return;
            case (_, Traversal.PreOrder): process(node); Traverse(node.Left, process, order); Traverse(node.Right, process, order); break;
            case (_, Traversal.InOrder): Traverse(node.Left, process, order); process(node); Traverse(node.Right, process, order); break;
            case (_, Traversal.PostOrder): Traverse(node.Left, process, order); Traverse(node.Right, process, order); process(node); break;
            default: throw new ArgumentOutOfRangeException(nameof(order));
        }
    }
}