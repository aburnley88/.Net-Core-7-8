
using ModelValidationsExample.Utility;

namespace ModelValidationsExample.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string PropertyParameter { get; set; } = string.Empty;
        public DateRangeValidatorAttribute(string propertyParam)
        {
            PropertyParameter = propertyParam;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                DateTime toDate = Convert.ToDateTime(value);
                DateTime fromDate  = ReflectionUtility.GetDateTimeValue(validationContext, PropertyParameter);
                if (fromDate > toDate)
                {
                    return new ValidationResult(ErrorMessage, new string[] { PropertyParameter, validationContext.MemberName });
                }
                else
                {
                    return ValidationResult.Success;
                }

            }
            return null;
        }
    }
}
