namespace AxaFrance.Extensions.DependencyInjection.WebApi
{
    using System;
    using System.Web.Http.ModelBinding;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromServicesAttribute : ModelBinderAttribute
    {
        public FromServicesAttribute()
            : base(typeof(FromServicesModelBinder))
        {
        }
    }
}   