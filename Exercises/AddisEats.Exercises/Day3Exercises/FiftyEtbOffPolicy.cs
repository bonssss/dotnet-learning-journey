namespace AddisEats.Catalog;

public class FiftyEtbOffPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount) => Math.Max(0m, amount - 50m);
}
