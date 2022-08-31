using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Hosting;
    using System.Web.Http.Metadata;
    using System.Web.Http.Metadata.Providers;
    using System.Web.Http.ModelBinding;

    using Moq;

    public class FromServicesModelBinder_BindModelShould
    {
        private readonly HttpActionContext httpActionContext;

        private readonly Mock<IDependencyResolver> dependencyResolverMock;

        public interface IService
        {
        }

        public FromServicesModelBinder_BindModelShould()
        {
            this.dependencyResolverMock = new Mock<IDependencyResolver>();
            var httpConfiguration = new HttpConfiguration
                                        {
                                            DependencyResolver = this.dependencyResolverMock.Object
                                        };
            var httpControllerContext = new HttpControllerContext
                                            {
                                                Configuration = httpConfiguration,
                                                Request = new HttpRequestMessage
                                                              {
                                                                  Properties =
                                                                      {
                                                                          { HttpPropertyKeys.DependencyScope, httpConfiguration.DependencyResolver }
                                                                      }
                                                              }
                                            };
            this.httpActionContext = new HttpActionContext
                                        {
                                            ControllerContext = httpControllerContext,
                                        };
        }

        [Fact]
        public void ReturnTrueWhenModelTypeIsResolved()
        {
            var expectedType = typeof(IService);
            this.dependencyResolverMock.Setup(d => d.GetService(expectedType))
                .Returns(new Service());
            var fromServicesModeBinder = new FromServicesModelBinder();
            var modelBindingContext = new ModelBindingContext
                                          {
                                              ModelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), null, null, expectedType, null)
                                          };

            var binded = fromServicesModeBinder.BindModel(this.httpActionContext, modelBindingContext);

            Assert.IsAssignableFrom<IService>(modelBindingContext.Model);
            Assert.True(binded);
        }

        [Fact]
        public void ReturnFalseWhenModelTypeIsNotResolved()
        {
            this.dependencyResolverMock.Setup(d => d.GetService(It.IsAny<Type>()))
                .Returns(null);
            var fromServicesModeBinder = new FromServicesModelBinder();
            var modelBindingContext = new ModelBindingContext
                                          {
                                              ModelMetadata = new ModelMetadata(new EmptyModelMetadataProvider(), null, null, typeof(IService), null)
                                          };

            var binded = fromServicesModeBinder.BindModel(this.httpActionContext, modelBindingContext);

            Assert.Null(modelBindingContext.Model);
            Assert.False(binded);
        }

        public class Service : IService
        {
        }
    }
}
