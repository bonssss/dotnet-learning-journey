using AddsEats.Api.Models;
using AddsEats.Api.Data;
namespace AddsEats.Api.Services.RestaurantServices;

public class RestaurantServices : IRestaurantServices
{
    private readonly List<Restaurant> allRestaurants = SeedData.restaurants;
    public Restaurant? GetById(int id)
    {
        return allRestaurants.Where(x => x.Id == id).FirstOrDefault();
    }
    public List<Restaurant> GetAll(string? address, int page, int pageSize)
    {
        var restaurants = allRestaurants
        .Where(r => address == null || r.Address ==address)
        .OrderBy(r=>r.Name)
        .Skip((page -1) * pageSize)
        .Take(pageSize)
        .ToList();
        
        return restaurants;
    }
   
}