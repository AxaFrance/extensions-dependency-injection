using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.Mvc
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType) => this.GetServiceScope()
            .ServiceProvider.GetService(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => this.GetServiceScope()
            .ServiceProvider.GetServices(serviceType);

        private IServiceScope GetServiceScope()
        {
            if (HttpContext.Current.Items[ScopedLifetimeHttpModule.HttpContextKey] == null)
            {
                var serviceScope = this.serviceProvider.CreateScope();
                HttpContext.Current.Items[ScopedLifetimeHttpModule.HttpContextKey] = serviceScope;
                return serviceScope;
            }

            return (IServiceScope)HttpContext.Current.Items[ScopedLifetimeHttpModule.HttpContextKey];
        }
    }
}
