using Data_Structures.Nodes;

namespace Data_Structures.Lists;

public class CustomLinkedList<T> : LinkedCollection<T>
{
    public LinkedNode<T>? Tail { get; protected set; }
    public T this[T arg] => Find(Head, arg).Value;
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

    public void Add(T value)
    {
        var node = new LinkedNode<T>(value);
        Head ??= node;
    }
    public virtual void AddLast(T value)
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
    public virtual void AddFirst(T value)
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
    public virtual void Remove(T value)
    {
        if (Remove(Head, null, value)) Count--;
    }
    public virtual bool Contains(T value)
    {
        return Find(Head, value) != null;
    }
    public bool TryGetValue(T arg, out T? result)
    {
        result = default;
        if (Find(Head, arg) is not { Value: var value }) return false;
        result = value;
        return true;
    }

    protected virtual bool Remove(LinkedNode<T>? node, LinkedNode<T>? previous, T value)
    {
        if (node == null) return false;
        switch (node.Value.Equals(value), previous, node.Next)
        {
            case (false, _, _): return Remove(node.Next, node, value);
            case (true, null, null): Head = null; Tail = null; return true;
            case (true, { }, null): previous.Next = null; Tail = previous; return true;
            case (true, null, { }): Head = node.Next; return true;
            case (true, { }, { }): previous.Next = node.Next; return true;
        }
        //if (node.Value.Equals(data))
        //{
        //    if (node.Next is null && previous is null)
        //    {
        //        Head = null;
        //        Tail = null;
        //        return true;
        //    }
        //    if (node.Next is null)
        //    {
        //        previous.Next = null;
        //        Tail = previous;
        //        return true;
        //    }

        //    if (previous is null && node.Next is { })
        //    {
        //        Head = node.Next;
        //        return true;
        //    }
        //    previous.Next = node.Next;
        //    return true;
        //}
        //return Remove(node.Next, node, data);
    }
    protected virtual LinkedNode<T>? Find(LinkedNode<T>? node, T value) => node switch
    {
        null => null,
        _ when node.Value.Equals(value) => node,
        _ when Find(node.Next, value) is { } next => next,
        _ => null
    };
    protected virtual LinkedNode<T>? Find(LinkedNode<T>? node, Func<T, bool> predicate) => node switch
    {
        null => null,
        _ when predicate(node.Value) => node,
        _ when Find(node.Next, predicate) is { } next => next,
        _ => null
    };
}