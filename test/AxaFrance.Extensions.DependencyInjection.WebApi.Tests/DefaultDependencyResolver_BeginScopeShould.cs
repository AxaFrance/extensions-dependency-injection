using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    [TestClass]
    public class DefaultDependencyResolver_BeginScopeShould
    {
        private DefaultDependencyResolver resolver;
        private Mock<IServiceProvider> serviceProvider;
        private Mock<IServiceScope> serviceScope;
        private Mock<IServiceScopeFactory> serviceScopeFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.As<ISupportRequiredService>()
                .Setup(o => o.GetRequiredService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
            serviceScope = new Mock<IServiceScope>();
            resolver = new DefaultDependencyResolver(serviceProvider.Object);
            serviceScopeFactory.Setup(o => o.CreateScope())
                .Returns(serviceScope.Object);
        }

        [TestMethod]
        public void ProvideNewServiceScope_WithScopedServiceProvider()
        {
            var scopedServiceProvider = new Mock<IServiceProvider>();
            var expectedObjectInstance = new object();
            scopedServiceProvider.Setup(o => o.GetService(typeof(object)))
                .Returns(expectedObjectInstance);
            serviceScope.Setup(o => o.ServiceProvider)
                .Returns(scopedServiceProvider.Object);

            var dependencyScope = resolver.BeginScope();

            Assert.AreSame(expectedObjectInstance, dependencyScope.GetService(typeof(object)));
        }

        [TestMethod]
        public void BeADependencyScope()
        {
            Assert.IsInstanceOfType(resolver, typeof(DefaultDependencyScope));
        }
    }
}
