namespace AddsEats.Api.Controllers;

using AddsEats.Api.Models;
using AddsEats.Api.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using AddsEats.Api.DTOs;
using AddsEats.Api.Common;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderServices orderService) : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<Order> GetOrderById(int id)
    {
        var order = orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound(new { message = $"Order with ID {id} was not found." });
        }
        return Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> CreateOrder([FromBody] CreateOrderDTO createOrderDto)
    {
        var result = orderService.CreateOrder(createOrderDto);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Error!.StatusCode, result.Error);
        }

        var order = result.Value!;
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }
}
