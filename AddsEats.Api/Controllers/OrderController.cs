namespace AddsEats.Api.Controllers;

using AddsEats.Api.Common;
using AddsEats.Api.DTOs;
using AddsEats.Api.Models;
using AddsEats.Api.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

/// <summary>
/// Place orders and retrieve their details. Totals are always computed on the server.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrderController(IOrderServices orderService) : ControllerBase
{
    /// <summary>
    /// Returns a previously placed order by its id.
    /// </summary>
    /// <param name="id">The order id.</param>
    /// <returns>The stored order.</returns>
    /// <response code="200">The order was found.</response>
    /// <response code="404">No order exists with the given id.</response>
    // [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Order> GetOrderById(int id)
    {
        var order = orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound(new { message = $"Order with ID {id} was not found." });
        }
        return Ok(order);
    }

    /// <summary>
    /// Places a new order after validating the restaurant, menu item, and quantity.
    /// </summary>
    /// <param name="createOrderDto">Order details; <c>TotalAmount</c> is ignored and recomputed server-side.</param>
    /// <returns>The created order with its server-calculated total.</returns>
    /// <response code="201">The order was created.</response>
    /// <response code="400">Validation failed, e.g. inactive restaurant or mismatched menu item.</response>
    /// <response code="404">The restaurant or menu item does not exist.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
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
