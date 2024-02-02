using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.CustomModelBinding
{
    public class PersonModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Checks if the object being used for model biding is a person.
        /// If it is model binding is using the custom model binder.
        /// If not it follows standard model binding protocol
        /// </summary>
        /// <param name="context"></param>
        /// <returns>A PersonModelBinder object if the model is a person and null if not</returns>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            
            return context.Metadata.ModelType == typeof(Person) ? 
                new BinderTypeModelBinder(typeof(PersonModelBinder)) : null;
        }
    }
}
