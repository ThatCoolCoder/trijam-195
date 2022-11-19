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
        if (level == 2) return 50;
        if (level == 3) return 740;
        if (level == 4) return 3600;
        if (level == 5) return 9999;
        return 0;
    }
}