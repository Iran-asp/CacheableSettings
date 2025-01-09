using CacheableSettings.Library;
using Microsoft.Extensions.DependencyInjection;

namespace CacheableSettings.ConsoleApp
{
    internal class Program
    {
        static async void Main(string[] args)
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

            service.TTL = 120;
            service.Store(settings);

            var value = await service.GetOrCreate_Async("item1", () => "");

            Console.WriteLine(value);
            Console.ReadLine();

        }
    }
}
