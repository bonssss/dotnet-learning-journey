namespace AddsEats.Api.DTOs;

public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int Page { get; set; } = 1;
    public int CurrentPage { get => Page; set => Page = value; }
    public int PageSize { get; set; } = 3;
    public int TotalCount { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}