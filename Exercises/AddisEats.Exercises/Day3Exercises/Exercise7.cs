namespace AddisEats.Exercises.Day3Exercises;

public static class Exercise7
{
    public static void ValidateEtbAmount(decimal amount)
    {
        if (amount <= 0m)
            throw new ArgumentException("ETB amount must be positive.", nameof(amount));

        Console.WriteLine($"  Valid amount accepted: {amount} ETB");
    }

    private static void CallWithHandling(decimal amount)
    {
        Console.WriteLine($"Calling ValidateEtbAmount({amount})");
        try
        {
            ValidateEtbAmount(amount);
            Console.WriteLine("  try: completed without exception");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"  catch: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("  finally: this block always runs");
        }

        Console.WriteLine();
    }

    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 7: Exception handling with finally ===");
        CallWithHandling(250m);   // does not throw
        CallWithHandling(0m);     // throws
        CallWithHandling(-50m);   // throws
    }
}
