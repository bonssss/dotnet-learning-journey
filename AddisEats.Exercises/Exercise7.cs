namespace AddisEats.Exercises;

public static class Exercise7
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 7: Safe Null Handling for String Length ===");

        // Case 1: String is null
        string? deliveryNote = null;
        Console.WriteLine("Testing with deliveryNote = null:");
        PrintLengths(deliveryNote);

        Console.WriteLine();

        // Case 2: String has a value
        string? activeNote = "Leave food at gate 3";
        Console.WriteLine($"Testing with activeNote = \"{activeNote}\":");
        PrintLengths(activeNote);
    }

    private static void PrintLengths(string? input)
    {
        // 1. Explicit null check
        int length1;
        if (input != null)
        {
            length1 = input.Length;
        }
        else
        {
            length1 = 0;
        }
        Console.WriteLine($"  1. Explicit null check : length = {length1}");

        // 2. Null-coalescing operator (??)
        int length2 = (input ?? string.Empty).Length;
        Console.WriteLine($"  2. Null-coalescing (??): length = {length2}");

        // 3. Null-conditional operator (?.)
        int? length3 = input?.Length;
        Console.WriteLine($"  3. Null-conditional (?): length = {length3 ?? 0}");
    }
}

