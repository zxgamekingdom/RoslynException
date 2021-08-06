using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConsoleApp1.Extensions
{
    public static class DictionaryExtensison
    {
        public static IDictionary<TK, TV> Copy<TK, TV>(
            this IDictionary<TK, TV> concurrentDictionary) where TK : notnull
        {
            if (concurrentDictionary == null)
                throw new ArgumentNullException(nameof(concurrentDictionary));
            IDictionary<TK, TV> dictionary = new ConcurrentDictionary<TK, TV>();
            foreach ((TK key, TV value) in concurrentDictionary)
            {
                dictionary.Add(key, value);
            }

            return dictionary;
        }
    }
}
