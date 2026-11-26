namespace AddsEats.Api.DTOs;

/// <summary>
/// Query parameters for listing restaurants.
/// </summary>
public class RestaurantQuery
{
    /// <summary>
    /// Optional address filter, e.g. Bole, Ayat, Mexico, or Megenagna.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 1-based page number. Defaults to 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of restaurants per page. Defaults to 10.
    /// </summary>
    public int PageSize { get; set; } = 10;
}
