namespace AddisEats.OrderBook;

public class Order
{
    public Order(int id, string customer, string area, IEnumerable<OrderLine> lines)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(customer);
        ArgumentException.ThrowIfNullOrWhiteSpace(area);
        ArgumentNullException.ThrowIfNull(lines);

        Id = id;
        Customer = customer;
        Area = area;
        Lines = lines.ToList();
    }

    public int Id { get; }
    public string Customer { get; }
    public string Area { get; }
    public IReadOnlyList<OrderLine> Lines { get; }

    public decimal Total => Lines.Sum(line => line.LineTotal);
}
