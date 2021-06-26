using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace core.Extensions
{
    public static class ModelStateExtension
    {
        public static List<String> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
        }
    }
}
