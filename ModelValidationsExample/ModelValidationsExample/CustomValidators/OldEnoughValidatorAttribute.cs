using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.CustomValidators
{
    public class OldEnoughValidatorAttribute : ValidationAttribute
    {
        public DateTime MinimumBirthDate { get; set; } // Holds the minimum allowed birth date based on age

        public OldEnoughValidatorAttribute(int minAge)
        {
            MinimumBirthDate = DateTime.Now.AddYears(-minAge); // Corrected to -minAge for clarity
            ErrorMessage = $"Birth date must be higher than {MinimumBirthDate}";


        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                // Validate if the birth date is on or before the minimum allowed date
                return date <= MinimumBirthDate
                    ? ValidationResult.Success
                    : new ValidationResult(string.Format(ErrorMessage, MinimumBirthDate));
            }
            // Return a validation error or null depending on whether you want to enforce this field to be required
            return new ValidationResult("BirthDate is required."); // or return null if the field is not required
        }
    }
}
