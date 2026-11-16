using AddsEats.Api.Models;
using AddsEats.Api.Data;
using AddsEats.Api.DTOs;
using AddsEats.Api.Mappers;

namespace AddsEats.Api.Services.RestaurantServices;

public class RestaurantServices : IRestaurantServices
{
    private readonly List<Restaurant> allRestaurants = SeedData.restaurants;
    public Restaurant? GetById(int id)
    {
        return allRestaurants.Where(x => x.Id == id).FirstOrDefault();
    }

    
    public PaginatedResult<RestaurantDTO> GetAll(RestaurantQuery restaurantQuery)
    {
        var restaurants = allRestaurants
        .Where(r => restaurantQuery.Address == null || r.Address ==restaurantQuery.Address)
        .OrderBy(r=>r.Name)
        
        .ToList();

        var totalCount= restaurants.Count();

        var toReturn = restaurants
        .Skip((restaurantQuery.Page -1) * restaurantQuery.PageSize)
        .Take(restaurantQuery.PageSize)
        .ToList();

        PaginatedResult<RestaurantDTO> resultDTO = new PaginatedResult<RestaurantDTO>
        {
            Items = toReturn.Select(r => r.ToRestaurantDTO()).ToList(),
            TotalCount = totalCount,
            CurrentPage = restaurantQuery.Page,
            PageSize = restaurantQuery.PageSize
        };
        
        return resultDTO;
    }


    public List<MenuItem>? GetMenuByRestaurantId(int restaurantId)
    {
        var restaurant = GetById(restaurantId);
        if (restaurant == null)
        {
            return null;
        }

        return SeedData.menuItems
            .Where(m => m.RestaurantId == restaurantId)
            .ToList();
    }

    public List<Restaurant> GetAllActiveRestaurants()
    {
        var restaurants = allRestaurants
        .Where(r => r.IsActive == true)
        .OrderBy(r=>r.Name)
        .ToList();
        return restaurants;
    }
}