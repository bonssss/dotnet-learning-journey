namespace AddisEats.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using AddisEats.Api.Models;


[ApiController]
[Route("api/[controller]")]

public class MenuController : ControllerBase
{
   private static List<MenuItem> menu = new List<MenuItem>
   {
       new MenuItem { Id = 1, Name = "Pizza", Price = 10.99m },
       new MenuItem { Id = 2, Name = "Burger", Price = 8.99m },
       new MenuItem { Id = 3, Name = "Pasta", Price = 12.99m }
   };

   [HttpGet]
   public IActionResult GetMenu() {
       return Ok(menu);
   }
    
}