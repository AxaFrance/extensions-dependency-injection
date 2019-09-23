namespace AxaFrance.Extensions.DependencyInjection.WCF
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;
    using Microsoft.Extensions.DependencyInjection;

    internal class DIInstanceProvider : IInstanceProvider
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Type contractType;

        public DIInstanceProvider(IServiceProvider serviceProvider, Type contractType)
        {
            this.serviceProvider = serviceProvider;
            this.contractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext) => this.GetInstance(instanceContext, null);

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            DIExtension diExtension = instanceContext.Extensions.Find<DIExtension>();
            IServiceScope serviceScope = diExtension.GetServiceScope(this.serviceProvider);
            return serviceScope.ServiceProvider.GetService(this.contractType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            DIExtension diExtension = instanceContext.Extensions.Find<DIExtension>();
            diExtension.ReleaseServiceScope();
        }
    }
}