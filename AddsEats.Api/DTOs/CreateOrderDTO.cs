using System.ComponentModel.DataAnnotations;
using AddsEats.Api.Validation;
namespace AddsEats.Api.DTOs;

/// <summary>
/// Payload for placing an order. The total is computed by the server from the menu item price and quantity.
/// </summary>
/// <param name="RestaurantId">Id of an active restaurant.</param>
/// <param name="MenuItemId">Id of a menu item that belongs to that restaurant.</param>
/// <param name="CustomerName">Full name of the customer (2-100 characters).</param>
/// <param name="CustomerPhone">Valid Ethiopian mobile number.</param>
/// <param name="DeliveryAddress">Where the order should be delivered.</param>
/// <param name="Quantity">Number of portions, from 1 to 100.</param>
/// <param name="TotalAmount">Client-supplied hint; ignored by the server.</param>
public record CreateOrderDTO(
    [Range(1, int.MaxValue, ErrorMessage = "RestaurantId must be greater than 0")]
    int RestaurantId,

    [Range(1, int.MaxValue, ErrorMessage = "MenuItemId must be greater than 0")]
    int MenuItemId,

    [Required(ErrorMessage = "CustomerName is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "CustomerName must be between 2 and 100 characters")]
    string CustomerName,

    [Required(ErrorMessage = "CustomerPhone is required")]
    [EthiopianMobile(ErrorMessage = "CustomerPhone is not a valid phone number")]
    string CustomerPhone,

    [Required(ErrorMessage = "DeliveryAddress is required")]
    string DeliveryAddress,

    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
    int Quantity,

    decimal? TotalAmount = null
);
