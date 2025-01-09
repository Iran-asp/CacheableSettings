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

        public int TTL { get; set; }
        public Dictionary<string, string>? SettingItems { get; set; }

        public SettingStore(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
            TTL = 60;
        }

        public virtual Task<string?> GetOrCreate(string key, Func<string> createItem)
        {
            if (!_memoryCache.TryGetValue(key, out string? cachedValue))
            {
                cachedValue = createItem();

                var option = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(TTL));

                _memoryCache.Set(key, cachedValue, option);
            }

            return Task.FromResult(cachedValue);
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
