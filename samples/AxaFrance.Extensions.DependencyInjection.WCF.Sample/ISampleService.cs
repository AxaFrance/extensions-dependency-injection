using System.ServiceModel;

namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    [ServiceContract]
    public interface ISampleService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }
}
