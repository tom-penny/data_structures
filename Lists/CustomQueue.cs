using Data_Structures.Nodes;

namespace Data_Structures.Lists;

public interface ICustomQueue<T>
{
    void Enqueue(T value);
    T? Dequeue();
    bool Peek(out T? result);
}

public class CustomQueue<T> : LinkedCollection<T>, ICustomQueue<T>
{
    public LinkedNode<T>? Tail { get; protected set; }

    public virtual T this[int index]
    {
        get
        {
            if (index > Count) throw new IndexOutOfRangeException();
            var node = Head;
            for (var i = 0; i < index; i++)
            {
                node = node.Next;
            }
            return node.Value;
        }
    }

    public virtual void Enqueue(T value)
    {
        var node = new LinkedNode<T>(value);
        if (Head is null)
        {
            Head = node;
            Tail = Head;
        }
        else
        {
            Tail!.Next = node;
            Tail = node;
        }
        Count++;
    }
    public virtual T Dequeue()
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