namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FromServicesModelBinder_BindModelShould
    {
        public interface IService
        {
        }

        [TestMethod]
        public void ReturnInstanceOfModelType()
        {
            var expectedType = typeof(IService);
            var dependencyResolverMock = new Mock<IDependencyResolver>();
            dependencyResolverMock.Setup(d => d.GetService(expectedType))
                                  .Returns(new Service());
            DependencyResolver.SetResolver(dependencyResolverMock.Object);

            var fromServicesModelBinder = new FromServicesModelBinder();
            var model = fromServicesModelBinder.BindModel(
                new ControllerContext(),
                new ModelBindingContext
                    {
                        ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, expectedType)
                    });

            Assert.IsInstanceOfType(model, expectedType);
        }

        public class Service : IService
        {
        }
    }
}