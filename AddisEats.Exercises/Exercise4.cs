namespace AddisEats.Exercises;

public static class Exercise4
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 4: Receipt Formatting ===");

        decimal subtotal = 1480.5m;
        decimal deliveryFee = 60m;
        decimal total = subtotal + deliveryFee;

        Console.WriteLine($"Subtotal:     {subtotal,10:N2}");
        Console.WriteLine($"Delivery Fee: {deliveryFee,10:N2}");
        Console.WriteLine($"Total:        {total,10:N2}");
    }
}
