using System.Runtime.Serialization;

namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; } = true;

        [DataMember]
        public string StringValue { get; set; } = "Hello ";
    }
}