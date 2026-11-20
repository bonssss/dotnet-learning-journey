namespace AddisEats.Exercises.Day3Exercises;

public record Customer(string Name, string PhoneNumber);

public class CustomerClass
{
    public CustomerClass(string name, string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public string Name { get; }
    public string PhoneNumber { get; }
}

public static class Exercise4
{
    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 4: Record vs Class equality ===");

        var record1 = new Customer("Abel", "0911000000");
        var record2 = new Customer("Abel", "0911000000");
        Console.WriteLine($"Record instances identical data: record1 == record2 => {record1 == record2}");
        Console.WriteLine($"record1.Equals(record2) => {record1.Equals(record2)}");

        // Converted to a class (same data shape), repeated comparison
        var class1 = new CustomerClass("Abel", "0911000000");
        var class2 = new CustomerClass("Abel", "0911000000");
        Console.WriteLine($"Class instances identical data: class1 == class2 => {class1 == class2}");
        Console.WriteLine($"class1.Equals(class2) => {class1.Equals(class2)}");

        Console.WriteLine();
        Console.WriteLine("Difference: records use value-based equality (compare data), while classes use reference equality by default (compare object identity).");
    }
}
