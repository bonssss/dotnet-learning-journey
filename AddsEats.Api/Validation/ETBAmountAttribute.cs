namespace AddsEats.Api.Validation;

using System.ComponentModel.DataAnnotations;

public class ETBAmountAttribute : ValidationAttribute
{
    public ETBAmountAttribute()
    {
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        if (!double.TryParse(value.ToString(), out double amount))
        {
            return new ValidationResult("Amount must be a valid number.");
        }

        if (amount < 0)
        {
            return new ValidationResult("Amount must be positive.");
        }

        return ValidationResult.Success;
    }
}
