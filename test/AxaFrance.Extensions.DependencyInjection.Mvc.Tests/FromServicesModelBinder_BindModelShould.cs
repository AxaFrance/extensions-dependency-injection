using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    using System.Web.Mvc;

    using Moq;

    public class FromServicesModelBinder_BindModelShould
    {
        public interface IService
        {
        }

        [Fact]
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

            Assert.IsAssignableFrom<IService>(model);
        }

        public class Service : IService
        {
        }
    }
}
