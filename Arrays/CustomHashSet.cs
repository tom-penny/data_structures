using Data_Structures.Enumeration;
using Data_Structures.Nodes;

namespace Data_Structures.Arrays
{
    public class CustomHashSet<T> : ICustomEnumerable<T>
    {
        private const float LoadFactor = 0.72f;
        private LinkedNode<T>?[] _buckets;

        public CustomHashSet()
        {
            _buckets = new LinkedNode<T>[11];
            InitialiseBuckets();
        }

        public int Count { get; protected set; }
        public T this[T arg] => Find(_buckets[GetIndex(arg)], arg).Value;

        public bool Add(T value)
        {
            var index = GetIndex(value);
            var node = _buckets[index];
            while (node != null)
            {
                if (node.Value.Equals(value)) return false;
                node = node.Next;
            }
            node = new LinkedNode<T>(value)
            {
                Next = _buckets[index]
            };
            _buckets[index] = node;
            Count++;
            if (Count > _buckets.Length * LoadFactor) Resize();
            return true;
        }
        public bool Remove(T value)
        {
            var node = _buckets[GetIndex(value)];
            if (!Remove(node, null, value)) return false;
            Count--;
            return true;
        }
        public virtual ICustomEnumerator<T> Custom_GetEnumerator()
        {
            var arr = Count > 0 ? new T[Count] : Array.Empty<T>();
            var index = 0;
            for (var i = 0; i < _buckets.Length; i++)
            {
                var node = _buckets[i];
                while (node is { })
                {
                    arr[index] = node.Value;
                    node = node.Next ?? null;
                    index++;
                }
            }
            return new CustomEnumerator<T>(arr);
        }
        public virtual bool Contains(T value)
        {
            var node = _buckets[GetIndex(value)];
            return Find(node, value) != null;
        }
        public bool TryGetValue(T arg, out T? result)
        {
            result = default;
            var node = _buckets[GetIndex(arg)];
            if (Find(node, arg) is not { Value: var value }) return false;
            result = value;
            return true;
        }
        public void ListBuckets()
        {
            for (var i = 0; i < _buckets.Length; i++)
            {
                var node = _buckets[i];
                if (node is null) continue;
                Console.Write($"Bucket({i}): ");
                while (node != null)
                {
                    Console.Write($"{node.Value}, ");
                    node = node.Next;
                }
                Console.Write("\n");
            }
        }

        protected bool Remove(LinkedNode<T>? node, LinkedNode<T>? previous, T value)
        {
            if (node is null) return false;
            switch (node.Value.Equals(value), previous)
            {
                case (false, _): return Remove(node.Next, node, value);
                case (true, null): _buckets[GetIndex(value)] = node.Next; return true;
                case (true, _): previous.Next = node.Next; return true;
            }
        }
        protected virtual LinkedNode<T>? Find(LinkedNode<T>? node, T value) => node switch
        {
            null => null,
            _ when node.Value.Equals(value) => node,
            _ when Find(node.Next, value) is { } next => next,
            _ => null
        };

        private int GetIndex(T value)
        {
            var hash = value.GetHashCode();
            var index = hash % _buckets.Length;
            return Math.Abs(index);
        }
        private void InitialiseBuckets()
        {
            for (var i = 0; i < _buckets.Length; i++)
            {
                _buckets[i] = null;
            }
        }
        private void Resize()
        {
            var capacity = 2 * _buckets.Length;
            var temp = _buckets;
            _buckets = new LinkedNode<T>?[capacity];
            InitialiseBuckets();
            Count = 0;
            for (var i = 0; i < temp.Length; i++)
            {
                var node = temp[i];
                while (node is { })
                {
                    Add(node.Value);
                    node = node.Next;
                }
            }
        }
    }
}
