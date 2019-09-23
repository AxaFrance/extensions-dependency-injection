using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    [TestClass]
    public class ServiceCollectionExtensions_AddWebApiShould
    {
        private IServiceCollection collection;

        [TestInitialize]
        public void BeforeEach()
        {
            collection = new ServiceCollection();
        }

        [TestMethod]
        public void RegisterEveryClassExtendingApiControllerFoundInCurrentAssembly()
        {
            collection.AddWebApi();

            var provider = collection.BuildServiceProvider();
            Assert.IsNotNull(provider.GetService(typeof(TestController)));
        }
    }

    public class TestController : ApiController
    {
    }
}
