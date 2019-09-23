using AxaFrance.Extensions.DependencyInjection.Owin;
using System;

namespace Owin
{
    public static class AppBuillderExtensions
    {
        public static IAppBuilder UseScopedServiceProvider(this IAppBuilder builder, IServiceProvider serviceProvider)
        {
            return builder.Use<ScopedServiceProviderMiddleware>(serviceProvider);
        }
    }
}