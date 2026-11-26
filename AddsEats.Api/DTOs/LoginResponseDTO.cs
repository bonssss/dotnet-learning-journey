namespace AddsEats.Api.DTOs;

/// <summary>
/// Access token returned by a successful login.
/// </summary>
public class LoginResponseDTO
{
    /// <summary>
    /// Signed JWT to send as <c>Authorization: Bearer &lt;token&gt;</c>.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Token type. Always <c>Bearer</c>.
    /// </summary>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// UTC time at which the token expires.
    /// </summary>
    public DateTime ExpiresAtUtc { get; set; }
}
