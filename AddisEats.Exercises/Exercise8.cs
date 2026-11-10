using System.Globalization;

namespace AddisEats.Exercises;

public static class Exercise8
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 8: Safe Parsing with TryParse ===");

        // Requirement: Test it with the input "four hundred" and confirm it does not crash
        Console.WriteLine("Demonstrating required test cases:");
        Console.WriteLine("1. Testing non-numeric string \"four hundred\":");
        ProcessAmount("four hundred");

        Console.WriteLine("2. Testing negative number \"-50\":");
        ProcessAmount("-50");

        Console.WriteLine("3. Testing zero \"0\":");
        ProcessAmount("0");

        Console.WriteLine("4. Testing valid positive number \"1480.5\":");
        ProcessAmount("1480.5");

        Console.WriteLine();
        Console.WriteLine("Interactive prompt:");
        Console.Write("Enter an ETB amount: ");
        string? userInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(userInput))
        {
            ProcessAmount(userInput);
        }
    }

    public static void ProcessAmount(string? input)
    {
        // Parse with TryParse to safely handle non-numeric inputs without crashing
        bool isValidNumber = decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal amount)
                             || decimal.TryParse(input, out amount);

        if (!isValidNumber)
        {
            Console.WriteLine($"  -> Rejected: \"{input}\" is not a valid number.");
        }
        else if (amount <= 0m)
        {
            Console.WriteLine($"  -> Rejected: Amount must be greater than zero. Received: {amount:N2}");
        }
        else
        {
            Console.WriteLine($"  -> Accepted: {amount:N2} ETB");
        }
    }
}

