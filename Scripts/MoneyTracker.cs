public static class MoneyTracker
{

    public static int Money { get; set; } = -10000;

    public const int MaxDebt = -10000;

    public static void Reset()
    {
        Money = -10000;
    }

    public static int ShopPrice(int level)
    {
        if (level == 1) return 50;
        if (level == 2) return 440;
        if (level == 3) return 1560;
        if (level == 4) return 4770;
        return 0;
    }
}