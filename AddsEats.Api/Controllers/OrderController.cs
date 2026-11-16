namespace AddsEats.Api.Controllers;

using AddsEats.Api.Models;
using AddsEats.Api.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using AddsEats.Api.DTOs;

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
        try
        {
            var order = orderService.CreateOrder(createOrderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
