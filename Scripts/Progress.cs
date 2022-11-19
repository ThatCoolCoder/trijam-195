public static class Progress
{
    public static int Level { get; set; }

    public static void Reset()
    {
        Level = 1;
    }
}