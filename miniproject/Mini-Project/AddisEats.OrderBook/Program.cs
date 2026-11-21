using AddisEats.OrderBook;

Console.WriteLine("=== Worked start: MenuItem + OrderLine + hard-coded order ===");

var tibs = new MenuItem(1, "Tibs", "Main", 250.00m);
var shiro = new MenuItem(2, "Shiro", "Main", 120.00m);
var coffee = new MenuItem(3, "Bunna", "Drink", 45.00m);

var starterOrder = new Order(
    id: 100,
    customer: "Mimi",
    area: "Bole",
    lines:
    [
        new OrderLine(tibs, 2),
        new OrderLine(coffee, 2)
    ]);

Console.WriteLine($"Order #{starterOrder.Id} for {starterOrder.Customer} ({starterOrder.Area})");
foreach (var line in starterOrder.Lines)
{
    Console.WriteLine($" - {line.Item.Name} x{line.Quantity} @ {line.Item.Price:0.00} ETB = {line.LineTotal:0.00} ETB");
}
Console.WriteLine($"Total: {starterOrder.Total:0.00} ETB");

Console.WriteLine();
Console.WriteLine("=== OrderBook demo ===");

var orderBook = new OrderBook();

var kitfo = new MenuItem(4, "Kitfo", "Main", 320.00m);
var juice = new MenuItem(5, "Mango Juice", "Drink", 70.00m);
var cake = new MenuItem(6, "Cake", "Dessert", 95.00m);

orderBook.AddOrder(new Order(1, "Abel", "Bole", [new OrderLine(tibs, 2), new OrderLine(coffee, 1)]));
orderBook.AddOrder(new Order(2, "Sara", "Kazanchis", [new OrderLine(kitfo, 1), new OrderLine(juice, 2)]));
orderBook.AddOrder(new Order(3, "Liya", "Bole", [new OrderLine(shiro, 3), new OrderLine(cake, 1)]));
orderBook.AddOrder(new Order(4, "Dawit", "Bole", [new OrderLine(kitfo, 2), new OrderLine(coffee, 2)]));
orderBook.AddOrder(new Order(5, "Ruth", "Piassa", [new OrderLine(tibs, 1), new OrderLine(juice, 1)]));
orderBook.AddOrder(new Order(6, "Nati", "Kazanchis", [new OrderLine(shiro, 2), new OrderLine(coffee, 2)]));
orderBook.AddOrder(new Order(7, "Hanna", "Bole", [new OrderLine(cake, 2), new OrderLine(juice, 1)]));
orderBook.AddOrder(new Order(8, "Yonatan", "CMC", [new OrderLine(kitfo, 1), new OrderLine(cake, 1)]));

Console.WriteLine("Orders for Bole by total descending:");
foreach (var order in orderBook.GetOrdersForAreaByTotalDescending("Bole"))
{
    Console.WriteLine($" - #{order.Id} {order.Customer}: {order.Total:0.00} ETB");
}

Console.WriteLine();
Console.WriteLine($"Total ETB revenue: {orderBook.GetTotalRevenueEtb():0.00} ETB");
Console.WriteLine($"Busiest category: {orderBook.GetBusiestCategory() ?? "No orders"}");

Console.WriteLine();
Console.WriteLine("Page 1 (5 per page):");
foreach (var order in orderBook.GetPage(1, 5))
{
    Console.WriteLine($" - #{order.Id} {order.Customer} ({order.Area}): {order.Total:0.00} ETB");
}

Console.WriteLine();
Console.WriteLine("Page 2 (5 per page):");
foreach (var order in orderBook.GetPage(2, 5))
{
    Console.WriteLine($" - #{order.Id} {order.Customer} ({order.Area}): {order.Total:0.00} ETB");
}

Console.WriteLine();
Console.WriteLine("Empty order book busiest category check:");
var emptyBook = new OrderBook();
Console.WriteLine($" - Result: {emptyBook.GetBusiestCategory() ?? "No orders"}");
