using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.WebApi
{
    public class DefaultDependencyScope : IDependencyScope
    {
        private readonly IServiceScope serviceScope;

        public DefaultDependencyScope(IServiceScope serviceScope)
        {
            this.serviceScope = serviceScope;
        }

        public void Dispose()
        {
            serviceScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return serviceScope.ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return serviceScope.ServiceProvider.GetServices(serviceType);
        }
    }
}
