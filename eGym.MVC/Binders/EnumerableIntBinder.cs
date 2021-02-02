using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGym.MVC.Binders
{
    public class EnumerableIntBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult == ValueProviderResult.None) return Task.CompletedTask;

            object actualValue = null;
            if (valueResult.FirstValue != string.Empty)
            {
                try
                {
                    var enumerable = valueResult.FirstValue.Split(",").Select(t => int.Parse(t.Trim())).AsEnumerable();
                    if (bindingContext.ModelType == typeof(List<int>))
                        actualValue = enumerable.ToList();
                    else if (bindingContext.ModelType == typeof(int[]))
                        actualValue = enumerable.ToArray();
                    else
                        actualValue = enumerable;
                }
                catch (FormatException)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "You should provide a valid string value");
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(actualValue);
            return Task.CompletedTask;
        }
    }
}
