namespace AddisEats.Catalog;

public class Order
{
    public int Id { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string Area { get; init; } = string.Empty;
    public decimal AmountEtb { get; init; }
    public DateTime CreatedAt { get; init; }
}
