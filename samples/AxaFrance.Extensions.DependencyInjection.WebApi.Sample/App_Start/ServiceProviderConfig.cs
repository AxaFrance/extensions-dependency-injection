using System;
using AxaFrance.Extensions.DependencyInjection.Mvc;
using AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample
{
    public static class ServiceProviderConfig
    {
        public static IServiceProvider ServiceProvider { get; } = new ServiceCollection()
            .AddScoped<IScopedService, ScopedService>()
            .AddSingleton<ISingletonService, SingletonService>()
            .AddTransient<ITransientService, TransientService>()
            .AddWebApi()
            .AddMvc()
            .BuildServiceProvider();
    }
}
