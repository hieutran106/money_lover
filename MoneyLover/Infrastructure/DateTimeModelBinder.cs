using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Infrastructure
{
    public class DateTimeModelBinder : IModelBinder
    {
        private readonly IModelBinder baseBinder = new SimpleTypeModelBinder(typeof(DateTime));
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Try to fetch the value of the argument by name
            string modelName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult!=ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                string valueAsString = valueProviderResult.FirstValue;
                //  valueAsString will have a string value of your date, e.g. '31-12-2017'
                //  Parse it as you need and build DateTime object
                DateTime dateTime = DateTime.ParseExact(valueAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                bindingContext.Result = ModelBindingResult.Success(dateTime);

                return Task.CompletedTask;
            }
            return baseBinder.BindModelAsync(bindingContext);
        }
    }
}
