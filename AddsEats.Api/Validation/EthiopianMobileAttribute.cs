namespace AddsEats.Api.Validation;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class EthiopianMobileAttribute : ValidationAttribute
{
    private static readonly Regex _phoneRegex =
        new(@"^(?:\+251|0)(?:9|7)\d{8}$");

    public EthiopianMobileAttribute()
    {
        ErrorMessage = "Invalid Ethiopian phone number format.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        string stringValue = value.ToString()!;
        if (!_phoneRegex.IsMatch(stringValue))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
