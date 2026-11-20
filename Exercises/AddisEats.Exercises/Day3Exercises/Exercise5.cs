namespace AddisEats.Catalog;

public static class Exercise5
{
    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 5: LINQ chains over orders ===");

        var baseTime = new DateTime(2026, 11, 20, 12, 0, 0);
        var orders = new List<Order>();
        var areas = new[] { "Bole", "Kazanchis", "Piassa", "CMC" };
        var names = new[] { "Mimi", "Abel", "Sara", "Liya", "Yonatan", "Kebede", "Hanna", "Dawit", "Ruth", "Nati" };

        for (int i = 1; i <= 30; i++)
        {
            orders.Add(new Order
            {
                Id = i,
                CustomerName = names[(i - 1) % names.Length],
                Area = areas[(i - 1) % areas.Length],
                AmountEtb = 100m + (i * 45m),
                CreatedAt = baseTime.AddMinutes(i)
            });
        }

        // 1) Orders in Bole over 500 ETB
        var boleOver500 = orders
            .Where(o => o.Area == "Bole" && o.AmountEtb > 500m)
            .ToList();

        Console.WriteLine("1) Orders in Bole over 500 ETB:");
        foreach (var o in boleOver500)
            Console.WriteLine($"   #{o.Id} {o.CustomerName} {o.Area} {o.AmountEtb} ETB {o.CreatedAt:HH:mm}");

        // 2) Every customer name sorted alphabetically
        var customerNamesSorted = orders
            .Select(o => o.CustomerName)
            .Distinct()
            .OrderBy(name => name)
            .ToList();

        Console.WriteLine("2) Every customer name sorted alphabetically:");
        Console.WriteLine("   " + string.Join(", ", customerNamesSorted));

        // 3) Total ETB revenue for Kazanchis
        var kazanchisRevenue = orders
            .Where(o => o.Area == "Kazanchis")
            .Sum(o => o.AmountEtb);

        Console.WriteLine($"3) Total ETB revenue for Kazanchis: {kazanchisRevenue} ETB");

        // 4) Page 3 at 10 per page, newest first
        var page3NewestFirst = orders
            .OrderByDescending(o => o.CreatedAt)
            .Skip(20)
            .Take(10)
            .ToList();

        Console.WriteLine("4) Page 3 at 10 per page, newest first:");
        foreach (var o in page3NewestFirst)
            Console.WriteLine($"   #{o.Id} {o.CustomerName} {o.Area} {o.AmountEtb} ETB {o.CreatedAt:HH:mm}");
    }
}
