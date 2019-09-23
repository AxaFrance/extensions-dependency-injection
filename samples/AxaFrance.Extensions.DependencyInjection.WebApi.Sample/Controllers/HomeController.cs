namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.Extensions.DependencyInjection;

    using Mvc;

    using Services;

    public class HomeController : Controller
    {
        private readonly IServiceProvider serviceProvider;

        private readonly ISingletonService singletonService;

        private readonly IEnumerable<ITransientService> transientServices;

        public HomeController(ISingletonService singletonService, IServiceProvider serviceProvider)
        {
            this.singletonService = singletonService;
            this.serviceProvider = serviceProvider;
            this.transientServices = Enumerable.Range(0, 10)
                                               .Select(_ => this.serviceProvider.GetService<ITransientService>());
        }

        // GET: Home
        public ActionResult Index([FromServices] IScopedService scopedService) => Content($"{this.singletonService.Id}-{string.Join("-", scopedService.GetValues())}-{string.Join("-", this.transientServices.Select(x => x.Id))}");
    }
}