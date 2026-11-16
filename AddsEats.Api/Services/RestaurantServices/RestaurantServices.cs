using AddsEats.Api.Models;
using AddsEats.Api.Data;
using AddsEats.Api.DTOs;

namespace AddsEats.Api.Services.RestaurantServices;

public class RestaurantServices : IRestaurantServices
{
    private readonly List<Restaurant> allRestaurants = SeedData.restaurants;
    public Restaurant? GetById(int id)
    {
        return allRestaurants.Where(x => x.Id == id).FirstOrDefault();
    }
    public List<Restaurant> GetAll(RestaurantQuery restaurantQuery)
    {
        var restaurants = allRestaurants
        .Where(r => restaurantQuery.Address == null || r.Address ==restaurantQuery.Address)
        .OrderBy(r=>r.Name)
        .Skip((restaurantQuery.Page -1) * restaurantQuery.PageSize)
        .Take(restaurantQuery.PageSize)
        .ToList();
        
        return restaurants;
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