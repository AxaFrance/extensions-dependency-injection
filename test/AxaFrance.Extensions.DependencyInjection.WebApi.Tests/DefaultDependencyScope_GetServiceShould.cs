using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    public class DefaultDependencyScope_GetServiceShould
    {
        private readonly DefaultDependencyScope _defaultDependencyScope;
        private readonly object implementationInstance;
        private readonly IServiceProvider serviceProvider;

        public DefaultDependencyScope_GetServiceShould()
        {
            implementationInstance = new object();
            serviceProvider = new ServiceCollection()
                .AddSingleton(implementationInstance)
                .AddTransient(typeof(ITestService), typeof(TestService))
                .AddTransient(typeof(ITestService), typeof(AnotherTestService))
                .BuildServiceProvider();
            _defaultDependencyScope = new DefaultDependencyScope(serviceProvider.CreateScope());
        }

        [Fact]
        public void ResolveOneServiceOfOneType()
        {
            var instance = _defaultDependencyScope.GetService(typeof(object));

            Assert.Same(implementationInstance, instance);
        }

        [Fact]
        public void ResolveMultipleServicesOfOneType()
        {
            var instances = _defaultDependencyScope.GetServices(typeof(ITestService)).ToList();

            Assert.Contains(instances, o => o is TestService);
            Assert.Contains(instances, o => o is AnotherTestService);
        }

        private interface ITestService
        {
        }

        private class TestService : ITestService
        {
        }

        private class AnotherTestService : ITestService
        {
        }
    }
}
