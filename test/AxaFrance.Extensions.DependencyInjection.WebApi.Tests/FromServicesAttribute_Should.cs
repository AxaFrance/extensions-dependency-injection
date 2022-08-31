using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    using System.Web.Http.ModelBinding;

    public class FromServicesAttribute_Should
    {
        [Fact]
        public void InheriteOfModelBinderAttribute()
        {
            var fromServices = new FromServicesAttribute();
            
            Assert.IsAssignableFrom<ModelBinderAttribute>(fromServices);
        }

        [Fact]
        public void ReturnFromServicesModelBinderTypeWhenCallBinderType()
        {
            var fromServices = new FromServicesAttribute();
            var modelBinder = fromServices.BinderType;
            Assert.Equal(typeof(FromServicesModelBinder), modelBinder);
        }
    }
}
