using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    [TestClass]
    public class ServiceCollectionExtensions_AddMvcShould
    {
        private IServiceCollection collection;

        [TestInitialize]
        public void BeforeEach()
        {
            collection = new ServiceCollection();
        }

        [TestMethod]
        public void RegisterEveryClassExtendingControllerFoundInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.IsNotNull(provider.GetService(typeof(TestController)));
        }

        [TestMethod]
        public void NotRegisterAbstractAndGenericClassExtendingControllerFoundInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.IsNull(provider.GetService(typeof(GenericController<string>)));
            Assert.IsNull(provider.GetService(typeof(AbstractController)));
        }

        [TestMethod]
        public void NotRegisterClassExtendingControllerFoundWhichNameDoesNotEndWithControllerInCurrentAssembly()
        {
            collection.AddMvc();

            var provider = collection.BuildServiceProvider();
            Assert.IsNull(provider.GetService(typeof(Dummy)));
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
