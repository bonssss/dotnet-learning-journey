namespace AddsEats.Api.Models;

/// <summary>
/// A dish offered by a restaurant. Prices include the 15% VAT used in Ethiopia.
/// </summary>
public class MenuItem
{
    /// <summary>
    /// Unique menu item id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id of the restaurant that owns this item.
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Dish name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Short description of the dish.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Base price in ETB, excluding VAT.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Category, e.g. main course or drink.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Price with VAT included, calculated as <c>Price * 1.15</c>.
    /// </summary>
    public decimal PriceWithVat => Price * 1.15m;
}
