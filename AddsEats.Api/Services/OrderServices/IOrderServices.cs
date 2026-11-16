namespace AddsEats.Api.Services.OrderServices;
using AddsEats.Api.DTOs;
using AddsEats.Api.Models;

public interface IOrderServices
{
    Order CreateOrder(CreateOrderDTO orderDto);

    // Task<IEnumerable<Order>> GetAllOrdersAsync();
    Order? GetOrderById(int id);
    // Task<Order?> UpdateOrderAsync(int id, CreateOrderDTO orderDto);
    // Task<Order?> DeleteOrderAsync(int id);
}