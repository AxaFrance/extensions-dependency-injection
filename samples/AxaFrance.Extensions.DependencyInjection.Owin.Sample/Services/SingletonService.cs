using System;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Sample.Services
{
    public interface ISingletonService
    {
        string Id { get; }
    }

    public class SingletonService : ISingletonService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}