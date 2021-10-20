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
            services.AddSingleton<ILedMatrix, LedMatrix>();
            services.AddSingleton<IWireList, WireList>();
            services.AddSingleton<ILogicFunctions, LogicFunctions>();

            return services;
        }

        private static IServiceCollection ConfigureChallengeModels(IServiceCollection services)
        {
            services.AddTransient<Challenge1A>();
            services.AddTransient<Challenge1B>();
            services.AddTransient<Challenge2A>();
            services.AddTransient<Challenge3>();
            services.AddTransient<Challenge4>();
            services.AddTransient<Challenge5>();
            services.AddTransient<Challenge6>();
            services.AddTransient<Challenge7>();
            services.AddTransient<Challenge8>();
            services.AddTransient<Challenge9>();
            services.AddTransient<Challenge10>();
            services.AddTransient<Challenge11>();
            services.AddTransient<Challenge12>();
            services.AddTransient<Challenge13>();
            services.AddTransient<Challenge14>();
            services.AddTransient<Challenge14b>();
            services.AddTransient<Challenge15>();
            services.AddTransient<Challenge16>();

            return services;
        }
        private static IServiceCollection ConfigureHelpers(IServiceCollection services)
        {
            services.AddSingleton<IPresentListParser, PresentListParser>();
            services.AddSingleton<IEncrypting, Encrypting>();

            return services;
        }

    }
}