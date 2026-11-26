namespace AddsEats.Api.DTOs;

/// <summary>
/// A page of results together with pagination metadata.
/// </summary>
/// <typeparam name="T">The type of the items in the page.</typeparam>
public class PaginatedResult<T>
{
    /// <summary>
    /// Items on the current page.
    /// </summary>
    public List<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// 1-based page number.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Alias of <see cref="Page"/> kept for backwards compatibility.
    /// </summary>
    public int CurrentPage { get => Page; set => Page = value; }

    /// <summary>
    /// Requested number of items per page.
    /// </summary>
    public int PageSize { get; set; } = 3;

    /// <summary>
    /// Total number of matching items across all pages.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages, calculated from <see cref="TotalCount"/> and <see cref="PageSize"/>.
    /// </summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}
