using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    public class ServiceCollectionExtensions_AddMvcShould
    {
        private readonly IServiceCollection collection;

        public ServiceCollectionExtensions_AddMvcShould()
        {
            this.collection = new ServiceCollection();
        }

        [Fact]
        public void RegisterEveryClassExtendingControllerFoundInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.NotNull(provider.GetService(typeof(TestController)));
        }

        [Fact]
        public void NotRegisterAbstractAndGenericClassExtendingControllerFoundInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.Null(provider.GetService(typeof(GenericController<string>)));
            Assert.Null(provider.GetService(typeof(AbstractController)));
        }

        [Fact]
        public void NotRegisterClassExtendingControllerFoundWhichNameDoesNotEndWithControllerInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.Null(provider.GetService(typeof(Dummy)));
        }
    }

    public class TestController : Controller
    {
    }

    public class Dummy : Controller
    {
    }

    public abstract class AbstractController : Controller
    {
        
    }

    public class GenericController<T> : Controller
    {
        
    }
}
