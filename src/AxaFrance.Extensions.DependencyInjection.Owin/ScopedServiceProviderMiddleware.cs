using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using System;
using System.Threading.Tasks;

namespace AxaFrance.Extensions.DependencyInjection.Owin
{
    public class ScopedServiceProviderMiddleware : OwinMiddleware
    {
        private readonly IServiceProvider rootServiceProvider;

        public ScopedServiceProviderMiddleware(OwinMiddleware next, IServiceProvider rootServiceProvider)
            : base(next)
        {
            this.rootServiceProvider = rootServiceProvider;
        }

        public override Task Invoke(IOwinContext context)
        {
            using (IServiceScope serviceScope = rootServiceProvider.CreateScope())
            {
                context.SetDependencyScope(serviceScope);
                return Next.Invoke(context);
            }
        }
    }
}
