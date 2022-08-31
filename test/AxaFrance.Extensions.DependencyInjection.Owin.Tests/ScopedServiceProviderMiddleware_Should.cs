using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Testing;
using Moq;
using Owin;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    public class ScopedServiceProviderMiddleware_Should
    {
        private readonly Mock<IServiceProvider> serviceProvider;
        private readonly Mock<IServiceScope> serviceScope;
        private readonly Mock<IServiceScopeFactory> serviceScopeFactory;

        public ScopedServiceProviderMiddleware_Should()
        {
            serviceProvider = new Mock<IServiceProvider>();
            serviceScope = new Mock<IServiceScope>();
            serviceScopeFactory = new Mock<IServiceScopeFactory>();

            serviceProvider.Setup(o => o.GetService(typeof(IServiceScopeFactory)))
                           .Returns(serviceScopeFactory.Object);

            serviceScopeFactory.Setup(o => o.CreateScope())
                               .Returns(serviceScope.Object);
        }

        [Fact]
        public async Task AddDependencyScopeOnOwinContextForNextMiddlewares()
        {
            IServiceScope ambiantServiceScope = null;
            using (var testServer = TestServer.Create(app =>
                                                      {
                                                          app.UseScopedServiceProvider(serviceProvider.Object)
                                                             .Run(context =>
                                                                  {
                                                                      ambiantServiceScope = context.GetDependencyScope();
                                                                      return context.Response.WriteAsync("Testing");
                                                                  });
                                                      })
            )
            {
                await testServer.CreateRequest("/")
                                .GetAsync();

                Assert.NotNull(ambiantServiceScope);
                Assert.Same(serviceScope.Object, ambiantServiceScope);
            }
        }

        [Fact]
        public async Task DisposeServiceScopeBeforeTheRequestEndsGracefully()
        {
            using (var testServer = TestServer.Create(app =>
                                                      {
                                                          app
                                                              .Use((context, next) =>
                                                                   {
                                                                       next.Invoke();
                                                                       serviceScope.Verify(o => o.Dispose());
                                                                       return Task.CompletedTask;
                                                                   })
                                                              .UseScopedServiceProvider(serviceProvider.Object);
                                                      })
            )
            {
                await testServer.CreateRequest("/")
                                .GetAsync();
            }
        }

        [Fact]
        public async Task DisposeServiceScopeBeforeTheRequestEndsOnException()
        {
            using (var testServer = TestServer.Create(app =>
                                                      {
                                                          app.Use((context, next) =>
                                                                  {
                                                                      try
                                                                      {
                                                                          next.Invoke();
                                                                      }
                                                                      catch
                                                                      {
                                                                          serviceScope.Verify(o => o.Dispose());
                                                                      }

                                                                      return Task.CompletedTask;
                                                                  })
                                                             .UseScopedServiceProvider(serviceProvider.Object)
                                                             .Run(context => throw new Exception("something failed!"));
                                                      })
            )
            {
                await testServer.CreateRequest("/")
                                .GetAsync();
            }
        }
    }
}
