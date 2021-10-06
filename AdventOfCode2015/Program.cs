using System;
using System.Threading.Tasks;
using AdventOfCode2015.Challenges;
using AdventOfCode2015.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace AdventOfCode2015
{
    class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            var challenge1A = host.Services.GetService<Challenge1A>();
            
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                        services.AddSingleton<IWebReader, WebReader>());
    }
}