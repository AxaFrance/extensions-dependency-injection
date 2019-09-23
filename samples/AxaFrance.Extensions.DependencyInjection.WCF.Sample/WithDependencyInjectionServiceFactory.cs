using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    public class WithDependencyInjectionServiceFactory : DIServiceHostFactory
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataProvider, SampleDataProvider>();
            services.AddTransient<ISampleService, SampleService>();
        }
    }
}