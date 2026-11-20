namespace AddisEats.Exercises.Day3Exercises;

public static class Exercise1
{
    public static decimal DeliveryFee(string area, bool isVip = false) => isVip
        ? 0m
        : area switch
        {
            "Bole" => 60m,
            "Kazanchis" => 40m,
            "Piassa" => 50m,
            "CMC" => 80m,
            _ => 70m
        };

    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 1: DeliveryFee ===");

        // Positional arguments
        decimal positional = DeliveryFee("Bole", false);
        Console.WriteLine($"Positional DeliveryFee(\"Bole\", false): {positional} ETB");

        // Named argument
        decimal named = DeliveryFee(area: "Kazanchis", isVip: true);
        Console.WriteLine($"Named DeliveryFee(area: \"Kazanchis\", isVip: true): {named} ETB");

        // Relying on the default (isVip defaults to false)
        decimal defaultVip = DeliveryFee("Piassa");
        Console.WriteLine($"Default DeliveryFee(\"Piassa\"): {defaultVip} ETB");
    }
}
