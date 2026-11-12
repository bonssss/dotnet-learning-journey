using AddsEats.Api.Models;
using AddsEats.Api.DTOs;

namespace AddsEats.Api.Services.RestaurantServices;

public interface IRestaurantServices
{
    Restaurant? GetById(int id); 
    List <Restaurant> GetAll( string? address, int page,int pageSize);
  
   
}

    
