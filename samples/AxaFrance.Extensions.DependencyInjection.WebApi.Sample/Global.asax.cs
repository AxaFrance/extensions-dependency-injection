using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AxaFrance.Extensions.DependencyInjection.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            MvcConfig.RegisterDependencyResolver();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
