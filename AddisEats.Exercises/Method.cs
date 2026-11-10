namespace AddisEats.Exercises;

public static class Method
{
    // Basic method
    public static void Add(decimal a, decimal b)
    {
        decimal result = a + b;
        Console.WriteLine($"The sum of {a} and {b} is {result}");
    }

    // Method Overloading: 3 versions of PlusMethod
    // 1. Two integers
    public static int PlusMethod(int a, int b)
    {
        return a + b;
    }

    // 2. Two doubles
    public static double PlusMethod(double a, double b)
    {
        return a + b;
    }

    // 3. Three integers
    public static int PlusMethod(int a, int b, int c)
    {
        return a + b + c;
    }

    public static void Run()
    {
        Console.WriteLine("--- Basic Method ---");
        Add(5.5m, 3.2m);

        Console.WriteLine("\n--- Method Overloading (PlusMethod) ---");
        int intResult = PlusMethod(8, 5);
        double doubleResult = PlusMethod(4.3, 6.26);
        int threeIntResult = PlusMethod(1, 2, 3);

        Console.WriteLine($"PlusMethod(int, int): 8 + 5 = {intResult}");
        Console.WriteLine($"PlusMethod(double, double): 4.3 + 6.26 = {doubleResult}");
        Console.WriteLine($"PlusMethod(int, int, int): 1 + 2 + 3 = {threeIntResult}");
    }
}