using Data_Structures.Arrays;
using Data_Structures.Lists;

namespace Data_Structures.Enumeration;

public interface ICustomEnumerable<out T>
{
    ICustomEnumerator<T> Custom_GetEnumerator();
}

public static class CustomEnumerable
{
    public static ICustomEnumerable<TSource> Custom_AsEnumerable<TSource>(this TSource[] arr)
    {
        return new EnumerableArray<TSource>(arr);
    }

    public static bool Custom_Any<TSource>(this ICustomEnumerable<TSource> source)
    {
        var enumerator = source.Custom_GetEnumerator();
        return enumerator.MoveNext();
    }
    public static bool Custom_Any<TSource>(this ICustomEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.Custom_GetEnumerator();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            if (predicate(value)) return true;
        }
        return false;
    }
    public static ICustomEnumerable<TResult> Custom_Select<TSource, TResult>(this ICustomEnumerable<TSource> source, Func<TSource, TResult> func)
    {
        var enumerator = source.Custom_GetEnumerator();
        var queue = new CustomQueue<TResult>();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            queue.Enqueue(func(value));
        }
        return queue;
    }

    public static ICustomEnumerable<TResult> Custom_SelectMany<TSource, TResult>(this ICustomEnumerable<ICustomEnumerable<TSource>> source, Func<TSource, TResult> func)
    {
        var outer = source.Custom_GetEnumerator();
        var queue = new CustomQueue<TResult>();
        while (outer.MoveNext())
        {
            var outerValue = outer.Current!.ThrowIfNull();
            var inner = outerValue.Custom_GetEnumerator();
            while (inner.MoveNext())
            {
                var innerValue = inner.Current;
                queue.Enqueue(func(innerValue));
            }
        }
        return queue;
    }

    public static ICustomEnumerable<TSource> Custom_Where<TSource>(this ICustomEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.Custom_GetEnumerator();
        var queue = new CustomQueue<TSource>();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            if (predicate(value)) queue.Enqueue(value);
        }
        return queue;
    }

    public static TSource? Custom_FirstOrDefault<TSource>(this ICustomEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.Custom_GetEnumerator();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            if (predicate(value)) return value;
        }
        return default;
    }

    public static ICustomEnumerable<TSource> Custom_Distinct<TSource>(this ICustomEnumerable<TSource> source)
    {
        var enumerator = source.Custom_GetEnumerator();
        var table = new CustomHashSet<TSource>();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            table.Add(value);
        }
        return table;
    }

    public static int Custom_Count<TSource>(this ICustomEnumerable<TSource> source)
    {
        var enumerator = source.Custom_GetEnumerator();
        return enumerator.Collection.Length;
    }

    public static int Custom_Count<TSource>(this ICustomEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var sum = 0;
        var enumerator = source.Custom_GetEnumerator();
        while (enumerator.MoveNext())
        {
            var value = enumerator.Current!.ThrowIfNull();
            if (predicate(value)) sum++;
        }
        return sum;
    }

    public static TSource[] Custom_ToArray<TSource>(this ICustomEnumerable<TSource> source)
    {
        var enumerator = source.Custom_GetEnumerator();
        return (TSource[])enumerator.Collection.Clone();
    }

    private sealed class EnumerableArray<T> : ICustomEnumerable<T>
    {
        private readonly T[] _arr;

        public EnumerableArray(T[] arr)
        {
            _arr = arr;
        }

        public ICustomEnumerator<T> Custom_GetEnumerator()
        {
            return new CustomEnumerator<T>(_arr);
        }
    }
}