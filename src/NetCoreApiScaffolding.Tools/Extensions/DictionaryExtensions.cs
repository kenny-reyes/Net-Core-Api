using System;
using System.Collections.Generic;

namespace NetCoreApiScaffolding.Tools.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> dictionary)
        {
            dictionary.ForEach(x => source.Add(x.Key, x.Value));
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
