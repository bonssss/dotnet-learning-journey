using Microsoft.AspNetCore.Mvc;
using AddsEats.Api.Models;

namespace AddsEats.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
 
public class MenuItemController : ControllerBase
{
    List<MenuItem> menuItems = new List<MenuItem>
    {
        new MenuItem {Id = 1, Name = "Burger", Description = "Juicy beef burger", Price = 10.0m},
        new MenuItem {Id = 2, Name = "Pizza", Description = "Classic Margherita", Price = 12.0m},
        new MenuItem {Id = 3, Name = "Pasta", Description = "Creamy Carbonara", Price = 11.0m},
        new MenuItem {Id = 4, Name = "Salad", Description = "Fresh Caesar salad", Price = 9.0m},
        new MenuItem {Id = 5, Name = "Steak", Description = "Grilled Ribeye steak", Price = 15.0m},
        new MenuItem {Id = 6, Name = "Fish", Description = "Baked Salmon", Price = 14.0m},
        new MenuItem {Id = 7, Name = "Chicken", Description = "Grilled Chicken", Price = 13.0m},
        new MenuItem {Id = 8, Name = "Rice", Description = "Fried Rice", Price = 8.0m},
        new MenuItem {Id = 9, Name = "Noodles", Description = "Stir-fried Noodles", Price = 9.0m},
        new MenuItem {Id = 10, Name = "Soup", Description = "Tomato Soup", Price = 7.0m}
    };
    [HttpGet()]
   public IActionResult Get()
   {
    return Ok(menuItems);
   }
  [HttpGet("{id}")]
  public IActionResult GetById(int id)
  {
    var menuItem = menuItems.Find(x => x.Id == id);
if(menuItem == null)
{
    return NotFound();
}
    return Ok(menuItem);
  }

}
