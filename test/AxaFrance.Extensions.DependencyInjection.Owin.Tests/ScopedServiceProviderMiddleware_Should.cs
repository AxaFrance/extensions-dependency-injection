using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Owin;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Tests
{
    [TestClass]
    public class ScopedServiceProviderMiddleware_Should
    {
        private Mock<IServiceProvider> serviceProvider;
        private Mock<IServiceScope> serviceScope;
        private Mock<IServiceScopeFactory> serviceScopeFactory;

        [TestInitialize]
        public void BeforeEach()
        {
            serviceProvider = new Mock<IServiceProvider>();
            serviceScope = new Mock<IServiceScope>();
            serviceScopeFactory = new Mock<IServiceScopeFactory>();

            serviceProvider.Setup(o => o.GetService(typeof(IServiceScopeFactory)))
                           .Returns(serviceScopeFactory.Object);

            serviceScopeFactory.Setup(o => o.CreateScope())
                               .Returns(serviceScope.Object);
        }

        [TestMethod]
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

                Assert.IsNotNull(ambiantServiceScope);
                Assert.AreSame(serviceScope.Object, ambiantServiceScope);
            }
        }

        [TestMethod]
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

        [TestMethod]
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
