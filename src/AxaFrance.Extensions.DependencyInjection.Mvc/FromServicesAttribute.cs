namespace AxaFrance.Extensions.DependencyInjection.Mvc
{
    using System;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Parameter, Inherited = false)]
    public sealed class FromServicesAttribute : CustomModelBinderAttribute
    {
        private readonly ModelBinderAttribute modelBinder;

        public FromServicesAttribute()
        {
            this.modelBinder = new ModelBinderAttribute(typeof(FromServicesModelBinder));
        }

        public override IModelBinder GetBinder()
        {
            return this.modelBinder.GetBinder();
        }
    }
}