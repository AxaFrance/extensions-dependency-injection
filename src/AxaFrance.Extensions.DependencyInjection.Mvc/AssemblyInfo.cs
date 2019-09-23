using System.Runtime.CompilerServices;
using System.Web;

using AxaFrance.Extensions.DependencyInjection.Mvc;

[assembly: PreApplicationStartMethod(typeof(PreApplicationStartCode), "Start")]
[assembly: InternalsVisibleTo("AxaFrance.Extensions.DependencyInjection.Mvc.Tests")]
