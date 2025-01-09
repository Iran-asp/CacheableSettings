using CacheableSettings.Library;
using Microsoft.Extensions.DependencyInjection;

namespace CacheableSettings.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cacheService = new ServiceCollection()
                .AddSingleton<ISettingStore, SettingStore>()
                .AddMemoryCache()
                .BuildServiceProvider();

            var service = cacheService.GetService<ISettingStore>();

            var settings = new Dictionary<string, string>
            {
                {"item1","Hi"},
                {"item2", "20"}
            };

            service.Store(settings);

            var value = await service.Get_Async("item1");
            var value2 = await service.GetOrCreate_Async("key1", () => "new data");
            var value3 = await service.GetOrCreate_Async("key2", () => "new data 2", 120);

            Console.WriteLine(value);
            Console.WriteLine(value2);
            Console.WriteLine(value3);
            Console.ReadLine();

        }
    }
}
