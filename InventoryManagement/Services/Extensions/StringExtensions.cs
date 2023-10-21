namespace Services.Extensions;

public static class StringExtensions
{
    public static string TransformToContinuousLowercase(this string str)
    {
        return string.Join("", str.Trim().ToLower().Split());
    }
}
