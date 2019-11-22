using Microsoft.Extensions.Caching.Memory;
using System;

namespace RoadOfGrowth.ExternalService
{
    /// <summary>
    /// MemoryCache帮助类
    /// </summary>
    public static class MemoryCacheUtility
    {

        /// <summary>
        /// add a new value into cache with(or without) expiration time
        /// </summary>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        /// <param name="s">expiration time 'second'</param>
        public static void Set(string k, object v, int? s = null)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                if (s.HasValue)
                    cache.Set(k, v, DateTime.Now.AddSeconds(s.Value));
                else
                    cache.Set(k, v);
            }
        }

        /// <summary>
        /// add a new value into cache with expiration time
        /// </summary>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        /// <param name="t">expiration time</param>
        public static void Set(string k, object v, DateTime t)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                cache.Set(k, v, t);
            }
        }

        /// <summary>
        /// get a string value from cache by key
        /// </summary>
        /// <param name="k">key</param>
        /// <returns></returns>
        public static string Get(string k)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                return cache.Get<string>(k);
            }
        }

        /// <summary>
        /// get a generic value from cache by key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k">key</param>
        /// <returns></returns>
        public static T Get<T>(string k)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                return cache.Get<T>(k);
            }
        }

        /// <summary>
        /// try get a string value from cache by key
        /// </summary>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        /// <returns></returns>
        public static bool TryGetValue(string k, out string v)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                return cache.TryGetValue(k, out v);
            }
        }

        /// <summary>
        /// try get a generic value from cache by key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="k">key</param>
        /// <param name="v">value</param>
        /// <returns></returns>
        public static bool TryGetValue<T>(string k, out T v)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                return cache.TryGetValue<T>(k, out v);
            }
        }

        /// <summary>
        /// remove a key/value
        /// </summary>
        /// <param name="k">key</param>
        public static void Remove(string k)
        {
            using (MemoryCache cache = new MemoryCache(new MemoryCacheOptions()))
            {
                cache.Remove(k);
            }
        }

    }
}
