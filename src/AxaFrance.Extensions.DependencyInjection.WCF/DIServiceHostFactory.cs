namespace AxaFrance.Extensions.DependencyInjection.WCF
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class DIServiceHostFactory : ServiceHostFactory
    {
        protected abstract void ConfigureServices(IServiceCollection services);

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return new DIServiceHost(serviceProvider, serviceType, baseAddresses);
        }
    }
}
