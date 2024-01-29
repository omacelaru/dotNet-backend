using System.ComponentModel.DataAnnotations;

namespace dotNet_backend.CustomActionFilters;

public class ValidatePozitiveNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var number = value as int?;
        if (number < 0)
        {
            return new ValidationResult("Number must be pozitive");
        }
        return ValidationResult.Success;
    }
}