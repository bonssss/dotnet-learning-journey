namespace AddsEats.Api.Services.OrderServices;

using AddsEats.Api.DTOs;
using AddsEats.Api.Models;
using AddsEats.Api.Common;

public interface IOrderServices
{
    Result<Order> CreateOrder(CreateOrderDTO orderDto);
    Order? GetOrderById(int id);
}
