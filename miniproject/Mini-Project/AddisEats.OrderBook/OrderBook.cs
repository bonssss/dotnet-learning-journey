namespace AddisEats.OrderBook;

public class OrderBook
{
    private readonly List<Order> _orders = [];

    public IReadOnlyList<Order> Orders => _orders;

    public void AddOrder(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        _orders.Add(order);
    }

    public IReadOnlyList<Order> GetOrdersForAreaByTotalDescending(string area)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(area);

        return _orders
            .Where(order => string.Equals(order.Area, area, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(order => order.Total)
            .ThenBy(order => order.Id)
            .ToList();
    }

    public decimal GetTotalRevenueEtb()
    {
        return _orders.Sum(order => order.Total);
    }

    public string? GetBusiestCategory()
    {
        return _orders
            .SelectMany(order => order.Lines)
            .GroupBy(line => line.Item.Category)
            .Select(group => new
            {
                Category = group.Key,
                TotalQuantity = group.Sum(line => line.Quantity)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .ThenBy(x => x.Category)
            .Select(x => x.Category)
            .FirstOrDefault();
    }

    public IReadOnlyList<Order> GetPage(int pageNumber, int pageSize = 5)
    {
        if (pageNumber <= 0)
            throw new ArgumentException("Page number must be positive.", nameof(pageNumber));
        if (pageSize <= 0)
            throw new ArgumentException("Page size must be positive.", nameof(pageSize));

        return _orders
            .OrderByDescending(order => order.Total)
            .ThenBy(order => order.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}
