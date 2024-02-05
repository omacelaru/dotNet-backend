using System.ComponentModel.DataAnnotations;

namespace dotNet_backend.CustomActionFilters
{
    public class ValidateUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;
            if (string.IsNullOrWhiteSpace(username) || username.Length < 5)
            {
                return new ValidationResult("Username must be at least 5 characters long");
            }
            return ValidationResult.Success;
        }
    }
}
