using System;

namespace AxaFrance.Extensions.DependencyInjection.WCF.Sample
{
    public interface IDataProvider : IDisposable
    {
        string GetData(int value);
        string GetSuffix();
    }
}