public static class NumberFormatter
{
    public static string FormatNumber(int num)
    {
        if (num >= 1_000_000) // Triliun
            return $"{(num / 1_000_000.0).ToString("0.######").TrimEnd('0').TrimEnd('.')} T";
        if (num >= 1_000) // Miliar
            return $"{(num / 1_000.0).ToString("0.######").TrimEnd('0').TrimEnd('.')} M";
        if (num >= 1) // Juta
            return $"{num} Jt";

        return num.ToString();
    }
}
