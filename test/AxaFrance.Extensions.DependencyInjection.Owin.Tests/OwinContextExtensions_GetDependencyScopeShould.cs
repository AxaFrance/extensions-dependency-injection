using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    [TestClass]
    public class OwinContextExtensions_GetDependencyScopeShould
    {
        private IOwinContext context;

        [TestInitialize]
        public void BeforeEach()
        {
            context = new OwinContext();
        }

        [TestMethod]
        public void ReturnNull_WhenNoServiceScopeWasSetYet()
        {
            IServiceScope serviceScope = context.GetDependencyScope();

            Assert.IsNull(serviceScope);
        }

        [TestMethod]
        public void ReturnServiceScope_WhenServiceScopeWasSet()
        {
            var original = new ServiceCollection().BuildServiceProvider()
                                                  .CreateScope();
            context.Environment.Add(OwinContextExtensions.PerRequestServiceScopeKey, original);

            var serviceScope = context.GetDependencyScope();

            Assert.AreSame(original, serviceScope);
        }
    }
}
