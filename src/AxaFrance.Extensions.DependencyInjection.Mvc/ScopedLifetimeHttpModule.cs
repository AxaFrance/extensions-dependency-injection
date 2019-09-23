using System;
using System.Web;
using Microsoft.Extensions.DependencyInjection;

namespace AxaFrance.Extensions.DependencyInjection.Mvc
{
    public class ScopedLifetimeHttpModule : IHttpModule
    {
        public static readonly string HttpContextKey = "AxaFrance.Extensions.DependencyInjection.Mvc.ScopedLifetimeHttpModule:ServiceScopeKey";

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            (context ?? throw new ArgumentNullException(nameof(context))).EndRequest += OnEndRequest;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            IServiceScope scope = app.Context.Items[HttpContextKey] as IServiceScope;
            scope?.Dispose();
        }
    }
}
