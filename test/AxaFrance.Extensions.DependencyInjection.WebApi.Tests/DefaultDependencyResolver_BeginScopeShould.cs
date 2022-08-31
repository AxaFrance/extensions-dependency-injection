using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    public class DefaultDependencyResolver_BeginScopeShould
    {
        private readonly DefaultDependencyResolver resolver;
        private readonly Mock<IServiceProvider> serviceProvider;
        private readonly Mock<IServiceScope> serviceScope;
        private readonly Mock<IServiceScopeFactory> serviceScopeFactory;

        public DefaultDependencyResolver_BeginScopeShould()
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

        [Fact]
        public void ProvideNewServiceScope_WithScopedServiceProvider()
        {
            var scopedServiceProvider = new Mock<IServiceProvider>();
            var expectedObjectInstance = new object();
            scopedServiceProvider.Setup(o => o.GetService(typeof(object)))
                .Returns(expectedObjectInstance);
            serviceScope.Setup(o => o.ServiceProvider)
                .Returns(scopedServiceProvider.Object);

            var dependencyScope = resolver.BeginScope();

            Assert.Same(expectedObjectInstance, dependencyScope.GetService(typeof(object)));
        }

        [Fact]
        public void BeADependencyScope()
        {
            Assert.IsType<DefaultDependencyResolver>(resolver);
        }
    }
}
