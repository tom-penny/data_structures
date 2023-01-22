using Data_Structures.Enumeration;
using Data_Structures.Nodes;

namespace Data_Structures.Lists;

public abstract class LinkedCollection<T> : ICustomEnumerable<T>
{
    public LinkedNode<T>? Head { get; protected set; }
    public int Count { get; protected set; }

    public virtual ICustomEnumerator<T> Custom_GetEnumerator()
    {
        var arr = Count > 0 ? new T[Count] : Array.Empty<T>();
        var node = Head ?? null;
        for (var i = 0; i < Count; i++)
        {
            arr[i] = node.Value;
            node = node.Next ?? null;
        }
        return new CustomEnumerator<T>(arr);
    }
}