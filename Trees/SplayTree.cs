using Data_Structures.Nodes;

namespace Data_Structures.Trees;

public class SplayTree<T> : BinarySearchTree<T> where T : IComparable<T>
{
    protected override BinaryNode<T>? Find(BinaryNode<T>? node, T value)
    {
        BinaryNode<T> prev = null;
        while (node != null)
        {
            prev = node;
            if (value.CompareTo(node.Value) == 0)
            {
                Root = node;
                return Splay(node);
            }
            node = value.CompareTo(node.Value) < 0 ? node.Left : node.Right;
        }
        if (prev != null) Root = prev;
        return Splay(prev);
    }

    private BinaryNode<T>? Splay(BinaryNode<T>? node)
    {
        while (node is { Parent: { } parent })
        {
            switch (parent, node)
            {
                case ({ IsLeftChild: true }, { IsLeftChild: true }): Rotate(parent.Parent!, parent); Rotate(parent, node); break;
                case ({ IsRightChild: true }, { IsRightChild: true }): Rotate(parent.Parent!, parent); Rotate(parent, node); break;
                case ({ Parent: { } gParent }, _): Rotate(parent, node); Rotate(gParent, node); break;
                default: Rotate(parent, node); break;
            }
        }
        return node ?? null;
    }
    private void Rotate(BinaryNode<T> parent, BinaryNode<T> node)
    {
        switch (parent, node)
        {
            case ({ IsLeftChild: true, Parent: { } gParent }, { IsLeftChild: true }): RotateRight(parent, node); gParent.Left = node; node.Parent = gParent; break;
            case ({ IsRightChild: true, Parent: { } gParent }, { IsLeftChild: true }): RotateRight(parent, node); gParent.Right = node; node.Parent = gParent; break;
            case ({ IsLeftChild: true, Parent: { } gParent }, { IsRightChild: true }): RotateLeft(parent, node); gParent.Left = node; node.Parent = gParent; break;
            case ({ IsRightChild: true, Parent: { } gParent }, { IsRightChild: true }): RotateLeft(parent, node); gParent.Right = node; node.Parent = gParent; break;
            case (_, { IsLeftChild: true }): RotateRight(parent, node); node.Parent = null; break;
            case (_, { IsRightChild: true }): RotateLeft(parent, node); node.Parent = null; break;
        }
        LinkChildren(node);
        LinkChildren(parent);
    }
    private void RotateRight(BinaryNode<T> parent, BinaryNode<T> node)
    {
        switch (node)
        {
            case (_, { } right): node.Right = parent; parent.Left = right; break;
            case (_, _): node.Right = parent; parent.Left = null; break;
        }
    }
    private void RotateLeft(BinaryNode<T> parent, BinaryNode<T> node)
    {
        switch (node)
        {
            case ({ } left, _): node.Left = parent; parent.Right = left; break;
            case (_, _): node.Left = parent; parent.Right = null; break;
        }
    }
    private void LinkChildren(BinaryNode<T> node)
    {
        if (node.Left is { }) node.Left.Parent = node;
        if (node.Right is { }) node.Right.Parent = node;
    }
}