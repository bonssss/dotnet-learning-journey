namespace AddisEats.Exercises.Day3Exercises;

public class Restaurant
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;

    public string DisplayName => $"{Name} ({Area})";
}

public static class Exercise3
{
    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 3: Restaurant class ===");

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Habesha Delight",
            Area = "Bole"
        };

        Console.WriteLine($"After init: Id={restaurant.Id}, Name={restaurant.Name}, Area={restaurant.Area}");
        Console.WriteLine($"DisplayName: {restaurant.DisplayName}");

        // Prove Name is settable
        restaurant.Name = "Habesha Deluxe";
        restaurant.Area = "Kazanchis";
        Console.WriteLine($"After changing Name/Area: DisplayName={restaurant.DisplayName}");

        // Prove Id is init-only (cannot be reassigned after initialization)
        // restaurant.Id = 2; // CS8852: Init-only property or indexer can only be assigned in an object initializer...
        Console.WriteLine("Id is init-only: attempting 'restaurant.Id = 2;' outside initialization would cause compiler error CS8852.");
    }
}
