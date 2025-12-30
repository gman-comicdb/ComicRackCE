using System;
using System.Collections.Generic;

namespace cYo.Common.Collections;

public class SimpleCache<K, T>
{
    private Dictionary<K, T> dict = new();

    public T Get(K key, Func<K, T> create)
    {
        dict ??= new Dictionary<K, T>();
        return !dict.TryGetValue(key, out var value) ? (dict[key] = create(key)) : value;
    }
}
