using AddsEats.Api.DTOs;
using AddsEats.Api.Models;

namespace AddsEats.Api.Mappers;

public static class RestaurantMapper
{
    public static RestaurantDTO ToRestaurantDTO(this Restaurant restaurant)
    {
        return new RestaurantDTO
        (
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Phone,
            restaurant.IsActive
        );
    }
}