namespace AxaFrance.Extensions.DependencyInjection.Mvc
{
    using System.Web.Mvc;

    internal class FromServicesModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var service = DependencyResolver.Current.GetService(bindingContext.ModelType);
            return service;
        }
    }
}