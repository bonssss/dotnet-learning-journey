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
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        if (!_phoneRegex.IsMatch(value.ToString()))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}