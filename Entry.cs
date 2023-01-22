namespace Data_Structures
{
    public class KeyValue<TKey, TValue> : IEquatable<KeyValue<TKey, TValue>>
    {
        public KeyValue(TKey key, TValue? value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; }
        public TValue? Value { get; set; }

        public bool Equals(KeyValue<TKey, TValue>? other)
        {
            return other is { } && Key.Equals(other.Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }

    public class Entry<TKey, TValue> : IEquatable<Entry<TKey, TValue>>, IComparable<Entry<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public Entry(TKey key, TValue? value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; }
        public TValue? Value { get; set; }

        public bool Equals(Entry<TKey, TValue>? other)
        {
            return other is { } && Key.Equals(other.Key);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
        public int CompareTo(Entry<TKey, TValue>? other)
        {
            return other is { } ? Key.CompareTo(other.Key) : 1;
        }
    }
}