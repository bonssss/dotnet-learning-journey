namespace AddisEats.Catalog;

public class TenPercentOffPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount) => amount * 0.90m;
}
