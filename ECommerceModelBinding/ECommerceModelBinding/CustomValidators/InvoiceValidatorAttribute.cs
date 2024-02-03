using System.ComponentModel.DataAnnotations;

namespace ECommerceModelBinding.CustomValidators
{
    public class InvoiceValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var order = validationContext.ObjectInstance as Order;
            if (order == null)
            {
                return new ValidationResult("Invalid object type.");
            }

            if (order.Products == null || !order.Products.Any())
            {
                return new ValidationResult("The order must contain at least one product.");
            }

            // Loop through each product in the order
            foreach (var product in order.Products)
            {
                // Validate each property of the product here
                var productProperties = product.GetType().GetProperties();
                foreach (var prop in productProperties)
                {
                    var propValue = prop.GetValue(product, null);
                    if (Convert.ToInt32(propValue) == 0)
                    {
                        return new ValidationResult($"{prop.Name} must be provided and greater than 0");
                    }

                    // Check if the type of the value matches the type of the property
                    if (propValue != null && prop.PropertyType != propValue.GetType())
                    {
                        // Return an error if the type doesn't match
                        return new ValidationResult($"{prop.Name} must be a valid number");
                    }
                }
            }

            // Calculate the expected invoice amount
            double expectedInvoicePrice = order.Products.Sum(product => product.Quantity * product.Price);

            // Check if the invoice price matches the expected amount
            if (order.InvoicePrice.HasValue && order.InvoicePrice.Value != expectedInvoicePrice)
            {
                return new ValidationResult($"The invoice price does not match the total price of the products. Expected invoice price: ${expectedInvoicePrice}");
            }
            return ValidationResult.Success;
        }
    }

}
