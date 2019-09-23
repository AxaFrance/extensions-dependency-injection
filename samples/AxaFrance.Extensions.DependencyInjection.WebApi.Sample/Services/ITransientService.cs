using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxaFrance.Extensions.DependencyInjection.WebApi.Sample.Services
{
    public interface ITransientService
    {
        string Id { get; }
    }
}
