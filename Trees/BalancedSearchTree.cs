using Data_Structures.Nodes;

namespace Data_Structures.Trees;

public class BalancedSearchTree<T> : BinarySearchTree<T> where T : IComparable<T>
{
    protected override BinaryNode<T> Add(BinaryNode<T>? node, BinaryNode<T> newNode)
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
        else return node;

        ChangeHeight(node);
        return Rotate(node);
    }
    protected override BinaryNode<T>? Remove(BinaryNode<T>? node, T value)
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
        ChangeHeight(node);
        return Rotate(node);
    }
    protected virtual void ChangeHeight(BinaryNode<T> node)
    {
        var height = Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        node.Height = height + 1;
    }
    protected int GetHeight(BinaryNode<T>? node)
    {
        return node is { Height: var height } ? height : 0;
    }
    protected int GetBalance(BinaryNode<T>? node)
    {
        return node is { Height: var height } ? GetHeight(node.Left) - GetHeight(node.Right) : 0;
    }
    protected virtual BinaryNode<T> Rotate(BinaryNode<T> node)
    {
        var balance = GetBalance(node);
        if (balance > 1)
        {
            if (GetBalance(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
            }
            return RotateRight(node);
        }
        if (balance < -1)
        {
            if (GetBalance(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            return RotateLeft(node);
        }
        return node;
    }
    protected virtual BinaryNode<T> RotateRight(BinaryNode<T> node)
    {
        var left = node.Left;
        var right = left.Right;
        left.Right = node;
        node.Left = right;
        ChangeHeight(node);
        ChangeHeight(left);
        return left;
    }
    protected virtual BinaryNode<T> RotateLeft(BinaryNode<T> node)
    {
        var right = node.Right;
        var left = right.Left;
        right.Left = node;
        node.Right = left;
        ChangeHeight(node);
        ChangeHeight(right);
        return right;
    }
}