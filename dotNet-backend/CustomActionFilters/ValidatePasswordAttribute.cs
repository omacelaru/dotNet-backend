using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace dotNet_backend.CustomActionFilters
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //>8 characters, one uppercase, one lowercase, one number, one special character
            var password = value as string;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return new ValidationResult("Password must be at least 8 characters long");
            }

            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasNumber = false;
            bool hasSpecialChar = false;
            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpperCase = true;
                if (char.IsLower(c)) hasLowerCase = true;
                if (char.IsDigit(c)) hasNumber = true;
                if (!char.IsLetterOrDigit(c)) hasSpecialChar = true;
            }

            if (!hasUpperCase || !hasLowerCase || !hasNumber || !hasSpecialChar)
            {
                return new ValidationResult(
                    "Password must contain at least one uppercase, one lowercase, one number and one special character");
            }

            return ValidationResult.Success;
        }
    }
}