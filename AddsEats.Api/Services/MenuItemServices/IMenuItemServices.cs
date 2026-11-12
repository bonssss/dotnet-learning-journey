using AddsEats.Api.Models;

namespace AddsEats.Api.Services.MenuItemServices;

public interface IMenuItemServices
{
     MenuItem? GetById(int id);
     List<MenuItem> GetAll();
}
