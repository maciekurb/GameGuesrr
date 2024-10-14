namespace GameGuessr.Api.Domain.Common;

public static class StringExtensions
{
    public static bool HasValue(this string @string) => String.IsNullOrEmpty(@string) == false;
}
