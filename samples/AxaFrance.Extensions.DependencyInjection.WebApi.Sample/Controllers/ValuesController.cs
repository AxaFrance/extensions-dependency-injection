namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Services;

    public class ValuesController : ApiController
    {
        private readonly IScopedService scopedService;

        public ValuesController(IScopedService scopedService)
        {
            this.scopedService = scopedService;
        }

        public IEnumerable<string> Get()
        {
            return this.scopedService.GetValues();
        }

        public IEnumerable<string> Get([FromServices] IScopedService service, int count)
        {
            return service.GetValues()
                          .Take(count);
        }
    }
}
