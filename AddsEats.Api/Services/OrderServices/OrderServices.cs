namespace AddsEats.Api.Services.OrderServices;

using AddsEats.Api.Models;
using AddsEats.Api.DTOs;
using AddsEats.Api.Data;
using AddsEats.Api.Common;
using AddsEats.Api.Services.RestaurantServices;

public class OrderServices(IRestaurantServices restaurantServices) : IOrderServices
{
    private static readonly List<Order> _orders = new List<Order>();
    private static int _nextId = 1;
    private static readonly object _lock = new();

    public Order? GetOrderById(int id)
    {
        lock (_lock)
        {
            return _orders.FirstOrDefault(x => x.Id == id);
        }
    }

    public Result<Order> CreateOrder(CreateOrderDTO newOrder)
    {
        // 1. Verify restaurant exists from Restaurant DTO
        var restaurant = restaurantServices.GetById(newOrder.RestaurantId);
        if (restaurant == null)
        {
            return Result<Order>.Failure(
                "NotFound",
                $"Restaurant with ID {newOrder.RestaurantId} does not exist.",
                404);
        }

        // 2. Verify restaurant is active
        if (!restaurant.IsActive)
        {
            return Result<Order>.Failure(
                "InactiveRestaurant",
                $"Restaurant '{restaurant.Name}' is currently inactive and cannot accept orders.",
                400);
        }

        // 3. Verify menu item exists
        var menuItem = SeedData.menuItems.FirstOrDefault(m => m.Id == newOrder.MenuItemId);
        if (menuItem == null)
        {
            return Result<Order>.Failure(
                "NotFound",
                $"Menu item with ID {newOrder.MenuItemId} does not exist.",
                404);
        }

        // 4. Verify menu item belongs to the restaurant
        if (menuItem.RestaurantId != newOrder.RestaurantId)
        {
            return Result<Order>.Failure(
                "InvalidMenuItem",
                $"Menu item '{menuItem.Name}' (ID {menuItem.Id}) does not belong to restaurant '{restaurant.Name}' (ID {restaurant.Id}).",
                400);
        }

        // 5. Server-side total calculation (prevents client price tampering)
        decimal calculatedTotal = menuItem.Price * newOrder.Quantity;

        var order = new Order
        {
            RestaurantId = newOrder.RestaurantId,
            MenuItemId = newOrder.MenuItemId,
            CustomerName = newOrder.CustomerName.Trim(),
            CustomerPhone = newOrder.CustomerPhone.Trim(),
            DeliveryAddress = newOrder.DeliveryAddress.Trim(),
            Quantity = newOrder.Quantity,
            TotalAmount = calculatedTotal,
            OrderDate = DateTime.UtcNow
        };

        lock (_lock)
        {
            order.Id = _nextId++;
            _orders.Add(order);
        }

        return Result<Order>.Success(order);
    }
}
