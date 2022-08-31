using Xunit;

namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    public class FromServicesAttribute_GetBinderShould
    {
        [Fact]
        public void ReturnInstanceOfTypeFromServicesModelBinder()
        {
            var fromServices = new FromServicesAttribute();

            var modelBinder = fromServices.GetBinder();

            Assert.IsType<FromServicesModelBinder>(modelBinder);
        }
    }
}
