using AddsEats.Api.Models;
using AddsEats.Api.Services.RestaurantServices;
using Microsoft.AspNetCore.Mvc;
using AddsEats.Api.DTOs;
using AddsEats.Api.Mappers;

namespace AddsEats.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        [HttpGet]
        public ActionResult<List<Restaurant>> GetRestaurants([FromQuery] string? address = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var restaurants = _restaurantServices.GetAll(address, page, pageSize);
            var restaurantsDTO = restaurants.Select(r => r.ToRestaurantDTO()).ToList();
            return Ok(restaurantsDTO);
        }
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurantById(int id)
        {
            var restaurant = _restaurantServices.GetById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        [HttpGet("{id}/menu")]
        public ActionResult<List<MenuItem>> GetRestaurantMenu(int id)
        {
            var menu = _restaurantServices.GetMenuByRestaurantId(id);
            if (menu == null)
            {
                return NotFound($"Restaurant with ID {id} was not found.");
            }

            return Ok(menu);
        }
    }
}