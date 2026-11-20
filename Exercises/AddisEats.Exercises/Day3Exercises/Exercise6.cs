namespace AddisEats.Catalog;

public static class Exercise6
{
    public static decimal Checkout(decimal amount, IDiscountPolicy policy) => policy.Apply(amount);

    public static void Run()
    {
        Console.WriteLine("=== Day 3 Exercise 6: IDiscountPolicy ===");

        decimal original = 400m;
        Console.WriteLine($"Original amount: {original} ETB");

        IDiscountPolicy tenPercent = new TenPercentOffPolicy();
        IDiscountPolicy fiftyOff = new FiftyEtbOffPolicy();
        IDiscountPolicy vipTwenty = new VipTwentyPercentOffPolicy();

        Console.WriteLine($"Ten percent off: {Checkout(original, tenPercent)} ETB");
        Console.WriteLine($"50 ETB off:      {Checkout(original, fiftyOff)} ETB");
        Console.WriteLine($"VIP 20% off (3rd policy, no change to Checkout): {Checkout(original, vipTwenty)} ETB");
    }
}
