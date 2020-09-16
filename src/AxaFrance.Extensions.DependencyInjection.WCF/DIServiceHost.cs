namespace AxaFrance.Extensions.DependencyInjection.WCF
{
    using System;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using Microsoft.Extensions.DependencyInjection;

    public class DIServiceHost : ServiceHost
    {
        public DIServiceHost(IServiceProvider serviceProvider, Type serviceType, Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.ApplyServiceBehaviors(serviceProvider);
            this.ApplyContractBehaviors(serviceProvider);
            foreach (var contractDescription in this.ImplementedContracts.Values)
            {
                var diInstanceProvider = new DIInstanceProvider(serviceProvider, contractDescription.ContractType);
                var contractBehavior = new DIContractBehavior(diInstanceProvider);

                contractDescription.Behaviors.Add(contractBehavior);
            }
        }

        private void ApplyContractBehaviors(IServiceProvider serviceProvider)
        {
            var registeredContractBehaviors = serviceProvider.GetServices<IContractBehavior>();

            foreach (var contractBehavior in registeredContractBehaviors)
            {
                foreach (var contractDescription in this.ImplementedContracts.Values)
                {
                    contractDescription.Behaviors.Add(contractBehavior);
                }
            }
        }

        private void ApplyServiceBehaviors(IServiceProvider serviceProvider)
        {
            var registeredServiceBehaviors = serviceProvider.GetServices<IServiceBehavior>();

            foreach (var serviceBehavior in registeredServiceBehaviors)
            {
                this.Description.Behaviors.Add(serviceBehavior);
            }
        }
    }
}
