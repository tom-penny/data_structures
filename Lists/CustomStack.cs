using Data_Structures.Nodes;

namespace Data_Structures.Lists;

public interface ICustomStack<T>
{
    void Push(T value);
    T? Pop();
    bool Peek(out T? result);
}

public class CustomStack<T> : LinkedCollection<T>
{
    public virtual void Push(T value)
    {
        var node = new LinkedNode<T>(value);
        if (Head is null)
        {
            Head = node;
        }
        else
        {
            node.Next = Head;
            Head = node;
        }
        Count++;
    }
    public virtual T Pop()
    {
        var result = Head.ThrowIfNull()!.Value;
        Head = Head!.Next ?? null;
        Count--;
        return result;
    }
    public virtual bool Peek(out T? result)
    {
        result = default;
        if (Head is null) return false;
        result = Head.Value;
        return true;
    }
}