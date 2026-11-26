using AddsEats.Api.Models;
using AddsEats.Api.Services.MenuItemServices;
using Microsoft.AspNetCore.Mvc;

namespace AddsEats.Api.Controllers;

/// <summary>
/// Look up individual menu items across all restaurants.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemServices _menuItemServices;

    public MenuItemController(IMenuItemServices menuItemServices)
    {
        _menuItemServices = menuItemServices;
    }

    /// <summary>
    /// Returns every menu item in the catalog.
    /// </summary>
    /// <returns>All menu items, including VAT-inclusive prices.</returns>
    /// <response code="200">The catalog was returned.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<MenuItem>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var result = _menuItemServices.GetAll();
        return Ok(result);
    }

    /// <summary>
    /// Returns a single menu item by its id.
    /// </summary>
    /// <param name="id">The menu item id.</param>
    /// <returns>The requested menu item.</returns>
    /// <response code="200">The menu item was found.</response>
    /// <response code="404">No menu item exists with the given id.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(MenuItem), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var menuItem = _menuItemServices.GetById(id);
        if (menuItem == null)
        {
            return NotFound();
        }
        return Ok(menuItem);
    }
}
