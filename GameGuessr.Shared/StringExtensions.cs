namespace GameGuessr.Shared;

public static class StringExtensions
{
    public static string ReverseString(this string s)
    {
        var array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }
}
