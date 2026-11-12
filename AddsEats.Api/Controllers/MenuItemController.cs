using Microsoft.AspNetCore.Mvc;
using AddsEats.Api.Models;
using AddsEats.Api.Services.MenuItemServices;
namespace AddsEats.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
 
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemServices _menuItemServices;
    public MenuItemController(IMenuItemServices menuItemServices)
    {
        _menuItemServices = menuItemServices;
    }
    [HttpGet()]
    public IActionResult Get()
    {
        var result = _menuItemServices.GetAll();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var menuItem = _menuItemServices.GetById(id);
        if(menuItem == null)
        {
            return NotFound();
        }
        return Ok(menuItem);
    }
}

