using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    public class OwinContextExtensions_SetDependencyScopeShould
    {
        private readonly IOwinContext context;

        public OwinContextExtensions_SetDependencyScopeShould()
        {
            this.context = new OwinContext();
        }

        [Fact]
        public void ReturnServiceScope_WhenServiceScopeWasSet()
        {
            var original = new ServiceCollection().BuildServiceProvider()
                                                  .CreateScope();
            context.Environment.Add(OwinContextExtensions.PerRequestServiceScopeKey, original);

            var serviceScope = context.GetDependencyScope();

            Assert.Same(original, serviceScope);
        }

        [Fact]
        public void NotOverrideExistingServiceScope_WhenServiceScopeWasAlreadySet()
        {
            var original = new ServiceCollection().BuildServiceProvider()
                                                  .CreateScope();
            context.SetDependencyScope(original);

            var secondServiceScope = new ServiceCollection().BuildServiceProvider()
                                                            .CreateScope();
            context.SetDependencyScope(secondServiceScope);

            var serviceScope = context.GetDependencyScope();

            Assert.Same(original, serviceScope);
        }
    }
}
