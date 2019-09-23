namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample
{
    public static class MvcConfig
    {
        public static void RegisterDependencyResolver()
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new Mvc.DefaultDependencyResolver(ServiceProviderConfig.ServiceProvider));
        }
    }
}