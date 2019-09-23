using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.Mvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMvc(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            foreach (var @type in assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IController).IsAssignableFrom(t)
                            && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)))
            {
                services.AddTransient(@type);
            }

            return services;
        }
    }
}
