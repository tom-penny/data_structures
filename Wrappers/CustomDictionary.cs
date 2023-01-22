using Data_Structures.Enumeration;
using Data_Structures.Trees;

namespace Data_Structures.Wrappers;

public class CustomDictionary<TKey, TValue> : ICustomEnumerable<TValue> where TKey : IComparable<TKey>
{
    protected BalancedSearchTree<Entry<TKey, TValue>> Tree = new();

    public ICustomEnumerable<TKey> Keys => Tree.Custom_Select(e => e.Key);
    public ICustomEnumerable<TValue> Values => Tree.Custom_Select(e => e.Value);
    public TValue this[TKey key]
    {
        get => Tree[new Entry<TKey, TValue>(key, default)].Value;
        set
        {
            var entry = new Entry<TKey, TValue>(key, value);
            Tree[entry] = entry;
        }
    }

    public bool TryGetValue(TKey key, out TValue? value)
    {
        value = default;
        if (!Tree.TryGetValue(new Entry<TKey, TValue>(key, default), out var result)) return false;
        value = result!.Value;
        return true;
    }
    public void Add(TKey key, TValue value)
    {
        Tree.Add(new Entry<TKey, TValue>(key, value));
    }
    public void Remove(TKey key)
    {
        Tree.Remove(new Entry<TKey, TValue>(key, default));
    }
    public ICustomEnumerator<TValue> Custom_GetEnumerator()
    {
        return Tree.Custom_Select(e => e.Value).Custom_GetEnumerator();
    }
}