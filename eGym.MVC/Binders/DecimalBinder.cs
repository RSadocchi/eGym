using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace eGym.MVC.Binders
{
    public class DecimalBinder : IModelBinder
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
                    actualValue = decimal.Parse(valueResult.FirstValue.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "You should provide a valid decimal value");
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Success(actualValue);
            return Task.CompletedTask;
        }
    }
}
