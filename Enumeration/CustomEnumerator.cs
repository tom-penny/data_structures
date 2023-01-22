namespace Data_Structures.Enumeration;

public interface ICustomEnumerator<out T>
{
    T[] Collection { get; }
    T? Current { get; }
    bool MoveNext();
    void Reset();
}

public class CustomEnumerator<T> : ICustomEnumerator<T>
{
    public T[] Collection { get; }
    private int _index = -1;

    public T? Current { get; private set; }

    public CustomEnumerator(T[] collection)
    {
        Collection = collection;
    }

    public bool MoveNext()
    {
        if (++_index >= Collection.Length) return false;
        Current = Collection[_index];
        return true;
    }

    public void Reset()
    {
        _index = -1;
    }
}