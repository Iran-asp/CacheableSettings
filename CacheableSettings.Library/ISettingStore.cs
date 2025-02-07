using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheableSettings.Library
{
    public interface ISettingStore
    {
        /// <summary>
        /// List of keys that used on STORE method
        /// </summary>
        public ICollection<string> KEYS { get; }

        /// <summary>
        /// Get stored item in memory or create it
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="createItem">Your desired function</param>
        /// <param name="ttl">Time to live in seconds - 0 means unlimited</param>
        /// <returns></returns>
        Task<string?> GetOrCreate_Async(string key, Func<string> createItem, int ttl = 0);

        /// <summary>
        /// Get an item from cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string?> Get_Async(string key);

        /// <summary>
        /// Store all setting items in memory
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        void Store(Dictionary<string, string> items);

        /// <summary>
        /// Get all data that saved in cache
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, string?>> GetAll();
    }
}
