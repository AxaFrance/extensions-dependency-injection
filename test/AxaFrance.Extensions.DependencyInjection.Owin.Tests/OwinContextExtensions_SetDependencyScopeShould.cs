using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    [TestClass]
    public class OwinContextExtensions_SetDependencyScopeShould
    {
        private IOwinContext context;

        [TestInitialize]
        public void BeforeEach()
        {
            context = new OwinContext();
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

        [TestMethod]
        public void NotOverrideExistingServiceScope_WhenServiceScopeWasAlreadySet()
        {
            var original = new ServiceCollection().BuildServiceProvider()
                                                  .CreateScope();
            context.SetDependencyScope(original);

            var secondServiceScope = new ServiceCollection().BuildServiceProvider()
                                                            .CreateScope();
            context.SetDependencyScope(secondServiceScope);

            var serviceScope = context.GetDependencyScope();

            Assert.AreSame(original, serviceScope);
        }
    }
}
