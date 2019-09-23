using System;
using System.Collections.Generic;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Services
{
    public interface IScopedService : IDisposable
    {
        IEnumerable<string> GetValues();
    }
}