namespace AxaFrance.Extensions.DependencyInjection.Mvc.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FromServicesAttribute_GetBinderShould
    {
        [TestMethod]
        public void ReturnInstanceOfTypeFromServicesModelBinder()
        {
            var fromServices = new FromServicesAttribute();

            var modelBinder = fromServices.GetBinder();

            Assert.IsInstanceOfType(modelBinder, typeof(FromServicesModelBinder));
        }
    }
}