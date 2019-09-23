namespace AxaFrance.Extensions.DependencyInjection.WebApi
{
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.ModelBinding;

    internal class FromServicesModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var dependencyScope = actionContext.Request.GetDependencyScope();
            var service = dependencyScope.GetService(bindingContext.ModelType);
            bindingContext.Model = service;
            return service != null;
        }
    }
}