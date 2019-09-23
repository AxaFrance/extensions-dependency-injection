using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    [TestClass]
    public class DefaultDependencyScope_DisposeShould
    {
        private DefaultDependencyScope defaultDependencyScope;
        private Mock<IServiceScope> serviceScope;

        [TestInitialize]
        public void BeforeEach()
        {
            serviceScope = new Mock<IServiceScope>();
            defaultDependencyScope = new DefaultDependencyScope(serviceScope.Object);
        }

        [TestMethod]
        public void DisposeServiceScope()
        {
            defaultDependencyScope.Dispose();

            serviceScope.Verify(o => o.Dispose());
        }
    }
}
