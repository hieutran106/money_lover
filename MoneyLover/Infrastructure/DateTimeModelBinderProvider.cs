﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Infrastructure
{
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder binder = new DateTimeModelBinder();
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
        }
    }
}
