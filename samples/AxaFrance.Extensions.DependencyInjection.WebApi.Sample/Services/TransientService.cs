using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Services
{
    public class TransientService : ITransientService
    {
        public string Id { get; } = Guid.NewGuid().ToString();
    }
}