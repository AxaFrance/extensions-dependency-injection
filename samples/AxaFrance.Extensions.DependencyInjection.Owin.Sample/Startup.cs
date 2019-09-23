using System.Threading.Tasks;
using AxaFrance.Extensions.DependencyInjection.Owin.Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AxaFrance.Extensions.DependencyInjection.Owin.Sample.Startup))]

namespace AxaFrance.Extensions.DependencyInjection.Owin.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IScopedService, ScopedService>()
                    .AddTransient<ITransientService, TransientService>()
                    .AddSingleton<ISingletonService, SingletonService>();
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=31688
            app.UseScopedServiceProvider(services.BuildServiceProvider());

            app.Use((context, next) =>
            {
                var serviceScope = context.GetDependencyScope();

                var scopedService1 = serviceScope.ServiceProvider.GetService<IScopedService>();
                var scopedService2 = serviceScope.ServiceProvider.GetService<IScopedService>();
                var singletonService = serviceScope.ServiceProvider.GetService<ISingletonService>();
                var transientService = serviceScope.ServiceProvider.GetService<ITransientService>();

                string message = $"scopedService1 Id : {scopedService1.Id} | scopedService2 Id : {scopedService2.Id} | singleton ID : {singletonService.Id} | transient ID : {transientService.Id}";

                context.Response.WriteAsync(message);

                return Task.FromResult(0);
            });
        }
    }
}
