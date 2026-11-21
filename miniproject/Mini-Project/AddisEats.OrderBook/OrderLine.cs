namespace AddisEats.OrderBook;

public record OrderLine
{
    public OrderLine(MenuItem item, int quantity)
    {
        ArgumentNullException.ThrowIfNull(item);
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.", nameof(quantity));

        Item = item;
        Quantity = quantity;
    }

    public MenuItem Item { get; }
    public int Quantity { get; }

    public decimal LineTotal => Item.Price * Quantity;
}
