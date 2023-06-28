# AxaFrance Dependency Injection

[![Continuous Integration](https://github.com/AxaFrance/extensions-dependency-injection/actions/workflows/extensions-dependency-injection.yml/badge.svg)](https://github.com/AxaFrance/extensions-dependency-injection/actions/workflows/extensions-dependency-injection.yaml) [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=AxaFrance_extensions-dependency-injection&metric=alert_status)](https://sonarcloud.io/dashboard?id=AxaFrance_extensions-dependency-injection) [![Reliability](https://sonarcloud.io/api/project_badges/measure?project=AxaFrance_extensions-dependency-injection&metric=reliability_rating)](https://sonarcloud.io/component_measures?id=AxaFrance_extensions-dependency-injection&metric=reliability_rating) [![Security](https://sonarcloud.io/api/project_badges/measure?project=AxaFrance_extensions-dependency-injection&metric=security_rating)](https://sonarcloud.io/component_measures?id=AxaFrance_extensions-dependency-injection&metric=security_rating) [![Code Corevage](https://sonarcloud.io/api/project_badges/measure?project=AxaFrance_extensions-dependency-injection&metric=coverage)](https://sonarcloud.io/component_measures?id=AxaFrance_extensions-dependency-injection&metric=Coverage)

## About
This package allows projects running on older version of .NET Framework to use the new dependency injection framework introduced with ASP.NET Core.
It works with OWIN, MVC and WebApi web applications as well as WCF services.

This gives an easier migration path to those projects towards ASP.NET Core by allowing you to write your Injection configuration as if you were on ASP.NET Core.

## Packages

```powershell
Install-Package AxaFrance.Extensions.DependencyInjection.Mvc
```

```powershell
Install-Package AxaFrance.Extensions.DependencyInjection.Owin
```

```powershell
Install-Package AxaFrance.Extensions.DependencyInjection.WCF
```

```powershell
Install-Package AxaFrance.Extensions.DependencyInjection.WebApi
```

## Getting Started

### Registering services
You can register your services like you would in ASP.NET Core:
```csharp
services.AddScoped<IScopedService, ScopedService>()
        .AddTransient<ITransientService, TransientService>()
        .AddSingleton<ISingletonService, SingletonService>();
```

### Using services
Your services registered can now be passed to constructors:
```csharp
public HomeController(ISingletonService singletonService, IServiceProvider serviceProvider)
{
    this.singletonService = singletonService;
    this.serviceProvider = serviceProvider;
    this.transientServices = Enumerable.Range(0, 10)
                                        .Select(_ => this.serviceProvider.GetService<ITransientService>());
}
```

The `IServiceProvider` interface can be used to programmatically get service at runtime.


In WebApi/Mvc you can even use the `[FromService]` attribute in controller actions:
```cs
public ActionResult Index([FromServices] IScopedService scopedService)
{
    //Logic
}
```

### WCF
1. Install the WCF package:
```ps
Install-Package AxaFrance.Extensions.DependencyInjection.WCF
```

2. Create a class that inherits `DIServiceHostFactory` to register your services:
```csharp
public class WithDependencyInjectionServiceFactory : DIServiceHostFactory
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDataProvider, SampleDataProvider>();
        services.AddTransient<ISampleService, SampleService>();
    }
}
```

3. Declare this class as the factory in the `.svc` of your webservice:
```xml
<%@ ServiceHost Language="C#" Debug="true" Service="AxaFrance.Extensions.DependencyInjection.WCF.Sample.SampleService" Factory="AxaFrance.Extensions.DependencyInjection.WCF.Sample.WithDependencyInjectionServiceFactory" %>
```

A [sample](./samples/AxaFrance.Extensions.DependencyInjection.WCF.Sample/) app is available.

### OWIN
In an OWIN application, you must register a startup class using the `[OwinStartup]` attribute:
```cs
[assembly: OwinStartup(typeof(Owin.Sample.Startup))]

namespace Owin.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<IScopedService, ScopedService>()
                    .AddTransient<ITransientService, TransientService>()
                    .AddSingleton<ISingletonService, SingletonService>();
            
            // Register the service provider
            app.UseScopedServiceProvider(services.BuildServiceProvider());

            //...
        }
    }
}
```

A [sample](./samples/AxaFrance.Extensions.DependencyInjection.Owin.Sample) is also available.

### WebApi & MVC
Install the WebApi package:
```ps
Install-Package AxaFrance.Extensions.DependencyInjection.WebApi
Install-Package AxaFrance.Extensions.DependencyInjection.Mvc
```

Register your service collection in `Global.asax.cs`:
```cs
IServiceProvider provider = new ServiceCollection()
    .AddScoped<IScopedService, ScopedService>()
    .AddSingleton<ISingletonService, SingletonService>()
    .AddTransient<ITransientService, TransientService>()
    .AddWebApi()
    .AddMvc()
    .BuildServiceProvider();
System.Web.Mvc.DependencyResolver.SetResolver(new Mvc.DefaultDependencyResolver(provider));

GlobalConfiguration.Configure(config => {
    // ...
    config.DependencyResolver = new DefaultDependencyResolver(provider);
    // ...
});
```
You need to configure the resolver for both MVC and WebApi if you use both in your application

A [sample](./samples/AxaFrance.Extensions.DependencyInjection.WebApi.Sample) is also available.

## Dependencies

### For all packages

- Microsoft.Extensions.DependencyInjection.Abstractions

### For MVC
- Microsoft.AspNet.Mvc

### For Owin
- Microsoft.Owin
- Owin

### For WCF
- System.ServiceModel.Primitives
- Microsoft.Extensions.DependencyInjection

### For WebApi
- Microsoft.AspNet.WebApi

## Contributing

We welcome all contributions. Our contribution guidelines can be found [here](./CONTRIBUTING.md).

## Acknowledgement 

Thanks to our amazing contributors:

* [Simon DIB](https://github.com/sdib)
* [Julien VANDENBUSSCHE](https://github.com/ng-julien)
* [Guillaume DELAHAYE](https://twitter.com/g7ed6e)
* [Antoine BLANCKE](https://github.com/antoineblancke)
* [Jean-Lou PIERME](https://github.com/JLou)
* [Rémi JACQUART](https://github.com/remija)
