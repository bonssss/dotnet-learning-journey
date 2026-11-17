using AddsEats.Api.Models;
using AddsEats.Api.DTOs;

namespace AddsEats.Api.Services.RestaurantServices;

public interface IRestaurantServices
{
    RestaurantDTO? GetById(int id); 
    PaginatedResult<RestaurantDTO> GetAll(RestaurantQuery restaurantQuery);
    List<MenuItem>? GetMenuByRestaurantId(int restaurantId);
    List<RestaurantDTO> GetAllActiveRestaurants();
}
