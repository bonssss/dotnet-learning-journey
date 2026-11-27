using AddsEats.Api.DTOs;
using AddsEats.Api.Mappers;
using AddsEats.Api.Models;
using AddsEats.Api.Services.RestaurantServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AddsEats.Api.Controllers
{
    /// <summary>
    /// Discover restaurants, filter them by address, and browse their menus.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;

        public RestaurantController(IRestaurantServices restaurantServices)
        {
            _restaurantServices = restaurantServices;
        }

        /// <summary>
        /// Returns a paginated list of restaurants, optionally filtered by address.
        /// </summary>
        /// <param name="restaurantQuery">Pagination (<c>page</c>, <c>pageSize</c>) and optional <c>address</c> filter.</param>
        /// <returns>A page of restaurant summaries with pagination metadata.</returns>
        /// <response code="200">The requested page was returned.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<RestaurantDTO>), StatusCodes.Status200OK)]
        public ActionResult<PaginatedResult<RestaurantDTO>> GetRestaurants([FromQuery] RestaurantQuery restaurantQuery)
        {
            var result = _restaurantServices.GetAll(restaurantQuery);
            return Ok(result);
        }

        /// <summary>
        /// Returns a single restaurant by its id.
        /// </summary>
        /// <param name="id">The restaurant id.</param>
        /// <returns>The restaurant summary.</returns>
        /// <response code="200">The restaurant was found.</response>
        /// <response code="404">No restaurant exists with the given id.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RestaurantDTO> GetRestaurantById(int id)
        {
            var restaurant = _restaurantServices.GetById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        /// <summary>
        /// Returns the menu items offered by a specific restaurant.
        /// </summary>
        /// <param name="id">The restaurant id.</param>
        /// <returns>The restaurant's menu, including VAT-inclusive prices.</returns>
        /// <response code="200">The menu was returned.</response>
        /// <response code="404">No restaurant exists with the given id.</response>
        [HttpGet("{id}/menu")]
        [ProducesResponseType(typeof(List<MenuItem>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<MenuItem>> GetRestaurantMenu(int id)
        {
            var menu = _restaurantServices.GetMenuByRestaurantId(id);
            if (menu == null)
            {
                return NotFound($"Restaurant with ID {id} was not found.");
            }

            return Ok(menu);
        }

        /// <summary>
        /// Returns all restaurants that are currently open for orders.
        /// </summary>
        /// <returns>A list of active restaurant summaries.</returns>
        /// <response code="200">The active restaurants were returned.</response>
        [HttpGet("active")]
        [ProducesResponseType(typeof(List<RestaurantDTO>), StatusCodes.Status200OK)]
        public ActionResult<List<RestaurantDTO>> GetActiveRestaurants()
        {
            var restaurantsDTO = _restaurantServices.GetAllActiveRestaurants();
            return Ok(restaurantsDTO);
        }
    }
}
