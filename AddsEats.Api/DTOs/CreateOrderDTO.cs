using System.ComponentModel.DataAnnotations;

namespace AddsEats.Api.DTOs;

public record CreateOrderDTO(
    [Range(1, int.MaxValue, ErrorMessage = "RestaurantId must be greater than 0")]
    int RestaurantId,

    [Range(1, int.MaxValue, ErrorMessage = "MenuItemId must be greater than 0")]
    int MenuItemId,

    [Required(ErrorMessage = "CustomerName is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "CustomerName must be between 2 and 100 characters")]
    string CustomerName,

    [Required(ErrorMessage = "CustomerPhone is required")]
    [Phone(ErrorMessage = "CustomerPhone is not a valid phone number")]
    string CustomerPhone,

    [Required(ErrorMessage = "DeliveryAddress is required")]
    string DeliveryAddress,

    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
    int Quantity,

    decimal? TotalAmount = null
);
