using System;
using System.Threading.Tasks;
using AdventOfCode2015.Challenges;
using AdventOfCode2015.Config;
using AdventOfCode2015.Resources;
using AdventOfCode2015.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace AdventOfCode2015
{
    internal static class Program
    {
        private static Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var challenge = host.Services.GetService<Challenge4>();
            
            return host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services = IoCExtensions.ConfigureIoC(services);
                });

    }
}