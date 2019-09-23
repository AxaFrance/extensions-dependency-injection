using System;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Sample.Services
{
    public interface IScopedService : IDisposable
    {
        string Id { get; }
    }

    public class ScopedService : IScopedService
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public void Dispose() => System.Diagnostics.Debug.WriteLine("Scoped Service is disposed");
    }
}