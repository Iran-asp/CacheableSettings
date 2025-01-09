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
                    var option = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(ttl));
                    _memoryCache.Set(key, cachedValue, option);
                }
            }

            return Task.FromResult(cachedValue);
        }

        public Task<string?> Get_Async(string key)
        {
            _memoryCache.TryGetValue(key, out string? value);
            return Task.FromResult(value);
        }

        public void Store(Dictionary<string, string> items)
        {
            foreach (var item in items)
            {
                _memoryCache.Remove(item.Key);
                _memoryCache.Set(item.Key, item.Value);
            }
        }
    }
}
