using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CacheableSettings.Library
{
    public interface ISettingStore
    {
        /// <summary>
        /// Time to live (seconds)
        /// <para>Default value is 60 seconds</para>
        /// </summary>
        public int TTL { get; set; }

        /// <summary>
        /// Setting collection
        /// </summary>
        public Dictionary<string, string>? SettingItems { get; set; }

        /// <summary>
        /// Get stored item in memory or create it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="createItem"></param>
        /// <returns></returns>
        Task<string?> GetOrCreate(string key, Func<string> createItem);

        /// <summary>
        /// Store all setting items in memory
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        void Store(Dictionary<string, string> items);
    }
}
