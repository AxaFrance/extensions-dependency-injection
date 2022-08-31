using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    public class OwinContextExtensions_GetDependencyScopeShould
    {
        private readonly IOwinContext context;

        public OwinContextExtensions_GetDependencyScopeShould()
        {
            this.context = new OwinContext();
        }

        [Fact]
        public void ReturnNull_WhenNoServiceScopeWasSetYet()
        {
            IServiceScope serviceScope = context.GetDependencyScope();

            Assert.Null(serviceScope);
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
    }
}
