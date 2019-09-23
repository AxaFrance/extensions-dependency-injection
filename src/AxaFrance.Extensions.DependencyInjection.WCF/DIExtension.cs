namespace AxaFrance.Extensions.DependencyInjection.WCF
{
    using System;
    using System.ServiceModel;
    using Microsoft.Extensions.DependencyInjection;

    internal class DIExtension : IExtension<InstanceContext>
    {
        private IServiceScope serviceScope;

        public IServiceScope GetServiceScope(IServiceProvider serviceProvider)
        {
            return this.serviceScope = this.serviceScope ??
                (this.serviceScope = serviceProvider.CreateScope());
        }

        public void ReleaseServiceScope()
        {
            this.serviceScope?.Dispose();
        }

        public void Attach(InstanceContext owner)
        {
        }

        public void Detach(InstanceContext owner)
        {
        }
    }
}