using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eGym.MVC.Binders
{
    public class CustomBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
                return new BinderTypeModelBinder(typeof(DecimalBinder));
            if (context.Metadata.ModelType == typeof(double) || context.Metadata.ModelType == typeof(double?))
                return new BinderTypeModelBinder(typeof(DoubleBinder));
            if (context.Metadata.ModelType == typeof(IEnumerable<string>) || context.Metadata.ModelType == typeof(List<string>) || context.Metadata.ModelType == typeof(string[]))
                return new BinderTypeModelBinder(typeof(EnumerableStringBinder));
            if (context.Metadata.ModelType == typeof(IEnumerable<int>) || context.Metadata.ModelType == typeof(List<int>) || context.Metadata.ModelType == typeof(int[]))
                return new BinderTypeModelBinder(typeof(EnumerableIntBinder));
            if (context.Metadata.ModelType == typeof(IEnumerable<short>) || context.Metadata.ModelType == typeof(List<short>) || context.Metadata.ModelType == typeof(short[]))
                return new BinderTypeModelBinder(typeof(EnumerableShortBinder));
            return null;
        }
    }
}
