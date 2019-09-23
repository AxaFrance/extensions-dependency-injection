using System.Collections.Generic;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Services
{
    public class ScopedService : IScopedService
    {
        public void Dispose() => System.Diagnostics.Debug.WriteLine("dispose of IDisposable");

        public IEnumerable<string> GetValues()
        {
            yield return "value1";
            yield return "value2";
        }
    }
}