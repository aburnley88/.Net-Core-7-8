
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;

namespace ModelValidationsExample.CustomModelBinding
{
  

    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelType = bindingContext.ModelType;
            var modelInstance = Activator.CreateInstance(modelType);

            foreach (PropertyInfo property in modelType.GetProperties())
            {
                // Check if the property is settable
                if (!property.CanWrite) continue;

                // Get the value from the value provider
                var valueProviderResult = bindingContext.ValueProvider.GetValue(property.Name);
                if (valueProviderResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueProviderResult.FirstValue))
                {
                    try
                    {
                        // Convert the value to the property type and set the property
                        var value = Convert.ChangeType(valueProviderResult.FirstValue, property.PropertyType, CultureInfo.InvariantCulture);
                        property.SetValue(modelInstance, value);
                    }
                    catch (Exception)
                    {
                        // Handle conversion errors, add model state errors if necessary
                        bindingContext.ModelState.TryAddModelError(property.Name, $"Invalid value for {property.Name}");
                    }
                }
            }

            // Calculate and set FullName after other properties are bound
            if (modelType.GetProperty("FirstName") != null &&
                modelType.GetProperty("LastName") != null)
            {
                string fullName = CreateFullName(bindingContext);
                if (!string.IsNullOrEmpty(fullName))
                {
                    modelType.GetProperty("FullName")?.SetValue(modelInstance, fullName);
                }
            }

            bindingContext.Result = ModelBindingResult.Success(modelInstance);
            return Task.CompletedTask;
        }


        public static string CreateFullName(ModelBindingContext modelBindingContext)
        {
            var firstNameResult = modelBindingContext.ValueProvider.GetValue("FirstName");
            var lastNameResult = modelBindingContext.ValueProvider.GetValue("LastName");

            if (firstNameResult != ValueProviderResult.None && lastNameResult != ValueProviderResult.None)
            {
                var firstName = firstNameResult.FirstValue?.Trim();
                var lastName = lastNameResult.FirstValue?.Trim();

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                {
                    return firstName + " " + lastName;
                }
            }

            return string.Empty;
        }

    }

}
