using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Sample
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}
