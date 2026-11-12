using AddsEats.Api.Models;
using AddsEats.Api.Services.RestaurantServices;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(restaurants);
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
    }
}