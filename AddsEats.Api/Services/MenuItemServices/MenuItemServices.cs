using AddsEats.Api.Models;
using AddsEats.Api.Data;

namespace AddsEats.Api.Services.MenuItemServices;

public class MenuItemServices : IMenuItemServices
{
   private readonly List<MenuItem> menuItems = SeedData.menuItems;

   public MenuItem? GetById(int id)
   {
    return menuItems.FirstOrDefault(x => x.Id == id);
   }
   public List<MenuItem> GetAll()
   {
    return menuItems;
   }
}