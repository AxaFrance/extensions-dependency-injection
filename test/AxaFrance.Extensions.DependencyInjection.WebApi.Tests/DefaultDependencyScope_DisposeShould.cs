using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    public class DefaultDependencyScope_DisposeShould
    {
        private readonly DefaultDependencyScope defaultDependencyScope;
        private readonly Mock<IServiceScope> serviceScope;

        public DefaultDependencyScope_DisposeShould()
        {
            this.serviceScope = new Mock<IServiceScope>();
            this.defaultDependencyScope = new DefaultDependencyScope(serviceScope.Object);
        }

        [Fact]
        public void DisposeServiceScope()
        {
            this.defaultDependencyScope.Dispose();

            serviceScope.Verify(o => o.Dispose());
        }
    }
}
