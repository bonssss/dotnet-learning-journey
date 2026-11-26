namespace AddsEats.Api.DTOs;

/// <summary>
/// Public representation of a restaurant. Internal fields such as the owner's address are excluded.
/// </summary>
/// <param name="Id">Unique restaurant id.</param>
/// <param name="Name">Restaurant name.</param>
/// <param name="Address">Neighborhood or address in Addis Ababa.</param>
/// <param name="Phone">Contact phone number.</param>
/// <param name="IsActive">Whether the restaurant currently accepts orders.</param>
public record RestaurantDTO
(
    int Id,
    string Name,
    string Address,
    string Phone,
    bool IsActive
);
