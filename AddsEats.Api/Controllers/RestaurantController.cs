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
        public ActionResult<PaginatedResult<RestaurantDTO>> GetRestaurants([FromQuery] RestaurantQuery restaurantQuery)
        {
            var result = _restaurantServices.GetAll(restaurantQuery);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDTO> GetRestaurantById(int id)
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

        [HttpGet("active")]
        public ActionResult<List<RestaurantDTO>> GetActiveRestaurants()
        {
            var restaurantsDTO = _restaurantServices.GetAllActiveRestaurants();
            return Ok(restaurantsDTO);
        }
    }
}
