using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    public class ServiceCollectionExtensions_AddWebApiShould
    {
        private readonly IServiceCollection collection;

        public ServiceCollectionExtensions_AddWebApiShould()
        {
            collection = new ServiceCollection();
        }

        [Fact]
        public void RegisterEveryClassExtendingApiControllerFoundInCurrentAssembly()
        {
            collection.AddWebApi();

            var provider = collection.BuildServiceProvider();
            Assert.NotNull(provider.GetService(typeof(TestController)));
        }
    }

    public class TestController : ApiController
    {
    }
}
