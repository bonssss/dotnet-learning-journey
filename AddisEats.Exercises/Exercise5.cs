namespace AddisEats.Exercises;

public static class Exercise5
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 5: Integer vs Decimal Division ===");

        // Integer division truncates the fractional portion (.5 is discarded)
        int intDivision = 7 / 2;
        Console.WriteLine($"Integer division (7 / 2): {intDivision}");

        // Division cast to decimal preserves the fractional portion
        decimal decimalDivision = (decimal)7 / 2;
        Console.WriteLine($"Decimal division ((decimal)7 / 2): {decimalDivision}");

        // I would use decimal division for splitting a delivery fee three ways so fractional amounts are not lost to truncation.
    }
}
