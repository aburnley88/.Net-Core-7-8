using System.ComponentModel.DataAnnotations;

namespace ECommerceModelBinding.CustomValidators
{
    public class OrderDateValidatorAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;

        public OrderDateValidatorAttribute(int year =2000, int month=1, int day=1)
        {
            _minDate = new DateTime(year, month, day);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                return Convert.ToDateTime(value) < _minDate ? new ValidationResult(ErrorMessage = $"OrderDate must be greater than {_minDate}") :
                        ValidationResult.Success;
            }
            if(value == null)
            {
                return new ValidationResult(ErrorMessage = "OrderDate is required");
            }

            return new ValidationResult(ErrorMessage = "Invalid format for OrderDate");

        }
    }
}
