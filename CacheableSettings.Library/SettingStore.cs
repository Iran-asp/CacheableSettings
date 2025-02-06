using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheableSettings.Library
{
    /// <summary>
    /// Main class to manage setting items in memory
    /// </summary>
    public class SettingStore : ISettingStore
    {
        private readonly IMemoryCache _memoryCache;

        public SettingStore(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// Get stored item in memory or create it
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="createItem">Your desired function</param>
        /// <param name="ttl">Time to live in seconds - 0 means unlimited</param>
        /// <returns></returns>
        public virtual Task<string?> GetOrCreate_Async(string key, Func<string> createItem, int ttl = 0)
        {
            if (!_memoryCache.TryGetValue(key, out string? cachedValue))
            {
                cachedValue = createItem();

                if (ttl == 0)
                {
                    _memoryCache.Set(key, cachedValue);
                }
                else
                {
                    _memoryCache.Set(key, cachedValue, new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(ttl)));
                }
            }

            return Task.FromResult(cachedValue);
        }

        /// <summary>
        /// Get an item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<string?> Get_Async(string key)
        {
            _memoryCache.TryGetValue(key, out string? value);
            return Task.FromResult(value);
        }

        /// <summary>
        /// Store all setting items in memory
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public virtual void Store(Dictionary<string, string> items)
        {
            foreach (var item in items)
            {
                _memoryCache.Remove(item.Key);
                _memoryCache.Set(item.Key, item.Value);
            }
        }
    }
}
