using System.ComponentModel.DataAnnotations;
using AddsEats.Api.Validation;

namespace AddsEats.Api.DTOs;

/// <summary>
/// Credentials for the demo login endpoint.
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// Ethiopian phone number of the demo user, e.g. 0911111111.
    /// </summary>
    [Required]
    [EthiopianMobile(ErrorMessage = "PhoneNumber is not a valid Ethiopian mobile number")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Password of the demo user.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}
