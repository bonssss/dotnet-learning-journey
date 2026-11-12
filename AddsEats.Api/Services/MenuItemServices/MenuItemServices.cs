using AddsEats.Api.Models;

namespace AddsEats.Api.Services.MenuItemServices;

public class MenuItemServices : IMenuItemServices
{
   private readonly List<MenuItem> menuItems = new List<MenuItem>()
   {
    new MenuItem {Id = 1, Name = "Burger", Description = "Beef Burger", Price = 10.0m},
    new MenuItem {Id = 2, Name = "Pizza", Description = "Pepperoni Pizza", Price = 12.0m},
    new MenuItem {Id = 3, Name = "Pasta", Description = "Spaghetti Carbonara", Price = 11.0m},
    new MenuItem {Id = 4, Name = "Salad", Description = "Caesar Salad", Price = 8.0m},
    new MenuItem {Id = 5, Name = "Rice", Description = "Fried Rice", Price = 7.0m},
    new MenuItem {Id = 6, Name = "Noodles", Description = "Stir-fried Noodles", Price = 9.0m},
    new MenuItem {Id = 7, Name = "Soup", Description = "Tomato Soup", Price = 7.0m},
    new MenuItem {Id = 8, Name = "Sandwich", Description = "Turkey Sandwich", Price = 9.0m},
    new MenuItem {Id = 9, Name = "Wrap", Description = "Chicken Wrap", Price = 8.0m},
    new MenuItem {Id = 10, Name = "Sushi", Description = "Salmon Sushi", Price = 13.0m}
   };
   public MenuItem? GetById(int id)
   {
    return menuItems.FirstOrDefault(x => x.Id == id);
   }
   public List<MenuItem> GetAll()
   {
    return menuItems;
   }
}