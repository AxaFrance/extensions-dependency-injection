using System.Web.Http;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.DependencyResolver = new DefaultDependencyResolver(ServiceProviderConfig.ServiceProvider);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{count}",
                defaults: new { count = RouteParameter.Optional }
            );
        }
    }
}
