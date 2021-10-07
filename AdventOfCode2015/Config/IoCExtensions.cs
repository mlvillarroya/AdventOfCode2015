using System;
using AdventOfCode2015.Challenges;
using AdventOfCode2015.Helpers;
using AdventOfCode2015.Resources;
using AdventOfCode2015.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCode2015.Config
{
    public static class IoCExtensions
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection ConfigureIoC(IServiceCollection services)
        {
            services = ConfigureChallengeModels(services);
            services = ConfigureServices(services);
            services = ConfigureHelpers(services);

            return services;
        }
        
        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IFileWrapper, FileWrapper>();
            services.AddSingleton<IFileServer, FileServer>();

            return services;
        }

        private static IServiceCollection ConfigureChallengeModels(IServiceCollection services)
        {
            services.AddTransient<Challenge1A>();
            services.AddTransient<Challenge1B>();
            services.AddTransient<Challenge2A>();

            return services;
        }
        private static IServiceCollection ConfigureHelpers(IServiceCollection services)
        {
            services.AddSingleton<IPresentListParser, PresentListParser>();

            return services;
        }

    }
}