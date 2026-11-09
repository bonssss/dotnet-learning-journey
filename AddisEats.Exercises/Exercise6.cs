namespace AddisEats.Exercises;

public static class Exercise6
{
    public static void Run()
    {
        Console.WriteLine("=== Exercise 6: Value vs Reference Semantics ===");

        // Value Semantics: 'int' is a value type stored directly on the stack; assignment copies the actual value,
        // so modifying 'copiedCount' does not affect 'originalCount' because each variable holds its own independent data.
        int originalCount = 5;
        int copiedCount = originalCount;
        copiedCount = 12;

        Console.WriteLine($"originalCount: {originalCount}");
        Console.WriteLine($"copiedCount:   {copiedCount}");

        Console.WriteLine();

        // Reference Semantics: 'List<string>' is a reference type whose object data lives on the heap; assignment copies only the reference pointer,
        // so mutating 'copiedOrders' also affects 'originalOrders' because both variables point to the same underlying object on the heap.
        List<string> originalOrders = new() { "Kitfo", "Shiro" };
        List<string> copiedOrders = originalOrders;
        copiedOrders.Add("Tibs");

        Console.WriteLine($"originalOrders: [{string.Join(", ", originalOrders)}]");
        Console.WriteLine($"copiedOrders:   [{string.Join(", ", copiedOrders)}]");
    }
}
