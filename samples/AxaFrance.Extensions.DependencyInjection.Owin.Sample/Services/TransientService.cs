using System;

namespace AxaFrance.Extensions.DependencyInjection.Owin.Sample.Services
{
    public interface ITransientService
    {
        string Id { get; }
    }

    public class TransientService : ITransientService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}
