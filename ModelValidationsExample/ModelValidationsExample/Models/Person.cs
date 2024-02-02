using ModelValidationsExample.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} Cannot be empty")]
        [Display(Name = "Person Name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "{0} Must be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z .]*$")]
        public string PersonName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "{0} Should be a valid email address")]
        public string Email { get; set; } = string.Empty;
        [Phone(ErrorMessage = "{0} should be a valid phone number")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "{0} and {1} must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Range(0, 999.99, ErrorMessage ="{0} Must be between ${1} and ${2} digits")]
        public double Price { get; set; }

        [OldEnoughValidator(18, ErrorMessage ="Birthday must be beyond {0}")]
        [Display(Name = "Date of birth")]
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return $"Person Details \nName: {PersonName} \nEmail: {Email}\nPhone: {Phone}\nPrice: {Price}";
        }
    }
}
