using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    [TestClass]
    public class DefaultDependencyScope_GetServiceShould
    {
        private DefaultDependencyScope _defaultDependencyScope;
        private object implementationInstance;
        private IServiceProvider serviceProvider;

        [TestInitialize]
        public void BeforeEach()
        {
            implementationInstance = new object();
            serviceProvider = new ServiceCollection()
                .AddSingleton(implementationInstance)
                .AddTransient(typeof(ITestService), typeof(TestService))
                .AddTransient(typeof(ITestService), typeof(AnotherTestService))
                .BuildServiceProvider();
            _defaultDependencyScope = new DefaultDependencyScope(serviceProvider.CreateScope());
        }

        [TestMethod]
        public void ResolveOneServiceOfOneType()
        {
            var instance = _defaultDependencyScope.GetService(typeof(object));

            Assert.AreSame(implementationInstance, instance);
        }

        [TestMethod]
        public void ResolveMultipleServicesOfOneType()
        {
            var instances = _defaultDependencyScope.GetServices(typeof(ITestService));

            Assert.IsTrue(instances.Any(o => o is TestService));
            Assert.IsTrue(instances.Any(o => o is AnotherTestService));
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
