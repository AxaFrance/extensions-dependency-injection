using System;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.WebApi
{
    public class DefaultDependencyResolver : DefaultDependencyScope, IDependencyResolver
    {
        private readonly IServiceProvider rootServiceProvider;

        public DefaultDependencyResolver(IServiceProvider rootServiceProvider)
            : base(rootServiceProvider.CreateScope())
        {
            this.rootServiceProvider = rootServiceProvider;
        }

        public IDependencyScope BeginScope()
        {
            return new DefaultDependencyScope(rootServiceProvider.CreateScope());
        }
    }
}
