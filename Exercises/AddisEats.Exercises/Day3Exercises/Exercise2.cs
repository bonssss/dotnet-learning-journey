namespace AddisEats.Exercises.Day3Exercises;

public static class Exercise2
{
    public static void Describe(int value)
    {
        Console.WriteLine($"Describe(int) was chosen: {value}");
    }

    public static void Describe(string value)
    {
        Console.WriteLine($"Describe(string) was chosen: {value}");
    }

    public static void Describe(decimal value)
    {
        Console.WriteLine($"Describe(decimal) was chosen: {value}");
    }

    // Fourth overload differing only by return type (does not compile):
    // public static string Describe(int value) => value.ToString();
    // Compiler error (CS0111): Type 'Exercise2' already defines a member called 'Describe' with the same parameter types

    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 2: Method Overloading (Describe) ===");

        Describe(42);
        Describe("Bole");
        Describe(199.99m);

        Console.WriteLine();
        Console.WriteLine("Attempted fourth overload differing only by return type:");
        Console.WriteLine("  public static string Describe(int value) => value.ToString();");
        Console.WriteLine("Compiler error: CS0111 - Type 'Exercise2' already defines a member called 'Describe' with the same parameter types");
    }
}
