using System;

namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    public class SampleService : ISampleService
    {
        private readonly IDataProvider _dataProvider;

        public SampleService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string GetData(int value)
        {
            return _dataProvider.GetData(value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }

            if (composite.BoolValue)
            {
                composite.StringValue += _dataProvider.GetSuffix();
            }

            return composite;
        }
    }
}
