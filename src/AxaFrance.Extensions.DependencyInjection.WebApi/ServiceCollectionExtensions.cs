using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace AxaFrance.Extensions.DependencyInjection.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetCallingAssembly();

            IEnumerable<Type> apiControllerTypes = currentAssembly.GetTypes()
                .Where(type => typeof(ApiController).IsAssignableFrom(type));

            foreach (var apiControllerType in apiControllerTypes)
            {
                services.AddTransient(apiControllerType);
            }

            return services;
        }
    }
}
