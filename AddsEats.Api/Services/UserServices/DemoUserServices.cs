using AddsEats.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace AddsEats.Api.Services.UserServices;

/// <summary>
/// In-memory demo user store used by the sample authentication flow.
/// </summary>
public class DemoUserServices
{
    private readonly List<User> _users = [];

    public DemoUserServices(IPasswordHasher<User> hasher)
    {
        var customer = new User
        {
            Id = 1,
            FullName = "bonsa",
            PhoneNumber = "0911111111",
            Role = "Customer"
        };
        customer.PasswordHash = hasher.HashPassword(customer, "123456");
        _users.Add(customer);
    }

    /// <summary>
    /// Finds a demo user by phone number.
    /// </summary>
    /// <param name="phoneNumber">Ethiopian phone number of the user.</param>
    /// <returns>The matching user, or <see langword="null"/> when not found.</returns>
    public Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return Task.FromResult(_users.SingleOrDefault(u => u.PhoneNumber == phoneNumber));
    }

    /// <summary>
    /// Alias for <see cref="GetByPhoneNumberAsync"/>.
    /// </summary>
    /// <param name="phoneNumber">Ethiopian phone number of the user.</param>
    /// <returns>The matching user, or <see langword="null"/> when not found.</returns>
    public Task<User?> FindByPhoneNumberAsync(string phoneNumber) => GetByPhoneNumberAsync(phoneNumber);
}
