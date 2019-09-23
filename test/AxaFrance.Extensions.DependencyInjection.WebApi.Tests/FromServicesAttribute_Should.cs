namespace AxaFrance.Extensions.DependencyInjection.WebApi.Tests
{
    using System.Web.Http.ModelBinding;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FromServicesAttribute_Should
    {
        [TestMethod]
        public void InheriteOfModelBinderAttribute()
        {
            var fromServices = new FromServicesAttribute();
            
            Assert.IsInstanceOfType(fromServices, typeof(ModelBinderAttribute));
        }

        [TestMethod]
        public void ReturnFromServicesModelBinderTypeWhenCallBinderType()
        {
            var fromServices = new FromServicesAttribute();
            var modelBinder = fromServices.BinderType;
            Assert.AreEqual(typeof(FromServicesModelBinder), modelBinder);
        }
    }
}