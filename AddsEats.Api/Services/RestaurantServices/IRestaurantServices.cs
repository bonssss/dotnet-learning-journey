using AddsEats.Api.Models;
using AddsEats.Api.DTOs;


namespace AddsEats.Api.Services.RestaurantServices;

public interface IRestaurantServices
{
    Restaurant? GetById(int id); 
    PaginatedResult<RestaurantDTO> GetAll(RestaurantQuery restaurantQuery);
    List<MenuItem>? GetMenuByRestaurantId(int restaurantId);
    List<Restaurant> GetAllActiveRestaurants();
}

    
