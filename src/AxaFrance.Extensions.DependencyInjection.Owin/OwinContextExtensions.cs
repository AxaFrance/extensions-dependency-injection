using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;

[assembly: InternalsVisibleTo("AxaFrance.Extensions.DependencyInjection.Owin.Tests")]
namespace AxaFrance.Extensions.DependencyInjection.Owin
{
    public static class OwinContextExtensions
    {
        internal static readonly string PerRequestServiceScopeKey = "AxaFrance.Extensions.DependencyInjection.Owin:PerRequestDependencyScope";

        public static IServiceScope GetDependencyScope(this IOwinContext context)
        {
            context.Environment.TryGetValue(PerRequestServiceScopeKey, out var serviceScope);
            return serviceScope as IServiceScope;
        }

        internal static void SetDependencyScope(this IOwinContext context, IServiceScope serviceScope)
        {
            if (context.GetDependencyScope() == null)
            {
                context.Environment.Add(PerRequestServiceScopeKey, serviceScope);
            }
        }
    }
}
