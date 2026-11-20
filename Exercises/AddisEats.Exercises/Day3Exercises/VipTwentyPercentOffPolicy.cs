namespace AddisEats.Catalog;

public class VipTwentyPercentOffPolicy : IDiscountPolicy
{
    public decimal Apply(decimal amount) => amount * 0.80m;
}
