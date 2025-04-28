using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NameSorter.Application.Interfaces;
using NameSorter.Application.Services;

namespace SorterCLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var app = host.Services.GetRequiredService<ConsoleAppRunner>();
            await app.RunAsync(args);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddScoped<ISortService, SortService>();
                    services.AddScoped<IFileService, FileService>();
                    services.AddScoped<ConsoleAppRunner>(); 
                });
    }
}
