using AddsEats.Api.Configurarions;
using AddsEats.Api.DTOs;
using AddsEats.Api.Models;
using AddsEats.Api.Services;
using AddsEats.Api.Services.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AddsEats.Api.Controllers;

/// <summary>
/// Authentication endpoints for the demo user store.
/// </summary>
[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly DemoUserServices _demoUserServices;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthService _authService;
    private readonly JwtOptions _jwtOptions;

    public AuthController(
        DemoUserServices demoUserServices,
        IPasswordHasher<User> passwordHasher,
        AuthService authService,
        IOptions<JwtOptions> jwtOptions)
    {
        _demoUserServices = demoUserServices;
        _passwordHasher = passwordHasher;
        _authService = authService;
        _jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// Logs in a demo user and returns a signed JWT access token.
    /// </summary>
    /// <param name="request">Phone number and password of the demo user.</param>
    /// <returns>A bearer token with its expiry time.</returns>
    /// <response code="200">Authentication succeeded.</response>
    /// <response code="400">The request is invalid or the credentials are wrong.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Invalid request" });
        }

        var user = await _demoUserServices.GetByPhoneNumberAsync(request.PhoneNumber);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid credentials" });
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return BadRequest(new { message = "Invalid credentials" });
        }

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        }

        var token = _authService.CreateAccessToken(user);
        return Ok(new LoginResponseDTO
        {
            AccessToken = token,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes)
        });
    }
}
